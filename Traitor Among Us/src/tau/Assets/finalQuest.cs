using UnityEngine;
using System.Collections;

public class finalQuest : MonoBehaviour {

	public bool youwin;

	public void OpenDoor(GameObject teleporter)
	{
		Debug.Log ("woooo");
		Inventory inventorio = teleporter.GetComponent<Inventory>();

		if (inventorio.lista.Count>0 && inventorio.lista[0].name == ItemEnum.IronKey.ToString())
		{
			Debug.Log ("youwin");
			youwin = true;
		}
	}


	void OnGUI()
	{
		if (youwin){
			GUI.Label(new Rect(Screen.width/2,Screen.height/2,200,200),"YOU WIN");
		}
	}
}
