Imports Void.MWCredit

Module Module1

    Sub Main()
        'Create Soap Client
        Dim MWCredit As New CreditSoapClient

        'Create Credentails Object
        Dim Credentials As New MerchantCredentials With {
        .MerchantName = "TEST MERCHANT",
        .MerchantSiteId = "XXXXXXXX",
        .MerchantKey = "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX"
        }

        'Create Request Object
        Dim VoidRequest As New VoidRequest With {
        .Token = "1234567890",
        .CardAcceptorTerminalId = "01"
        }

        'Process Request
        Dim VoidResponse As TransactionResponse45
        VoidResponse = MWCredit.Void(Credentials, VoidRequest)

        'Display Results
        Console.WriteLine(" Void Response: {0} Token: {1} Amount: ${2} RFMIQ: {3}", VoidResponse.ApprovalStatus + vbNewLine, VoidResponse.Token + vbNewLine, VoidResponse.Amount + vbNewLine, VoidResponse.Rfmiq + vbNewLine)
        Console.WriteLine("Press Any Key to Close")
        Console.ReadKey()
    End Sub

End Module
