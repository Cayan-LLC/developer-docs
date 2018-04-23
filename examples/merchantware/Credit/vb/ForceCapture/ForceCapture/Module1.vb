Imports ForceCapture.MWCredit45

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
        'Create PaymentData Object
        Dim paymentData As New PaymentData With {
            .Source = "KEYED",
            .CardNumber = "4012000033330026",
            .ExpirationDate = "1221",
            .CardHolder = "John Doe"
        }
        'Create Request Object
        Dim forceCaptureRequest As New ForceCaptureRequest With {
            .Amount = "1.01",
            .AuthorizationCode = "ABC123",
            .InvoiceNumber = "INV1234",
            .RegisterNumber = "01",
            .CardAcceptorTerminalId = "01"
        }
        'Process Request
        Dim transactionResponse45 As TransactionResponse45
        transactionResponse45 = creditSoapClient.ForceCapture(merchantCredentials, paymentData, forceCaptureRequest)
        'Display Results
        Console.WriteLine(" Force Capture Response: {0} Token: {1} Amount: ${2}", transactionResponse45.ApprovalStatus + vbNewLine, transactionResponse45.Token + vbNewLine, transactionResponse45.Amount + vbNewLine)
        Console.WriteLine("Press Any Key to Close")
        Console.ReadKey()
    End Sub

End Module
