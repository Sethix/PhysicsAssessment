using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetManager : NetworkManager {

    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        base.OnClientSceneChanged(conn);
        GameObject plr = Instantiate(playerPrefab);
        playerPrefab.transform.position = startPositions[Random.Range(0, startPositions.Count - 1)].position;
        NetworkServer.AddPlayerForConnection(conn, playerPrefab, 0);
    }
}
