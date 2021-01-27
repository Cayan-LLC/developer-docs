from zeep.client import Client

SoapClient = Client(wsdl='https://ps1.merchantware.net/Merchantware/ws/RetailTransaction/v45/Credit.asmx?WSDL')
SoapRequest = SoapClient.service.BoardCard(
    Credentials = {
        "MerchantName": "TEST MERCHANT",
        "MerchantSiteId": "XXXXXXXX",
        "MerchantKey": "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX"
        },
    PaymentData = {
        "Source": "Reader",
        "TrackData": "%B4012000033330026^TEST CARD/GENIUS^181210054321000000000000000 150 A?;4012000033330026=18121011000012345678?"
        })
print("Token: %s Error Code: %s Error Message: %s" % (SoapRequest.VaultToken, SoapRequest.ErrorCode, SoapRequest.ErrorMessage))
input("Press Enter to close")
