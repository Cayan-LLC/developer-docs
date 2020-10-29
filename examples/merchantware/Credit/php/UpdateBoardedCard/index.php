<?php
    header("Content-Type: application/json");
    $client = new SoapClient("https://ps1.merchantware.net/Merchantware/ws/RetailTransaction/v45/Credit.asmx?WSDL");
    $response = $client->UpdateBoardedCard(
        array (
            "Credentials" => array (
                "MerchantName"   => "TEST MERCHANT",
                "MerchantSiteId" => "XXXXXXXX",
                "MerchantKey"    => "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX"
                ),
            "Request" => array (
                "VaultToken"     => "100000100ABCDE123456",
                "ExpirationDate" => "1221"
                )
            )
        );
    print_r(json_encode($response->UpdateBoardedCardResult));
?>