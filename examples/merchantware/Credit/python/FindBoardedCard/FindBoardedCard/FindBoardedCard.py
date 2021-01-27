from zeep.client import Client

SoapClient = Client(wsdl='https://ps1.merchantware.net/Merchantware/ws/RetailTransaction/v45/Credit.asmx?WSDL')
SoapRequest = SoapClient.service.FindBoardedCard(
    Credentials = {
        "MerchantName": "TEST MERCHANT",
        "MerchantSiteId": "XXXXXXXX",
        "MerchantKey": "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX"
        },
    Request = {
        "VaultToken": "100000100ABCDE123456"
        })
print("Masked Card Number: %s Expiration Date: %s Cardholder: %s Card Type: %s AVS Street Address: %s Postal Code: %s Error Code: %s  Error Message: %s" % 
      (SoapRequest.CardNumber, SoapRequest.ExpirationDate, SoapRequest.Cardholder, 
       SoapRequest.CardType, SoapRequest.AvsStreetAddress, SoapRequest.AvsZipCode, 
       SoapRequest.ErrorCode, SoapRequest.ErrorMessage))
input("Press Enter to close")
