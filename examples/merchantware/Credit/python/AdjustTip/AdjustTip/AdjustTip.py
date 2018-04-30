from zeep.client import Client

SoapClient = Client(wsdl='https://ps1.merchantware.net/Merchantware/ws/RetailTransaction/v45/Credit.asmx?WSDL')
SoapRequest = SoapClient.service.AdjustTip(
    Credentials = {
        "MerchantName": "TEST MERCHANT",
        "MerchantSiteId": "XXXXXXXX",
        "MerchantKey": "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX"
        },
    Request = {
        "Token": "1234567890",
        "Amount": "1.01"
        })
print("Adjust Response: %s Token: %s Amount: $%s" % (SoapRequest.ApprovalStatus, SoapRequest.Token, SoapRequest.Amount))
input("Press Enter to close")
