Imports AdjustTip.MWCredit

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
        'Create Request Object
        Dim tipRequest As New TipRequest With {
            .Token = "1234567890",
            .Amount = "1.01"
            }
        'Process Request
        Dim transactionResponse45 As TransactionResponse45
        transactionResponse45 = creditSoapClient.AdjustTip(merchantCredentials, tipRequest)
        'Display Results
        Console.WriteLine(" Adjust Response: {0} Token: {1} Authorization Code: {2} Transaction Date: {3} Amount: ${4} Remaining Card Balance: {5} Card Number: {6} Cardholder: {7} Card Type: {8} FSA Card: {9} Reader Entry Mode: {10} AVS Response: {11} CV Response: {12} Error Message: {13} Extra Data: {14}", transactionResponse45.ApprovalStatus + vbNewLine, transactionResponse45.Token + vbNewLine, transactionResponse45.AuthorizationCode + vbNewLine, transactionResponse45.TransactionDate + vbNewLine, transactionResponse45.Amount + vbNewLine, transactionResponse45.RemainingCardBalance + vbNewLine, transactionResponse45.CardNumber + vbNewLine, transactionResponse45.Cardholder + vbNewLine, transactionResponse45.CardType + vbNewLine, transactionResponse45.FsaCard + vbNewLine, transactionResponse45.ReaderEntryMode + vbNewLine, transactionResponse45.AvsResponse + vbNewLine, transactionResponse45.CvResponse + vbNewLine, transactionResponse45.ErrorMessage + vbNewLine, transactionResponse45.ExtraData + vbNewLine)
        Console.WriteLine("Press Any Key to Close")
        Console.ReadKey()
    End Sub

End Module
