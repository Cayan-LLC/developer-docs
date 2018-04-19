Imports FindBoardedCard.MWCredit45

Module Module1

    Sub Main()
        'Create Soap Client
        Dim creditSoapClient As New CreditSoapClient
        'Create Credentials Object
        Dim merchantCredentials As New MerchantCredentials With {
            .MerchantName = "TEST MERCHANT",
            .MerchantSiteId = "XXXXXXXX",
            .MerchantKey = "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX"
        }
        'Create Request Object
        Dim vaultTokenRequest As New VaultTokenRequest With {
            .VaultToken = "100000100ABCDE123456"
        }
        'Process Request
        Dim vaultTokenResponse45 As VaultTokenResponse45
        vaultTokenResponse45 = creditSoapClient.FindBoardedCard(merchantCredentials, vaultTokenRequest)
        'Display Results
        Console.WriteLine(" Masked Card Number: {0} Expiration Date: {1} Cardholder: {2} Card Type: {3} AVS Street Address: {4} Postal Code: {5} Error Code: {6} Error Message: {7}", vaultTokenResponse45.CardNumber + vbNewLine, vaultTokenResponse45.ExpirationDate + vbNewLine, vaultTokenResponse45.Cardholder + vbNewLine, vaultTokenResponse45.CardType + vbNewLine, vaultTokenResponse45.AvsStreetAddress + vbNewLine, vaultTokenResponse45.AvsZipCode + vbNewLine, vaultTokenResponse45.ErrorCode + vbNewLine, vaultTokenResponse45.ErrorMessage + vbNewLine)
        Console.WriteLine("Press Any Key to Close")
        Console.ReadKey()
    End Sub

End Module
