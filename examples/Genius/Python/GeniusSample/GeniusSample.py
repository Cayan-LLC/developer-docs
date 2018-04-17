import urllib3
import lxml.etree as etree
import lxml.objectify as objectify
from zeep.client import Client

# Declare credentials to be used with the Stage Transaction Request
credentialsName = "TEST MERCHANT"
credentialsSiteID = "XXXXXXXX"
credentialsKey = "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX"
ipAddress = "192.168.0.123"

# Generate XML and XSD Validation
geniusSchema = etree.XMLSchema(file='Genius.xsd')
xmlparser = objectify.makeparser(schema=geniusSchema)

# Generate WSDL and SOAP Objects Build Transport request details
transportSoap = Client(wsdl='https://transport.merchantware.net/v4/transportService.asmx?WSDL')
transportRequest = transportSoap.get_type("ns0:TransportRequest")(
    TransactionType="SALE",
    Amount=1.01,
    ClerkId="1",
    OrderNumber="INV1234",
    Dba="TEST MERCHANT",
    SoftwareName="Test Software",
    SoftwareVersion="1.0",
    TransactionId="102911",
    TerminalId = "01",
    PoNumber="PO1234",
    TaxAmount="0.01",
    EntryMode="Undefined",
    ForceDuplicate=True
)

# Stage Transaction
print("Staging Transaction\n");
transportResponse = transportSoap.service.CreateTransaction(credentialsName, credentialsSiteID, credentialsKey, transportRequest)
transportKey = transportResponse.TransportKey
print("TransportKey Received: %s\n" % transportKey)

# Initiate transaction with TransportKey
print("Sending TransportKey %s to Terminal %s" % (transportKey, ipAddress))
geniusComm = urllib3.PoolManager()
geniusRequest = "http://%s:8080/v2/pos?TransportKey=%s&Format=XML" % (ipAddress, transportKey)
geniusResponse = geniusComm.request("GET", geniusRequest).data

# Validate the response with the Genius XSD
geniusResponseData = objectify.fromstring(geniusResponse, xmlparser)
print("Transaction Result: %s" % geniusResponseData.Status)
print("Amount: %s" % geniusResponseData.AmountApproved)
print("AuthCode: %s" % geniusResponseData.AuthorizationCode)
print("Token: %s" % geniusResponseData.Token)
print("Account Number: %s" % geniusResponseData.AccountNumber)

input("Press Enter to close")
