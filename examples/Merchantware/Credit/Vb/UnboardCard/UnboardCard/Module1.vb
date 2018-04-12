Imports UnboardCard.MWCredit

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
        Dim Request As New VaultTokenRequest
        Request.VaultToken = "100000100ABCDE123456"

        'Process Request
        Dim Response As VaultBoardingResponse45
        Response = MWCredit.UnboardCard(Credentials, Request)

        'Display Results
        Console.WriteLine(" Vault Token: {0} Error Code: {1} Error Message: {2}", Response.VaultToken + vbNewLine, Response.ErrorCode + vbNewLine, Response.ErrorMessage + vbNewLine)
        Console.WriteLine("Press Any Key to Close")
        Console.ReadKey()
    End Sub

End Module
