from zeep.client import Client

SoapClient = Client(wsdl='https://ps1.merchantware.net/Merchantware/ws/RetailTransaction/v45/Credit.asmx?WSDL')
SoapRequest = SoapClient.service.ForceCapture(
    Credentials = {
        "MerchantName": "TEST MERCHANT",
        "MerchantSiteId": "XXXXXXXX",
        "MerchantKey": "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX"
        },
    PaymentData = {
        "Source": "Keyed",
        "CardNumber": "4012000033330026",
        "ExpirationDate": "1220",
        "CardHolder": "John Doe"
        },
    Request = {
        "Amount": "1.01",
        "AuthorizationCode": "ABC123",
        "InvoiceNumber": "INV1234",
        "RegisterNumber": "01",
        "CardAcceptorTerminalId": "01"
        })
print("Capture Response: %s Token: %s Amount: $%s" % (SoapRequest.ApprovalStatus, SoapRequest.Token, SoapRequest.Amount))
input("Press Enter to close")

