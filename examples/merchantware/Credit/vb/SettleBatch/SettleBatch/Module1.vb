Imports SettleBatch.MWCredit45

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
        'Process Request
        Dim batchResponse45 As BatchResponse45
        batchResponse45 = creditSoapClient.SettleBatch(merchantCredentials)
        'Display Results
        Console.WriteLine(" Batch Status: {0} Amount: ${1} Authorization Code: {2} Transaction Count: {3} Transaction Date: {4} Error Message: {5}", batchResponse45.BatchStatus + vbNewLine, batchResponse45.BatchAmount + vbNewLine, batchResponse45.AuthorizationCode + vbNewLine, batchResponse45.TransactionCount + vbNewLine, batchResponse45.TransactionDate + vbNewLine, batchResponse45.ErrorMessage + vbNewLine)
        Console.WriteLine("Press Any Key to Close")
        Console.ReadKey()
    End Sub

End Module
