import urllib3
import lxml.etree as etree
import lxml.objectify as objectify
from zeep.client import Client

# Declare credentials to be used with the Stage Transaction Request
credentials_name = "TEST MERCHANT"
credentials_site_id = "XXXXXXXX"
credentials_key = "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX"
ip_address = "192.168.0.123"
# Generate XML and XSD Validation
genius_schema = etree.XMLSchema(file='Genius.xsd')
xml_parser = objectify.makeparser(schema=genius_schema)
# Generate WSDL and SOAP Objects Build Transport request details
transport_soap = Client(wsdl='https://transport.merchantware.net/v4/transportService.asmx?WSDL')
transport_request = transport_soap.get_type("ns0:TransportRequest")(
    TransactionType="SALE",
    Amount=1.01,
    ClerkId="1",
    OrderNumber="INV1234",
    Dba="TEST MERCHANT",
    SoftwareName="Test Software",
    SoftwareVersion="1.0",
    TerminalId = "01",
    PoNumber="PO1234",
    TaxAmount="0.10",
    EntryMode="Undefined",
    ForceDuplicate=True
)
# Stage Transaction
print("Staging Transaction\n");
transport_response = transport_soap.service.CreateTransaction(credentials_name, credentials_site_id, credentials_key, transport_request)
transport_key = transport_response.TransportKey
print("TransportKey Received: %s\n" % transport_key)
# Initiate transaction with TransportKey
print("Sending TransportKey %s to Terminal %s" % (transport_key, ip_address))
genius_comm = urllib3.PoolManager()
genius_request_url = "http://%s:8080/v2/pos?TransportKey=%s&Format=XML" % (ip_address, transport_key)
genius_response = genius_comm.request("GET", genius_request_url).data
# Validate the response with the Genius XSD
genius_response_data = objectify.fromstring(genius_response, xml_parser)
print("Transaction Result: %s" % genius_response_data.Status)
print("Amount: %s" % genius_response_data.AmountApproved)
print("AuthCode: %s" % genius_response_data.AuthorizationCode)
print("Token: %s" % genius_response_data.Token)
print("Account Number: %s" % genius_response_data.AccountNumber)

input("Press Enter to close")
