using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {
	public GameObject teleporteAqui;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OpenDoor(GameObject teleporter)
	{
		teleporter.SendMessage("teleporte",teleporteAqui.transform);
	}

	public void setTeleport(GameObject teleporte)
	{
		teleporteAqui = teleporte;
	}

}
