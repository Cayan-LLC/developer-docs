Imports System.Xml.Serialization
Module Module1

    Sub Main()
        Dim ipAddress = "192.168.0.123"
        ' Request Status of Device
        Using webClient = New Net.WebClient()
            Dim geniusResponse = webClient.OpenRead($"http://{ipAddress}:8080/v2/pos?Action=Status&Format=XML")
            'Validate XML to Genius XSD class
            Using streamReader As New IO.StreamReader(geniusResponse)
                Dim xmlSerializer As New XmlSerializer(GetType(StatusResult))
                Dim statusResult As StatusResult = xmlSerializer.Deserialize(streamReader)
                Console.WriteLine("Status: {0}", statusResult.Status)
                Console.WriteLine("Current Screen: {0}", statusResult.CurrentScreen)
                Console.WriteLine("Response Message: {0}", statusResult.ResponseMessage)
                Console.WriteLine("Serial Number: {0}", statusResult.SerialNumber)
                Console.WriteLine("Application Version: {0}", statusResult.ApplicationVersion)
            End Using
        End Using
        Console.WriteLine("Press Any Key to Close")
        Console.ReadKey()
    End Sub

End Module
