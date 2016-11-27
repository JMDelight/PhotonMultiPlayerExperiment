using UnityEngine;
using System.Collections;

public class Launcher : Photon.PunBehaviour {

    string _gameVersion = "1";
    public PhotonLogLevel LogLevel = PhotonLogLevel.Informational;

    bool isConnecting;

    // Use this for initialization
    void Awake()
    {
        PhotonNetwork.logLevel = LogLevel;
        PhotonNetwork.autoJoinLobby = false;

        PhotonNetwork.automaticallySyncScene = true;
    }
        
	void Start () {

    }

    public void Connect()
    {
        isConnecting = true;
        if (PhotonNetwork.connected)
        {
            // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnPhotonRandomJoinFailed() and we'll create one.
            Debug.Log("Join a room.");
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            // #Critical, we must first and foremost connect to Photon Online Server.
            PhotonNetwork.ConnectUsingSettings(_gameVersion);
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log(isConnecting);
        if (isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        Debug.Log("DemoAnimator/Launcher: OnConnectedToMaster() was called by PUN");
    }


    public override void OnDisconnectedFromPhoton()
    {
        Debug.LogWarning("DemoAnimator/Launcher: OnDisconnectedFromPhoton() was called by PUN");
    }

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        Debug.Log("Failed to join random, we crate our own one.");
        PhotonNetwork.CreateRoom(null);
    }

    public override void OnJoinedRoom()
    {
        
        Debug.Log("DemoAnimator/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");

        // #Critical: We only load if we are the first player, else we rely on  PhotonNetwork.automaticallySyncScene to sync our instance scene.
        if (PhotonNetwork.room.playerCount == 1)
        {
            Debug.Log("Yo Yo Yo, made a room.");
            PhotonNetwork.LoadLevel("Multiplayer");
        }
    }

}
