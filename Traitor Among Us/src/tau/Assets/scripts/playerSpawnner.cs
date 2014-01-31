using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playerSpawnner : Photon.MonoBehaviour {

	public Transform playerPrefab;

    void OnJoinedRoom()
    {
        Spawnplayer();
    }
    static int x = 0;
    void Spawnplayer()
    {
        Vector3 pos = transform.position + new Vector3(Random.Range(-3,3),0,Random.Range(-3,3));
       GameObject go = PhotonNetwork.Instantiate(playerPrefab.name, pos, transform.rotation, 0);
       x++;
       go.name = "Player" + x.ToString();
       
    }


    void OnPhotonPlayerDisconnected(PhotonPlayer player)
    {
        Debug.Log("Clean up after player " + player);
    }

    void OnDisconnectedFromPhoton()
    {
        Debug.Log("Clean up a bit after server quit");
        
        /* 
        * To reset the scene we'll just reload it:
        */
        Application.LoadLevel(Application.loadedLevel);
    }
}
