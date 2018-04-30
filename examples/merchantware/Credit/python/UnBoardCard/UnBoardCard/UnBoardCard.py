from zeep.client import Client

SoapClient = Client(wsdl='https://ps1.merchantware.net/Merchantware/ws/RetailTransaction/v45/Credit.asmx?WSDL')
SoapRequest = SoapClient.service.UnBoardCard(
    Credentials = {
        "MerchantName": "TEST MERCHANT",
        "MerchantSiteId": "XXXXXXXX",
        "MerchantKey": "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX"
        },
    Request = {
        "VaultToken": "100000100ABCDE123456"
        })
print("Token: %s Error Code: %s Error Message: %s" % (SoapRequest.VaultToken, SoapRequest.ErrorCode, SoapRequest.ErrorMessage))
input("Press Enter to close")
