using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using GeniusThreadedSample.TransportWeb;

namespace GeniusThreadedSample
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
            Task<object> transactionResultTask = GeniusRequest($"http://{ipAddress}:8080/v2/pos?TransportKey={transportKey}&Format=XML", typeof(TransactionResult));
            // Wait 5 seconds
            Console.WriteLine("Waiting 5 seconds before canceling transaction");
            Thread.Sleep(5000);
            CancelResult cancelResult = (CancelResult)GeniusRequest($"http://{ipAddress}:8080/pos?Action=Cancel&Format=XML", typeof(CancelResult)).Result;
            Console.WriteLine("Cancel Result: {0}", cancelResult.Status);
            // Wait for Transaction Results then output
            transactionResultTask.Wait();
            TransactionResult transactionResult = (TransactionResult)transactionResultTask.Result;
            Console.WriteLine("Transaction Result: {0}", transactionResult.Status);
            Console.WriteLine("Amount: {0}", transactionResult.AmountApproved);
            Console.WriteLine("AuthCode: {0}", transactionResult.AuthorizationCode);
            Console.WriteLine("Token: {0}", transactionResult.Token);
            Console.WriteLine("Account Number: {0}", transactionResult.AccountNumber);

            // Close application
            Console.WriteLine("Press Any Key to Close");
            Console.ReadKey();
        }

        private static async Task<object> GeniusRequest(string url, Type resultType)
        {
            WebRequest webReq = WebRequest.Create(url);
            using (WebResponse webResp = await webReq.GetResponseAsync())
            {
                using (Stream responseStream = webResp.GetResponseStream())
                {
                    // Validate XML to Genius XSD class
                    StreamReader streamReader = new StreamReader(responseStream);
                    XmlSerializer xmlSerializer = new XmlSerializer(resultType);
                    object transactionResult = xmlSerializer.Deserialize(streamReader);
                    return transactionResult;
                }
            }
        }
    }
}