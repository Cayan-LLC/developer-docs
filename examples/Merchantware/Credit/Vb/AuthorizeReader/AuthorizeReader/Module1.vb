Imports AuthorizeReader.MWCredit

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
        .Source = "READER",
        .TrackData = "%B4012000033330026^TEST CARD/GENIUS^191210054321000000000000000 150;4012000033330026=19121011000012345678?"
        }

        'Create Request Object
        Dim AuthorizeRequest As New AuthorizationRequest With {
        .Amount = "1.01",
        .TaxAmount = "0.10",
        .InvoiceNumber = "INV1234",
        .CardAcceptorTerminalId = "01",
        .CustomerCode = "1234",
        .PurchaseOrderNumber = "PO1234",
        .EnablePartialAuthorization = "true"
        }

        'Process Request
        Dim AuthorizeResponse As TransactionResponse45
        AuthorizeResponse = MWCredit.Authorize(Credentials, PaymentData, AuthorizeRequest)

        'Display Results
        Console.WriteLine(" Authorization Response: {0} Token: {1} Amount: ${2} RFMIQ: {3}", AuthorizeResponse.ApprovalStatus + vbNewLine, AuthorizeResponse.Token + vbNewLine, AuthorizeResponse.Amount + vbNewLine, AuthorizeResponse.Rfmiq + vbNewLine)
        Console.WriteLine("Press Any Key to Close")
        Console.ReadKey()
    End Sub

End Module
