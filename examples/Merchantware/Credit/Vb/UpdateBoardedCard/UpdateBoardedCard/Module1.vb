Imports UpdateBoardedCard.MWCredit45

Module Module1

    Sub Main()
        'Create Soap Client
        Dim creditSoapClient As New CreditSoapClient
        'Create Credentails Object
        Dim merchantCredentials As New MerchantCredentials With {
            .MerchantName = "TEST MERCHANT",
            .MerchantSiteId = "XXXXXXXX",
            .MerchantKey = "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX"
        }
        'Create Request Object
        Dim updateBoardedCardRequest As New UpdateBoardedCardRequest With {
            .VaultToken = "100000100ABCDE123456",
            .ExpirationDate = "1221"
        }
        'Process Request
        Dim vaultBoardingResponse45 As VaultBoardingResponse45
        vaultBoardingResponse45 = creditSoapClient.UpdateBoardedCard(merchantCredentials, updateBoardedCardRequest)
        'Display Results
        Console.WriteLine(" Vault Token: {0} Error Code: {1} Error Message: {2}", vaultBoardingResponse45.VaultToken + vbNewLine, vaultBoardingResponse45.ErrorCode + vbNewLine, vaultBoardingResponse45.ErrorMessage + vbNewLine)
        Console.WriteLine("Press Any Key to Close")
        Console.ReadKey()
    End Sub

End Module
