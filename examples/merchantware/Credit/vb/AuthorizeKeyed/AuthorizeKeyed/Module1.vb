Imports AuthorizeKeyed.MWCredit45

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
            .CardHolder = "John Doe",
            .AvsStreetAddress = "1 Federal St",
            .AvsZipCode = "02110",
            .CardVerificationValue = "123"
        }
        'Create Request Object
        Dim authorizationRequest As New AuthorizationRequest With {
            .Amount = "1.01",
            .TaxAmount = "0.10",
            .InvoiceNumber = "INV1234",
            .CardAcceptorTerminalId = "01",
            .CustomerCode = "1234",
            .PurchaseOrderNumber = "PO1234",
            .EnablePartialAuthorization = "true"
        }
        'Process Request
        Dim transactionResponse45 As TransactionResponse45
        transactionResponse45 = creditSoapClient.Authorize(merchantCredentials, paymentData, authorizationRequest)
        'Display Results
        Console.WriteLine(" Authorization Response: {0} Token: {1} Amount: ${2}", transactionResponse45.ApprovalStatus + vbNewLine, transactionResponse45.Token + vbNewLine, transactionResponse45.Amount + vbNewLine)
        Console.WriteLine("Press Any Key to Close")
        Console.ReadKey()
    End Sub

End Module
