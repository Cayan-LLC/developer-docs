Imports Capture.MWCredit

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

        'Create Request Object
        Dim CaptureRequest As New CaptureRequest With {
        .Token = "1234567890",
        .Amount = "1.01",
        .InvoiceNumber = "INV1234",
        .CardAcceptorTerminalId = "01"
        }

        'Process Request
        Dim CaptureResponse As TransactionResponse45
        CaptureResponse = MWCredit.Capture(Credentials, CaptureRequest)

        'Display Results
        Console.WriteLine(" Capture Response: {0} Token: {1} Amount: ${2} RFMIQ: {3}", CaptureResponse.ApprovalStatus + vbNewLine, CaptureResponse.Token + vbNewLine, CaptureResponse.Amount + vbNewLine, CaptureResponse.Rfmiq + vbNewLine)
        Console.WriteLine("Press Any Key to Close")
        Console.ReadKey()
    End Sub

End Module
