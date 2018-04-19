Imports MW_VB_RefundKeyed.MWCredit

Module Module1

    Sub Main()
        'Create Soap Client
        Dim soapClient As New CreditSoapClient

        'Create Credentails Object
        Dim merchantCredentials As New MerchantCredentials With {
        .MerchantName = "TEST MERCHANT",
        .MerchantSiteId = "XXXXXXXX",
        .MerchantKey = "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX"
        }

        'Create PaymentData Object
        Dim paymentData As New MWCredit.PaymentData With {
        .Source = "KEYED",
        .CardNumber = "4012000033330026",
        .ExpirationDate = "1221",
        .CardHolder = "John Doe",
        .AvsStreetAddress = "1 Federal St",
        .AvsZipCode = "02110",
        .CardVerificationValue = "123"
        }

        'Create Request Object
        Dim refundRequest As New RefundRequest With {
        .Amount = "1.01",
        .InvoiceNumber = "INV1234",
        .CardAcceptorTerminalId = "01",
        .RegisterNumber = "01",
        .MerchantTransactionId = "123"
        }

        'Process Request
        Dim transactionResponse45 As MWCredit.TransactionResponse45
        transactionResponse45 = soapClient.Refund(merchantCredentials, paymentData, refundRequest)

        'Display Results
        Console.WriteLine(" Refund Response: {0} Token: {1} Amount: ${2} RFMIQ: {3}", transactionResponse45.ApprovalStatus + vbNewLine, transactionResponse45.Token + vbNewLine, transactionResponse45.Amount + vbNewLine, transactionResponse45.Rfmiq + vbNewLine)
        Console.WriteLine("Press Any Key to Close")
        Console.ReadKey()
    End Sub

End Module