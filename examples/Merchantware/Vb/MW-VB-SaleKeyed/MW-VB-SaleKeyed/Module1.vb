Imports MW_VB_SaleKeyed.MWCredit

Module Module1

    Sub Main()
        'Create Soap Client
        Dim soapClient As New CreditSoapClient

        'Create Credentails Object
        Dim merchantCredentials As New MerchantCredentials With {
        .MerchantName = "Test Merchant",
        .MerchantSiteId = "XXXXXXXX",
        .MerchantKey = "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX"
        }

        'Create PaymentData Object
        Dim paymentData As New MWCredit.PaymentData With {
        .Source = "KEYED",
        .CardNumber = "4012000033330026",
        .ExpirationDate = "1220",
        .CardHolder = "John Doe",
        .AvsStreetAddress = "1 Federal St",
        .AvsZipCode = "02110",
        .CardVerificationValue = "123"
        }

        'Create Request Object
        Dim saleRequest As New SaleRequest With {
        .Amount = "1.01",
        .TaxAmount = "0.10",
        .InvoiceNumber = "INV1234",
        .CardAcceptorTerminalId = "01",
        .CustomerCode = "1234",
        .PurchaseOrderNumber = "PO1234",
        .EnablePartialAuthorization = "true"
        }

        'Process Request
        Dim transactionResponse45 As MWCredit.TransactionResponse45
        transactionResponse45 = soapClient.Sale(merchantCredentials, paymentData, saleRequest)

        'Display Results
        Console.WriteLine(" Sale Response: {0} Token: {1} Amount: ${2} RFMIQ: {3}", transactionResponse45.ApprovalStatus + vbNewLine, transactionResponse45.Token + vbNewLine, transactionResponse45.Amount + vbNewLine, transactionResponse45.Rfmiq + vbNewLine)
        Console.WriteLine("Press Any Key to Close")
        Console.ReadKey()
    End Sub

End Module