using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : Photon.MonoBehaviour {
	public GameObject Bag;
	public float rangeItem;
	protected string TxTBag;
	public Rect inv;
	public List<GameObject> lista;
	public int MaximoItem;
	private GameObject go;
	
	void Awake()
	{
		if (!photonView.isMine)
        {
            enabled = false;
        }
	}
	
	// Use this for initialization
	void Start () {
		MaximoItem = 1;
		TxTBag = "";
		rangeItem = 4;
		Bag = transform.FindChild("Inventory").gameObject;
		inv = new Rect(0f,Screen.height-100f,100f,100f);
	}
	
	// Update is called once per frame
	void Update () {

		//Debug.DrawRay(transform.position, transform.forward * rangeItem);
		if(Input.GetButtonDown("Fire1")){
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //(new Vector3(Screen.width/2,Screen.height/2,0f));
			Debug.DrawRay(Camera.main.transform.position, ray.direction*  rangeItem, Color.green);
			if(Physics.Raycast (Camera.main.transform.position,ray.direction, out hit, rangeItem)){
				Debug.Log(hit.collider.name);
				if(lista.Count < MaximoItem){
					if(hit.collider.tag == "Item"){
                        hit.collider.gameObject.SendMessage("desativeNet");
						hit.collider.transform.parent = Bag.transform;
						hit.collider.transform.position = Bag.transform.position;

                        if (hit.collider.gameObject.name == ItemEnum.Amulet.ToString() ||
						    hit.collider.gameObject.name == ItemEnum.VampirePainting.ToString() ||
						    hit.collider.gameObject.name == ItemEnum.Sarcofago.ToString())
                        {
                            gameObject.SendMessage("transformeNet");
                        }
                        else
                        {

                            TxTBag += (hit.collider.gameObject.name + "\n");
                            lista.Add(hit.collider.gameObject);
                        }
					}
				}
			}

		}
		if(Input.GetKeyDown(KeyCode.R)){
			//Debug.Log ("droping");
			for(int i=lista.Count-1; i >= 0; i--){
				go = lista[i];
				//Debug.Log(go.name);
				//go.rigidbody.AddForce(Camera.main.transform.forward*2000);
                go.SendMessage("ativeNet");
				go.transform.parent = null;
				go.transform.position = Bag.transform.position;				
				lista.RemoveAt(i);
				TxTBag = "";
			}
		}
		//lista = Bag.GetComponentsInChildren(typeof(GameObject),true) as GameObject[];
		//Debug.Log(lista.Count.ToString());
	}

	void OnGUI(){
		GUILayout.BeginArea(inv);
		GUILayout.TextArea(TxTBag,GUILayout.Width(inv.width),GUILayout.Height(inv.height));
		GUILayout.EndArea();
	}
}
