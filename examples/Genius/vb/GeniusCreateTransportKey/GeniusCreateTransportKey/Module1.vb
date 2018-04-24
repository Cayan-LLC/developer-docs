Imports GeniusCreateTransportKey.TransportService
Module Module1

    Sub Main()
        ' Declare credentials to be used with the Stage Transaction Request 
        Dim transportSoapClient As New TransportServiceSoapClient
        Dim credentialsName = "TEST MERCHANT"
        Dim credentialsSiteId = "XXXXXXXX"
        Dim credentialsKey = "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX"
        Dim ipAddress = "10.4.50.151"
        ' Build Transport request details
        Dim transportRequest As New TransportRequest With {
            .TransactionType = "SALE",
            .Amount = 1.01,
            .ClerkId = "1",
            .OrderNumber = "INV1234",
            .Dba = "TEST MERCHANT",
            .SoftwareName = "Test Software",
            .SoftwareVersion = "1.0",
            .TerminalId = "01",
            .PoNumber = "PO1234",
            .TaxAmount = "0.10",
            .EntryMode = EntryMode.Undefined,
            .ForceDuplicate = True
        }
        ' Stage Transaction
        Console.WriteLine("Staging Transaction{0}", Environment.NewLine)
        Dim transportResponse = transportSoapClient.CreateTransaction(credentialsName, credentialsSiteId, credentialsKey, transportRequest)
        Console.WriteLine("TransportKey Received {0}{1}", transportResponse.TransportKey, Environment.NewLine)
        Console.WriteLine("Press Any Key to Close")
        Console.ReadKey()
    End Sub

End Module
