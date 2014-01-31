using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GeradorSala : MonoBehaviour
{

    public GameObject[] salasPrefab;
    //public GameObject[] itensPrefab;
    public List<ItemEnum> filaObjetos;
    public int indiceFilaObjetos = 0;
	public Queue<GameObject> possiveisTeleportes = new Queue<GameObject>();
	public Queue<GameObject> portasSemDono = new Queue<GameObject>();
    public int maxComodos;
    public System.Random random = new System.Random();

	void Awake()
    {
        PhotonNetwork.ConnectUsingSettings("1.0");
    }
	
    // Use this for initialization
    void Start()
    {
		GameObject[] portas = GameObject.FindGameObjectsWithTag("porta");
		Debug.Log("achei portas " + portas.Length);
		int[] shuffle = new int[portas.Length];

		for (int i = 0; i < shuffle.Length; i++) {
			shuffle[i] = i;
		}

		int n = portas.Length;  
		while (n > 1) {  
			n--;  
			int k = random.Next(n + 1);  
			int value = shuffle[k];  
			shuffle[k] = shuffle[n];  
			shuffle[n] = value;  
		}

		for (int i = 0; i < portas.Length; i++) {
			portasSemDono.Enqueue(portas[shuffle[i]]);
		}

		while (portasSemDono.Count > 1)
		{
			setarTeleportePortas(portasSemDono.Dequeue(),portasSemDono.Dequeue());
		}
		if (portasSemDono.Count > 0)
		{
			setarTeleportePortas(portasSemDono.Dequeue(),portas[random.Next(portas.Length)]);
		}
    }

	void setarTeleportePortas(GameObject portaA, GameObject portaB)
	{
		Debug.Log(portaA.transform.parent.name);
		Debug.Log(portaB.transform.parent.name);
		GameObject teleporteA = portaA.transform.FindChild("Teleport").gameObject;
		GameObject teleporteB = portaB.transform.FindChild("Teleport").gameObject;
		
		portaA.SendMessage("setTeleport",teleporteB);
		portaB.SendMessage("setTeleport",teleporteA);
	}

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.isMasterClient)
        {
            spawnObjetos();
        }
    }
	
	private bool receivedRoomList = false;

    void OnConnectedToPhoton()
    {
        StartCoroutine(JoinOrCreateRoom());
    }

    void OnDisconnectedFromPhoton()
    {
        receivedRoomList = false;
    }
	
	void OnGUI()
    {
        GUILayout.Label("v0.0.3");
        //Check connection state..
        if (PhotonNetwork.connectionState == ConnectionState.Disconnected)
        {
            //We are currently disconnected
            GUILayout.Label("Connection status: Disconnected");

            GUILayout.BeginVertical();
            if (GUILayout.Button("Connect"))
            {
                //Connect using the PUN wizard settings (Self-hosted server or Photon cloud)
                PhotonNetwork.ConnectUsingSettings("1.0");
            }
            GUILayout.EndVertical();
        }
        else
        {
            //We're connected!
            if (PhotonNetwork.connectionState == ConnectionState.Connected)
            {
                GUILayout.Label("Connection status: Connected");
                if (PhotonNetwork.room != null)
                {
                    GUILayout.Label("Room: " + PhotonNetwork.room.name);
                    GUILayout.Label("Players: " + PhotonNetwork.room.playerCount + "/" + PhotonNetwork.room.maxPlayers);

                }
                else
                {
                    GUILayout.Label("Not inside any room");
                }

                GUILayout.Label("Ping to server: " + PhotonNetwork.GetPing());
            }
            else
            {
                //Connecting...
                GUILayout.Label("Connection status: " + PhotonNetwork.connectionState);
            }
        }
        
    }

	
	IEnumerator JoinOrCreateRoom()
    {
        float timeOut = Time.time + 2;
        while (Time.time < timeOut && !receivedRoomList)
        {
            yield return 0;
        }
        //We still didn't join any room: create one
        if (PhotonNetwork.room == null){
            string roomName = "TestRoom"+Application.loadedLevelName;
            PhotonNetwork.CreateRoom(roomName, true, true, 4);
        }
    }
    
    /// <summary>
    /// Not used in this script, just to show how list updates are handled.
    /// </summary>
    void OnReceivedRoomListUpdate()
    {
        Debug.Log("We received a room list update, total rooms now: " + PhotonNetwork.GetRoomList().Length);

        string wantedRoomName = "TestRoom" + Application.loadedLevelName;
        foreach (RoomInfo room in PhotonNetwork.GetRoomList())
        {
            if (room.name == wantedRoomName)
            {
                PhotonNetwork.JoinRoom(room.name);
                break;
            }
        }
        receivedRoomList = true;
    }

    public void prepararSpawnObjetos()
    {
        
    }

    public void spawnObjetos()
    {
        while (PhotonNetwork.room != null && indiceFilaObjetos < filaObjetos.Count)
        {
            ItemEnum item = filaObjetos[indiceFilaObjetos];
            //tipoObj[] tipos = ItemSage.tipos(item);
            //tipoObj tipo = tipos [random.Next(tipos.Length)];
			//Debug.Log(tipo.ToString());
            //List<int> lista = ObjectSpawn.dicionario[tipo];
            //ObjectSpawn spawn = ObjectSpawn.all[ lista[random.Next(lista.Count)] ];

			ObjectSpawn spawn = ObjectSpawn.all[ random.Next(ObjectSpawn.all.Count)];
            //spawn.spawn(itensPrefab[ItemSage.indicePrefab(item)],item.ToString());
			spawn.spawn(item.ToString());
            indiceFilaObjetos++;
        }
    }


	/*
    public void IniciarSpawnSala()
    {
        GameObject atual = Instantiate(salasPrefab[0], this.transform.position, Quaternion.identity) as GameObject;
        Sala salaAtual = atual.GetComponent<Sala>();
        for (int i = 0; i < salaAtual.salaSpawnners.Length; i++)
        {
            possiveisLocaisSala.Enqueue(salaAtual.salaSpawnners[i]);
        }
    }

    public void spawnMaisSalas()
    {
        if (Input.GetKeyDown(KeyCode.Space) && possiveisLocaisSala.Count > 0 && Sala.all.Count < maxComodos)
        {
            SalaSpawn spawnAtual = possiveisLocaisSala.Dequeue();
            if (spawnAtual.podeSpawnnar)
            {
                GameObject atual = Instantiate(salasPrefab[spawnAtual.sorteiaSala()], spawnAtual.gameObject.transform.position, Quaternion.identity) as GameObject;
                
                Sala salaAtual = atual.GetComponent<Sala>();
                for (int i = 0; i < salaAtual.salaSpawnners.Length; i++)
                {
                    possiveisLocaisSala.Enqueue(salaAtual.salaSpawnners[i]);
                }
            }
            else
            {
                spawnAtual.desativeSpawnner();
            }
        }
    }
	*/
}


