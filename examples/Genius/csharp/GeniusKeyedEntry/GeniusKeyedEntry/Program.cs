using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using GeniusKeyedEntry.TransportWeb;

namespace GeniusKeyedEntry
{
    public class Program
    {
        private static void Main(string[] args)
        {
            // Declare credentials to be used with the Stage Transaction Request
            string credentialsName = "TEST MERCHANT";
            string credentialsSiteID = "XXXXXXXX";
            string credentialsKey = "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX";
            string ipAddress = "192.168.0.123";

            // Build Transport request details
            Console.WriteLine("Staging Transaction{0}", Environment.NewLine);
            TransportServiceSoapClient transportServiceSoapClient = new TransportServiceSoapClient();
            TransportRequest transportRequest = new TransportRequest
            {
                TransactionType = "sale",
                Amount = 1.01M,
                ClerkId = "1",
                OrderNumber = "1126",
                Dba = "Test DBA",
                SoftwareName = "Test Software",
                SoftwareVersion = "1.0",
                TransactionId = "102911",
                TerminalId = "01",
                PoNumber = "PO12345",
                ForceDuplicate = true,
            };

            // Stage Transaction
            TransportResponse transportResponse = transportServiceSoapClient.CreateTransaction(credentialsName, credentialsSiteID, credentialsKey, transportRequest);
            string transportKey = transportResponse.TransportKey;
            Console.WriteLine("TransportKey Received = {0}{1}", transportKey, Environment.NewLine);
            // Initiate transaction thread with received TransportKey
            Task<TransactionResult> transactionResult = GeniusSale($"http://{ipAddress}:8080/v2/pos?TransportKey={transportKey}&Format=XML");
            string keyedEntryResult = InitiateKeyedEntry(ipAddress);
            Console.WriteLine("InitiateKeyedEntry Result: {0}", keyedEntryResult);
            transactionResult.Wait();
            Console.WriteLine("Transaction Result: {0}", transactionResult.Result.Status);

            // Close application
            Console.WriteLine("Press Any Key to Close");
            Console.ReadKey();
        }

        private static string InitiateKeyedEntry(string ipAddress)
        {
            bool onSaleScreen = false;
            int attempts = 0;
            do
            {
                // Wait 1 Second then get the current screen
                Thread.Sleep(1000);
                Console.WriteLine("Sending Status Request.");
                StatusResult statusResult = GeniusStatus($"http://{ipAddress}:8080/v2/pos?Action=Status&Format=XML");
                // Check the current screen and move on if we are on screen 02 or 03
                if (statusResult.CurrentScreen == "02" || statusResult.CurrentScreen == "03")
                {
                    Console.WriteLine("Terminal is ready for KeyedEntry");
                    onSaleScreen = true;
                }
                else
                {
                    Console.WriteLine("Terminal is not Ready. CurrentScreen {0}. Waiting 1 second before trying again.", statusResult.CurrentScreen);
                }
                ++attempts;
            }
            while (!onSaleScreen && attempts < 5);

            if (onSaleScreen)
            {
                InitiateKeyedEntryResult keyedEntryResult = GeniusKeyedEntry($"http://{ipAddress}:8080/pos?Action=InitiateKeyedEntry&Format=XML");
                return keyedEntryResult.Status;
            }
            else
            {
                return "Max attempts reached.";
            }
        }

        private static async Task<TransactionResult> GeniusSale(string url)
        {
            WebRequest webReq = WebRequest.Create(url);
            using (WebResponse webResp = await webReq.GetResponseAsync())
            {
                using (Stream responseStream = webResp.GetResponseStream())
                {
                    // Validate XML to Genius XSD class
                    StreamReader streamReader = new StreamReader(responseStream);
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(TransactionResult));
                    TransactionResult transactionResult = (TransactionResult)xmlSerializer.Deserialize(streamReader);
                    return transactionResult;
                }
            }
        }

        private static StatusResult GeniusStatus(string url)
        {
            WebRequest webReq = WebRequest.Create(url);
            using (WebResponse webResp = webReq.GetResponse())
            {
                using (Stream responseStream = webResp.GetResponseStream())
                {
                    // Validate XML to Genius XSD class
                    StreamReader streamReader = new StreamReader(responseStream);
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(StatusResult));
                    StatusResult statusResult = (StatusResult)xmlSerializer.Deserialize(streamReader);
                    return statusResult;
                }
            }
        }

        private static InitiateKeyedEntryResult GeniusKeyedEntry(string url)
        {
            WebRequest webReq = WebRequest.Create(url);
            using (WebResponse webResp = webReq.GetResponse())
            {
                using (Stream responseStream = webResp.GetResponseStream())
                {
                    // Validate XML to Genius XSD class
                    StreamReader streamReader = new StreamReader(responseStream);
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(InitiateKeyedEntryResult));
                    InitiateKeyedEntryResult keyedEntryResult = (InitiateKeyedEntryResult)xmlSerializer.Deserialize(streamReader);
                    return keyedEntryResult;
                }
            }
        }
    }
}