using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class Web3PrivateKeySendTransactionExample : MonoBehaviour
{
    async public void OnSendTransaction()
    {
        // private key of account
        string privateKey = "28214266cd14356370129e8ba1b416155673b8b38db6c42dadec1367523daa62";
        // set chain: ethereum, moonbeam, polygon etc
        string chain = "ethereum";
        // set network mainnet, testnet
        string network = "rinkeby";
        // account of player        
        string account = Web3PrivateKey.Address(privateKey);
        // account to send to
        string to = "0x42C621FB8b8524Bb96A6cB3B49244a7ff4d93024";
        // value in wei
        string value = "1000000000000000000";
        // optional rpc url
        string rpc = "";

        string chainId = await EVM.ChainId(chain, network, rpc);
        string gasPrice = await EVM.GasPrice(chain, network, rpc);
        string data = "";
        string gasLimit = "21000";
        string transaction = await EVM.CreateTransaction(chain, network, account, to, value, data, gasPrice, gasLimit, rpc);
        string signature = Web3PrivateKey.SignTransaction(privateKey, transaction, chainId);
        string response = await EVM.BroadcastTransaction(chain, network, account, to, value, data, signature, gasPrice, gasLimit, rpc);
        print(response);
        Application.OpenURL("https://rinkeby.etherscan.io/tx/" + response);
    }
}
