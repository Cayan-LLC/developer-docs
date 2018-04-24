<?php
    header("Content-Type: application/json");
    $client = new SoapClient("https://ps1.merchantware.net/Merchantware/ws/RetailTransaction/v45/Credit.asmx?WSDL");
    $response = $client->Sale(
        array (
            "Credentials" => array (
                "MerchantName"   => "TEST MERCHANT",
                "MerchantSiteId" => "XXXXXXXX",
                "MerchantKey"    => "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX"
                ),
            "PaymentData" => array (
                "Source"     => "Vault",
                "VaultToken" => "100000100ABCDE123456",
                ),
            "Request" => array (
                "Amount"                     => "1.01",
				"TaxAmount"                  => "0.10",
                "InvoiceNumber"              => "INV1234",
                "CardAcceptorTerminalId"     => "01",
                "CustomerCode"               => "1234",
                "PurchaseOrderNumber"        => "PO1234",
                "EnablePartialAuthorization" => "true"
                )
            )
        );
    print_r(json_encode($response->SaleResult));
?>