from zeep.client import Client

SoapClient = Client(wsdl='https://ps1.merchantware.net/Merchantware/ws/RetailTransaction/v45/Credit.asmx?WSDL')
SoapRequest = SoapClient.service.Sale(
    Credentials = {
        "MerchantName": "TEST MERCHANT",
        "MerchantSiteId": "XXXXXXXX",
        "MerchantKey": "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX"
        },
    PaymentData = {
        "Source": "Keyed",
        "CardNumber": "4012000033330026",
        "ExpirationDate": "1220",
        "CardHolder": "John Doe",
        "AvsStreetAddress": "1 Federal St",
        "AvsZipCode": "02110",
        "CardVerificationValue": "123"
        },
    Request = {
        "Amount": "1.01",
        "TaxAmount": "0.10",
        "InvoiceNumber": "INV1234",
        "CardAcceptorTerminalId": "01",
        "CustomerCode": "1234",
        "PurchaseOrderNumber": "PO1234",
        "EnablePartialAuthorization": "true"
        })
print("Sale Response: %s Token: %s Amount: $%s" % (SoapRequest.ApprovalStatus, SoapRequest.Token, SoapRequest.Amount))
input("Press Enter to close")
