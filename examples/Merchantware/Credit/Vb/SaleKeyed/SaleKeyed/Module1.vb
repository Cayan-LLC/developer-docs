Imports SaleKeyed.Cayan45

Module Module1

    Sub Main()
        'Create Soap Client
        Dim Cayan45 As New CreditSoapClient

        'Create Credentails Object
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
        .CardHolder = "John Doe",
        .AvsStreetAddress = "1 Federal Street",
        .AvsZipCode = "02110",
        .CardVerificationValue = "123"
        }

        'Create Request Object
        Dim SaleRequest As New SaleRequest With {
        .Amount = "1.00",
        .TaxAmount = ".10",
        .InvoiceNumber = "1234",
        .CardAcceptorTerminalId = "01",
        .CustomerCode = "1234",
        .PurchaseOrderNumber = "1234",
        .EnablePartialAuthorization = "true"
        }

        'Process Request
        Dim Response As TransactionResponse45
        Response = Cayan45.Sale(Credentials, PaymentData, SaleRequest)

        'Display Results
        Console.WriteLine(" Sale Response: {0} Token: {1} Amount: ${2} RFMIQ: {3}", Response.ApprovalStatus + vbNewLine, Response.Token + vbNewLine, Response.Amount + vbNewLine, Response.Rfmiq + vbNewLine)
        Console.WriteLine("Press Any Key to Close")
        Console.ReadKey()
    End Sub

End Module
