Imports ForceCapture.MWCredit

Module Module1

    Sub Main()

        'Create Soap Client
        Dim CreditSoapClient As New CreditSoapClient
        'Create Credentials Object
        Dim merchantCredentials As New MerchantCredentials With {
            .MerchantName = "TEST MERCHANT",
            .MerchantSiteId = "XXXXXXXX",
            .MerchantKey = "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX"
            }
        'Create PaymentData Object
        Dim PaymentData As New PaymentData With {
            .Source = "KEYED",
            .CardNumber = "4012000033330026",
            .ExpirationDate = "1221",
            .CardHolder = "John Doe"
            }
        'Create Request Object
        Dim ForceCaptureRequest As New ForceCaptureRequest With {
            .Amount = "1.01",
            .AuthorizationCode = "ABC123",
            .InvoiceNumber = "INV1234",
            .RegisterNumber = "01",
            .CardAcceptorTerminalId = "01"
            }
        'Process Request
        Dim TransactionResponse45 As TransactionResponse45
        TransactionResponse45 = CreditSoapClient.ForceCapture(merchantCredentials, PaymentData, ForceCaptureRequest)
        'Display Results
        Console.WriteLine(" Force Capture Response: {0} Token: {1} Amount: ${2} RFMIQ: {3}", TransactionResponse45.ApprovalStatus + vbNewLine, TransactionResponse45.Token + vbNewLine, TransactionResponse45.Amount + vbNewLine, TransactionResponse45.Rfmiq + vbNewLine)
        Console.WriteLine("Press Any Key to Close")
        Console.ReadKey()
    End Sub

End Module
