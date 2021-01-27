from zeep.client import Client

SoapClient = Client(wsdl='https://ps1.merchantware.net/Merchantware/ws/RetailTransaction/v45/Credit.asmx?WSDL')
SoapRequest = SoapClient.service.BoardCard(
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
        })
print("Token: %s Error Code: %s Error Message: %s" % (SoapRequest.VaultToken, SoapRequest.ErrorCode, SoapRequest.ErrorMessage))
input("Press Enter to close")
