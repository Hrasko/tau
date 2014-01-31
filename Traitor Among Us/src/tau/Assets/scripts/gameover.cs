using UnityEngine;
using System.Collections;

public class gameover : Photon.MonoBehaviour {

	string gameovermsg = "";
	Rect TxtBox;
	// Use this for initialization
	void Start () {
		TxtBox = new Rect(0f,Screen.height -50f,Screen.width,50f);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI()
	{
		if (gameovermsg != ""){
			GUILayout.BeginArea(TxtBox);
			GUILayout.Box(gameovermsg);
			GUILayout.EndArea();
		}
	}
}
