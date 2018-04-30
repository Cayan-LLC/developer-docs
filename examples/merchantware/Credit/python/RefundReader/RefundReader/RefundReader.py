from zeep.client import Client

SoapClient = Client(wsdl='https://ps1.merchantware.net/Merchantware/ws/RetailTransaction/v45/Credit.asmx?WSDL')
SoapRequest = SoapClient.service.Refund(
    Credentials = {
        "MerchantName": "TEST MERCHANT",
        "MerchantSiteId": "XXXXXXXX",
        "MerchantKey": "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX"
        },
    PaymentData = {
        "Source": "Reader",
        "TrackData": "%B4012000033330026^TEST CARD/GENIUS^181210054321000000000000000 150 A?;4012000033330026=18121011000012345678?"
        },
    Request = {
        "Amount": "1.01",
        "InvoiceNumber": "INV1234",
        "CardAcceptorTerminalId": "01"
        })
print("Refund Response: %s Token: %s Amount: $%s" % (SoapRequest.ApprovalStatus, SoapRequest.Token, SoapRequest.Amount))
input("Press Enter to close")
