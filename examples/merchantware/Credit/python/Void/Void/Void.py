from zeep.client import Client

SoapClient = Client(wsdl='https://ps1.merchantware.net/Merchantware/ws/RetailTransaction/v45/Credit.asmx?WSDL')
SoapRequest = SoapClient.service.Void(
    Credentials = {
        "MerchantName": "TEST MERCHANT",
        "MerchantSiteId": "XXXXXXXX",
        "MerchantKey": "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX"
        },
    PaymentData = {
        "Source": "PreviousTransaction",
        "VaultToken": "1234567890"
        },
    Request = {
        "InvoiceNumber": "INV1234",
        "CardAcceptorTerminalId": "01"
        })
print("Void Response: %s Token: %s Amount: $%s" % (SoapRequest.ApprovalStatus, SoapRequest.Token, SoapRequest.Amount))
input("Press Enter to close")

