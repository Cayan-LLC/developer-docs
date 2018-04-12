Imports ForceCapture.MWCredit

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

        'Create PaymentData Object
        Dim PaymentData As New PaymentData With {
        .Source = "KEYED",
        .CardNumber = "4012000033330026",
        .ExpirationDate = "1221",
        .CardHolder = "John Doe"
        }

        'Create Request Object
        Dim ForceRequest As New ForceCaptureRequest With {
        .Amount = "1.01",
        .AuthorizationCode = "ABC123",
        .InvoiceNumber = "INV1234",
        .RegisterNumber = "01",
        .CardAcceptorTerminalId = "01"
        }

        'Process Request
        Dim ForceResponse As TransactionResponse45
        ForceResponse = MWCredit.ForceCapture(Credentials, PaymentData, ForceRequest)

        'Display Results
        Console.WriteLine(" Force Capture Response: {0} Token: {1} Amount: ${2} RFMIQ: {3}", ForceResponse.ApprovalStatus + vbNewLine, ForceResponse.Token + vbNewLine, ForceResponse.Amount + vbNewLine, ForceResponse.Rfmiq + vbNewLine)
        Console.WriteLine("Press Any Key to Close")
        Console.ReadKey()
    End Sub

End Module
