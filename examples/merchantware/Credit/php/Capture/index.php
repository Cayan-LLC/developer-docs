<?php
    header("Content-Type: application/json");
    $client = new SoapClient("https://ps1.merchantware.net/Merchantware/ws/RetailTransaction/v45/Credit.asmx?WSDL");
    $response = $client->Capture(
        array (
            "Credentials" => array (
                "MerchantName"   => "TEST MERCHANT",
                "MerchantSiteId" => "XXXXXXXX",
                "MerchantKey"    => "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX"
                ),
            "Request" => array (
                "Token"                     => "1234567890",
                "Amount"                    => "1.01",
                "InvoiceNumber"             => "INV1234",
                "CardAcceptorTerminalId"    => "01"
                )
            )
        );
    print_r(json_encode($response->CaptureResult));
?>