Imports UnboardCard.Cayan45
Module Module1

    Sub Main()
        'Create Soap Client
        Dim Cayan45 As New CreditSoapClient

        'Create Credentails Object
        Dim Credentials As New MerchantCredentials With {
            .MerchantName = "TEST MERCHANT",
            .MerchantSiteId = "XXXXXXXX",
            .MerchantKey = "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX"
        }

        'Create Request Object
        Dim Request As New VaultTokenRequest
        Request.VaultToken = "100000100AOL628QZ3K5"

        'Process Request
        Dim Response As VaultBoardingResponse45
        Response = Cayan45.UnboardCard(Credentials, Request)

        'Display Results
        Console.WriteLine(" Vault Token: {0} Error Code: {1} Error Message: {2}", Response.VaultToken + vbNewLine, Response.ErrorCode + vbNewLine, Response.ErrorMessage + vbNewLine)
        Console.WriteLine("Press Any Key to Close")
        Console.ReadKey()
    End Sub

End Module
