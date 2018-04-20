from zeep.client import Client

# Declare credentials to be used with the Stage Transaction Request
credentials_name = "TEST MERCHANT"
credentials_site_id = "XXXXXXXX"
credentials_key = "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX"
ip_address = "192.168.0.123"
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

input("Press Enter to close")