Imports System.Xml.Serialization
Imports GeniusThreaded.TransportService
Module Module1

    Sub Main()
        ' Declare credentials to be used with the Stage Transaction Request 
        Dim transportSoapClient As New TransportServiceSoapClient
        Dim credentialsName = "TEST MERCHANT"
        Dim credentialsSiteId = "XXXXXXXX"
        Dim credentialsKey = "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX"
        Dim ipAddress = "192.168.0.123"

        ' Build Transport request details
        Console.WriteLine("Staging Transaction{0}", Environment.NewLine)
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

        ' Initiate Transaction
        Dim transportResponse = transportSoapClient.CreateTransaction(credentialsName, credentialsSiteId, credentialsKey, transportRequest)
        Dim transportKey = transportResponse.TransportKey
        Console.WriteLine("TransportKey Received {0}{1}", transportKey, Environment.NewLine)
        ' Initiate transaction thread with received TransportKey
        Dim transactionResultTask As Task(Of Object) = GeniusRequest($"http://{ipAddress}:8080/v2/pos?TransportKey={transportKey}&Format=XML", GetType(TransactionResult))
        ' Wait 5 seconds
        Console.WriteLine("Waiting 5 seconds before canceling transaction")
        Threading.Thread.Sleep(5000)
        Dim cancelResultData As CancelResult = GeniusRequest($"http://{ipAddress}:8080/pos?Action=Cancel&Format=XML", GetType(CancelResult)).Result
        Console.WriteLine("Cancel Result: {0}", cancelResultData.Status)
        ' Wait for Transaction Results then output
        transactionResultTask.Wait()
        Console.WriteLine("Transaction Result: {0}", transactionResultTask.Result.Status)
        ' Close Application
        Console.WriteLine("Press Any Key to Close")
        Console.ReadKey()
    End Sub

    Private Async Function GeniusRequest(url As String, returnType As Type) As Task(Of Object)
        Dim transactionResultData As Object
        Using webClient = New Net.WebClient()
            Dim geniusResponse = Await webClient.OpenReadTaskAsync(url)
            'Validate XML to Genius XSD class
            Using streamReader As New IO.StreamReader(geniusResponse)
                Dim xmlSerializer As New XmlSerializer(returnType)
                transactionResultData = xmlSerializer.Deserialize(streamReader)
            End Using
        End Using
        Return transactionResultData
    End Function

End Module
