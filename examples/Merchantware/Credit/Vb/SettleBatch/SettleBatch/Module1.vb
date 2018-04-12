Imports SettleBatch.MWCredit

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

        'Process Request
        Dim SettleResponse As BatchResponse45
        SettleResponse = MWCredit.SettleBatch(Credentials)

        'Display Results
        Console.WriteLine(" Batch Status: {0} Amount: ${1} Authorization Code: {2} Transaction Count: {3} Transaction Date: {4} Error Message: {5}", SettleResponse.BatchStatus + vbNewLine, SettleResponse.BatchAmount + vbNewLine, SettleResponse.AuthorizationCode + vbNewLine, SettleResponse.TransactionCount + vbNewLine, SettleResponse.TransactionDate + vbNewLine, SettleResponse.ErrorMessage + vbNewLine)
        Console.WriteLine("Press Any Key to Close")
        Console.ReadKey()
    End Sub

End Module
