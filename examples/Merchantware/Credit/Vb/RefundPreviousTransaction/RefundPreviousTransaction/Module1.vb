Imports RefundPreviousTransaction.MWCredit

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
        .Source = "PREVIOUSTRANSACTION",
        .Token = "1234567890"
        }

        'Create Request Object
        Dim RefundRequest As New RefundRequest With {
        .Amount = "1.01",
        .InvoiceNumber = "INV1234",
        .CardAcceptorTerminalId = "01"
        }

        'Process Request
        Dim RefundResponse As TransactionResponse45
        RefundResponse = MWCredit.Refund(Credentials, PaymentData, RefundRequest)

        'Display Results
        Console.WriteLine(" Refund Response: {0} Token: {1} Amount: ${2} RFMIQ: {3}", RefundResponse.ApprovalStatus + vbNewLine, RefundResponse.Token + vbNewLine, RefundResponse.Amount + vbNewLine, RefundResponse.Rfmiq + vbNewLine)
        Console.WriteLine("Press Any Key to Close")
        Console.ReadKey()
    End Sub

End Module
