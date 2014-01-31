using UnityEngine;
using System.Collections;

public class NetPersonagem : NetStuff
{
	public static GameObject playerQuest;
	public Transform cameraPosition;
    public player myPlayer;
	public Vector3 rezPosition;
    // Update is called once per frame
    void Start()
    {
        if (photonView.isMine)
        {
            Camera.main.transform.position = cameraPosition.position;
			Camera.main.transform.parent = transform;
			Camera.main.GetComponent("MouseLook").SendMessage("Habiliteme");
            Debug.Log("mandando...");
			GameObject.Find("New Text").renderer.enabled = false;
			gameObject.GetComponent("FPSInputController").SendMessage("Habiliteme");            
			gameObject.GetComponent("MouseLook").SendMessage("Habiliteme");   
			gameObject.transform.FindChild("Sphere").renderer.enabled = false;
			playerQuest = gameObject;
		}
		myPlayer = gameObject.GetComponent<player>();
    }

    public void transformeNet()
    {
		transformacao();
		if (PhotonNetwork.connectionStateDetailed == PeerState.Joined)
		{
			myPhotonView.RPC("transformacao", PhotonTargets.Others);
		}else{
			Debug.Log("deuMerda");
		}
    }

	public void morreOmen (GameObject atacanteGo)
	{
		death (atacanteGo);
	}

	public void morreHumano (GameObject atacanteGo)
	{
		death (atacanteGo);
		player atacante = atacanteGo.GetComponent<player>();
		if (photonView.isMine)
		{
			StartCoroutine("rezz", atacante.tipo);
		}
	}

	void death(GameObject atacanteGo)
	{
		if (photonView.isMine)
		{
			Camera.main.transform.position = atacanteGo.transform.position;
			Camera.main.transform.parent = atacanteGo.transform;
		}
		rezPosition = new Vector3(transform.position.x,transform.position.y+0.1f,transform.position.z);
		desative();
		myPhotonView.RPC("desative", PhotonTargets.Others);
	}

	IEnumerator rezz(tipoPlayer tipo)
	{
		yield return new WaitForSeconds(5);
		transform.position = rezPosition;
		Camera.main.transform.position = transform.position;
		Camera.main.transform.parent = transform;
		ative();
		myPhotonView.RPC("ative", PhotonTargets.Others);

		transformeNet();
	}

    [RPC]
    public void transformacao()
    {
        myPlayer.transforme(tipoPlayer.vampiro);
    }
    
}
