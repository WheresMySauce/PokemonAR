using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mirror;

[DisallowMultipleComponent]
//[AddComponentMenu("Network/Network Manager HUD")]
//[RequireComponent(typeof(NetworkManager))]
public class SceneSwitcher : MonoBehaviour
{
    public NetworkManager manager;
    private string inputIP;
    public InputField IP;
    //void Awake()
    //{
    //    manager = GetComponent<NetworkManager>();
    //}
    public void playGame()
    {

        if (!NetworkClient.active)
        {
            if (Application.platform != RuntimePlatform.WebGLPlayer)
            {
                manager.StartHost();
            }
        }
        //if (NetworkClient.isConnected && !NetworkClient.ready)
        //{
        //        NetworkClient.Ready();
        //        if (NetworkClient.localPlayer == null)
        //        {
        //            NetworkClient.AddPlayer();
        //        }
        //}
        //SceneManager.LoadScene("Sample Scene");

    }
    public void joinIP()
    {
        if (!NetworkClient.active)
        {
            if (Application.platform != RuntimePlatform.WebGLPlayer)
            {
                Debug.Log(inputIP);
                manager.networkAddress = inputIP;
                manager.StartClient();
            }
        }
        //SceneManager.LoadScene("Sample Scene");
    }
    //public void Back()
    //{
    //    SceneManager.LoadScene("Main Menu Scene");
    //}
    public void Quit()
    {
        Debug.Log("QUIT");
        Application.Quit();    
    }
    public void ReadIP(string s)
    {
        inputIP = IP.text;
    }
    public void StopServer()
    {
        if (NetworkServer.active && NetworkClient.isConnected)
        {
            manager.StopHost();
        }
        // stop client if client-only
        else if (NetworkClient.isConnected)
        {
            manager.StopClient();
        }
        
    }
}

