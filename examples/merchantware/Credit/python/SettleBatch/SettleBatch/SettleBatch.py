from zeep.client import Client

SoapClient = Client(wsdl='https://ps1.merchantware.net/Merchantware/ws/RetailTransaction/v45/Credit.asmx?WSDL')
SoapRequest = SoapClient.service.SettleBatch(
    Credentials = {
        "MerchantName": "TEST MERCHANT",
        "MerchantSiteId": "XXXXXXXX",
        "MerchantKey": "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX"
        })
print("Batch Status: %s Authorization Code: %s Amount: $%s TransactionCount: %s TransactionDate: %s ErrorMessage: %s" %
      (SoapRequest.BatchStatus, SoapRequest.AuthorizationCode, SoapRequest.BatchAmount,
       SoapRequest.TransactionCount, SoapRequest.TransactionDate, SoapRequest.ErrorMessage))
input("Press Enter to close")
