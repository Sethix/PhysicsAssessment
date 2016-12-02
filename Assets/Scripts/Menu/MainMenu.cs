using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {

    InputField ipText;

    NetworkManager netManager;

    void Start()
    {
        ipText = FindObjectOfType<InputField>();
        netManager = FindObjectOfType<NetworkManager>();
    }

    public void Host()
    {
        netManager.networkAddress = ipText.text;
        netManager.networkPort = 7777;
        netManager.StartHost();
    }

    public void Connect()
    {
        netManager.networkAddress = ipText.text;
        netManager.networkPort = 7777;
        netManager.StartClient();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
