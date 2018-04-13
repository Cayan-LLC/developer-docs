using System;
using MWSaleVault.MWCredit;

namespace MWSaleVault
{
    public class Program
    {
        private static void Main(string[] args)
        {
            // Create Soap Client
            CreditSoapClient soapClient = new CreditSoapClient("CreditSoap");
            // Create MerchantCredentails object
            MerchantCredentials merchantCredentials = new MerchantCredentials
            {
                MerchantName = "TEST MERCHANT",
                MerchantSiteId = "XXXXXXXX",
                MerchantKey = "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX"
            };
            // Create PaymentData object
            PaymentData paymentData = new PaymentData
            {
                Source = "Vault",
                VaultToken = "OTT_LT1KSFBNOZB123456A"
            };
            // Create SaleRequest Object
            SaleRequest saleRequest = new SaleRequest
            {
                Amount = "1.01",
                TaxAmount = "0.10",
                InvoiceNumber = "INV1234",
                CardAcceptorTerminalId = "01",
                CustomerCode = "1234",
                PurchaseOrderNumber = "PO1234",
                EnablePartialAuthorization = "true"
            };
            // Run Sale
            TransactionResponse45 saleResponse = soapClient.Sale(merchantCredentials, paymentData, saleRequest);
            // Print Results
            Console.WriteLine("Sale Response: {0}", saleResponse.ApprovalStatus);
            Console.WriteLine("Amount: ${0}", saleResponse.Amount);
            Console.WriteLine("Token: {0}", saleResponse.Token);
            Console.WriteLine("Press Any Key to Close");
            Console.ReadKey();
        }
    }
}
