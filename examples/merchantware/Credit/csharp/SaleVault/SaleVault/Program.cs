using System;
using SaleVault.MWCredit45;

namespace SaleVault
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
                EnablePartialAuthorization = "true",
                ForceDuplicate = "true"
            };
            // Run Sale
            TransactionResponse45 transactionResponse45 = soapClient.Sale(merchantCredentials, paymentData, saleRequest);
            // Print Results
            Console.WriteLine("Sale Response: {0} Token: {1} Amount: ${2}", transactionResponse45.ApprovalStatus, transactionResponse45.Token, transactionResponse45.Amount);
            Console.WriteLine("Press Any Key to Close");
            Console.ReadKey();
        }
    }
}
