Imports Capture.MWCredit45

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
        Dim captureRequest As New CaptureRequest With {
            .Token = "1234567890",
            .Amount = "1.01",
            .InvoiceNumber = "INV1234",
            .CardAcceptorTerminalId = "01"
        }
        'Process Request
        Dim transactionresponse45 As TransactionResponse45
        transactionresponse45 = creditSoapClient.Capture(merchantCredentials, captureRequest)
        'Display Results
        Console.WriteLine(" Capture Response: {0} Token: {1} Amount: ${2}", transactionresponse45.ApprovalStatus + vbNewLine, transactionresponse45.Token + vbNewLine, transactionresponse45.Amount + vbNewLine)
        Console.WriteLine("Press Any Key to Close")
        Console.ReadKey()
    End Sub

End Module
