Imports AdjustTip.MWCredit

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
        Dim AdjustRequest As New TipRequest With {
        .Token = "1234567890",
        .Amount = "1.01"
        }

        'Process Request
        Dim AdjustResponse As TransactionResponse45
        AdjustResponse = MWCredit.AdjustTip(Credentials, AdjustRequest)

        'Display Results
        Console.WriteLine(" Adjust Response: {0} Token: {1} Amount: ${2} RFMIQ: {3}", AdjustResponse.ApprovalStatus + vbNewLine, AdjustResponse.Token + vbNewLine, AdjustResponse.Amount + vbNewLine, AdjustResponse.Rfmiq + vbNewLine)
        Console.WriteLine("Press Any Key to Close")
        Console.ReadKey()
    End Sub

End Module
