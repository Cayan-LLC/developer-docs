<?php
    header("Content-Type: application/json");
    $client = new SoapClient("https://ps1.merchantware.net/Merchantware/ws/RetailTransaction/v45/Credit.asmx?WSDL");
    $response = $client->SettleBatch(
        array (
            "Credentials" => array (
                "MerchantName"   => "TEST MERCHANT",
                "MerchantSiteId" => "XXXXXXXX",
                "MerchantKey"    => "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX"
                )
            )
        );
    print_r(json_encode($response->SettleBatchResult));
?>