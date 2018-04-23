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
        Console.WriteLine(" Adjust Response: {0} Token: {1} Authorization Code: {2} Transaction Date: {3} Amount: ${4} Remaining Card Balance: {5} Card Number: {6} Cardholder: {7} Card Type: {8} FSA Card: {9} Reader Entry Mode: {10} AVS Response: {11} CV Response: {12} Error Message: {13} Extra Data: {14}", TransactionResponse45.ApprovalStatus + vbNewLine, TransactionResponse45.Token + vbNewLine, TransactionResponse45.AuthorizationCode + vbNewLine, TransactionResponse45.TransactionDate + vbNewLine, TransactionResponse45.Amount + vbNewLine, TransactionResponse45.RemainingCardBalance + vbNewLine, TransactionResponse45.CardNumber + vbNewLine, TransactionResponse45.Cardholder + vbNewLine, TransactionResponse45.CardType + vbNewLine, TransactionResponse45.FsaCard + vbNewLine, TransactionResponse45.ReaderEntryMode + vbNewLine, TransactionResponse45.AvsResponse + vbNewLine, TransactionResponse45.CvResponse + vbNewLine, TransactionResponse45.ErrorMessage + vbNewLine, TransactionResponse45.ExtraData + vbNewLine)
        Console.WriteLine("Press Any Key to Close")
        Console.ReadKey()
    End Sub

End Module
