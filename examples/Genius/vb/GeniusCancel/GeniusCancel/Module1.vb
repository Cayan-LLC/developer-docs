Imports System.Xml.Serialization
Module Module1

    Sub Main()
        Dim ipAddress = "192.168.0.123"
        ' Initiate Cancel
        Using webClient = New Net.WebClient()
            Dim geniusResponse = webClient.OpenRead($"http://{ipAddress}:8080/pos?Action=Cancel&Format=XML")
            'Validate XML to Genius XSD class
            Using streamReader As New IO.StreamReader(geniusResponse)
                Dim xmlSerializer As New XmlSerializer(GetType(CancelResult))
                Dim statusResult As CancelResult = xmlSerializer.Deserialize(streamReader)
                Console.WriteLine("Status: {0}", statusResult.Status)
                Console.WriteLine("Response Message: {0}", statusResult.ResponseMessage)
            End Using
        End Using
        Console.WriteLine("Press Any Key to Close")
        Console.ReadKey()
    End Sub

End Module
