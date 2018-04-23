Imports RefundVault.MWCredit45

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
            .Source = "VAULT",
            .VaultToken = "100000100ABCDE123456"
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
        Dim transactionResponse45 As TransactionResponse45
        transactionResponse45 = creditSoapClient.Refund(merchantCredentials, paymentData, refundRequest)
        'Display Results
        Console.WriteLine(" Refund Response: {0} Token: {1} Amount: ${2}", transactionResponse45.ApprovalStatus + vbNewLine, transactionResponse45.Token + vbNewLine, transactionResponse45.Amount + vbNewLine)
        Console.WriteLine("Press Any Key to Close")
        Console.ReadKey()
    End Sub

End Module
