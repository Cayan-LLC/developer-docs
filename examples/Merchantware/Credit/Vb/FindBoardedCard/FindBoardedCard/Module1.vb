Imports FindBoardedCard.MWCredit

Module Module1

    Sub Main()
        'Create Soap Client
        Dim MWCredit As New CreditSoapClient

        'Create Credentials Object
        Dim Credentials As New MerchantCredentials With {
        .MerchantName = "TEST MERCHANT",
        .MerchantSiteId = "XXXXXXXX",
        .MerchantKey = "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX"
        }

        'Create Request Object
        Dim Request As New VaultTokenRequest With {
        .VaultToken = "100000100ABCDE123456"
        }

        'Process Request
        Dim Response As VaultTokenResponse45
        Response = MWCredit.FindBoardedCard(Credentials, Request)

        'Display Results
        Console.WriteLine(" Masked Card Number: {0} Expiration Date: {1} Cardholder: {2} Card Type: {3} AVS Street Address: {4} Postal Code: {5} Error Code: {6} Error Message: {7} RFMIQ: {8}", Response.CardNumber + vbNewLine, Response.ExpirationDate + vbNewLine, Response.Cardholder + vbNewLine, Response.CardType + vbNewLine, Response.AvsStreetAddress + vbNewLine, Response.AvsZipCode + vbNewLine, Response.ErrorCode + vbNewLine, Response.ErrorMessage + vbNewLine, Response.Rfmiq + vbNewLine)
        Console.WriteLine("Press Any Key to Close")
        Console.ReadKey()
    End Sub

End Module
