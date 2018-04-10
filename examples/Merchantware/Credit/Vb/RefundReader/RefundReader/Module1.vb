Imports RefundReader.Cayan45

Module Module1

    Sub Main()
        'Create Soap Client
        Dim Cayan45 As New CreditSoapClient

        'Create Credentails Object
        Dim Credentials As New MerchantCredentials With {
        .MerchantName = "Spider POS",
        .MerchantSiteId = "42424242",
        .MerchantKey = "44444-44444-44444-44444-44444"
        }

        'Create PaymentData Object
        Dim PaymentData As New PaymentData With {
        .Source = "READER",
        .TrackData = "%B4012000033330026^TEST CARD/GENIUS^191210054321000000000000000 150;4012000033330026=19121011000012345678?"
        }

        'Create Request Object
        Dim RefundRequest As New RefundRequest With {
        .Amount = "1.00",
        .InvoiceNumber = "1234",
        .CardAcceptorTerminalId = "01",
        .RegisterNumber = "01",
        .MerchantTransactionId = "123"
        }

        'Process Request
        Dim Response As TransactionResponse45
        Response = Cayan45.Refund(Credentials, PaymentData, RefundRequest)

        'Display Results
        Console.WriteLine(" Refund Response: {0} Token: {1} Amount: ${2} RFMIQ: {3}", Response.ApprovalStatus + vbNewLine, Response.Token + vbNewLine, Response.Amount + vbNewLine, Response.Rfmiq + vbNewLine)
        Console.WriteLine("Press Any Key to Close")
        Console.ReadKey()
    End Sub

End Module
