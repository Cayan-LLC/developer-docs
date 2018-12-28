Imports System.Xml.Serialization
Imports GeniusKeyedEntry.TransportService
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
        Dim keyedEntryResult = InitiateKeyedEntry(ipAddress)
        Console.WriteLine("InitiateKeyedEntry Result: {0}", keyedEntryResult)
        ' Wait for Transaction Results then output
        transactionResultTask.Wait()
        Dim transacitonResultData = transactionResultTask.Result
        Console.WriteLine("Transaction Result: {0}", transacitonResultData.Status)
        Console.WriteLine("Amount: {0}", transacitonResultData.AmountApproved)
        Console.WriteLine("AuthCode: {0}", transacitonResultData.AuthorizationCode)
        Console.WriteLine("Token: {0}", transacitonResultData.Token)
        Console.WriteLine("Account Number: {0}", transacitonResultData.AccountNumber)
        ' Close Application
        Console.WriteLine("Press Any Key to Close")
        Console.ReadKey()
    End Sub

    Private Function InitiateKeyedEntry(ipAddress As String) As String
        Dim onSaleScreen = False
        Dim attempts = 0
        Do
            Threading.Thread.Sleep(1000)
            Console.WriteLine("Sending Status Request.")
            Dim statusResultData As StatusResult = GeniusRequest($"http://{ipAddress}:8080/pos?Action=Status&Format=XML", GetType(StatusResult)).Result
            If (statusResultData.CurrentScreen = "02" Or statusResultData.CurrentScreen = "03") Then
                Console.WriteLine("Terminal is ready for KeyedEntry")
                onSaleScreen = True
            Else
                Console.WriteLine($"Terminal is not Ready. CurrentScreen {statusResultData.CurrentScreen}. Waiting 1 second before trying again.")
            End If
            attempts += 1
        Loop While Not onSaleScreen And attempts < 5

        If onSaleScreen Then
            Dim keyedEntryResultData As InitiateKeyedEntryResult = GeniusRequest($"http://{ipAddress}:8080/pos?Action=InitiateKeyedEntry&Format=XML", GetType(InitiateKeyedEntryResult)).Result
            Return keyedEntryResultData.Status
        Else
            Return "Max attempts reached."
        End If
    End Function

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
