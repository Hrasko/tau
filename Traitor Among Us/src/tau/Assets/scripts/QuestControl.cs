using UnityEngine;
using System.Collections;

public class QuestControl : MonoBehaviour {
	public QuestDB qdb;
	public string text;
	public float rangeItem;
	public Rect TxtBox;
	private bool achouItem;
	public int atualQuest;

	// Use this for initialization
	void Start () {
		atualQuest = -1;
		TxtBox = new Rect(0f,0f,Screen.width,50f);
		text = (qdb.quests[0]).quest[0];
		rangeItem = 5;
		achouItem = false;
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2,0f));
		//Debug.DrawRay(transform.position, transform.forward * rangeItem);
		if(Physics.Raycast (ray, out hit, rangeItem)){
			if(hit.collider.tag == "Item"){
				for(int i = 1; i < this.qdb.quests.Length ; i++){
					for(int j = 0; j < this.qdb.quests[i].startItem.Length ; j++){
						if(hit.collider.gameObject.name == this.qdb.quests[i].startItem[j].objeto.ToString()){
							if (hit.collider.gameObject.name != ItemEnum.IronKey.ToString()){
								NetPersonagem.playerQuest.SendMessage("transformeNet");
							}

							if(!achouItem){
								if (!this.qdb.quests[i].startItem[j].ativado){
									text = "";
									this.qdb.quests[i].startItem[j].ativado = true;
									foreach(string s in this.qdb.quests[i].quest){
										text += (s + "\n");
									}
									atualQuest = i;
									achouItem = true;
									break;
								}
							}
						}
					}
					if(achouItem) break;
				}
				if(atualQuest != -1){
					for(int j = 0; j < this.qdb.quests[atualQuest].objective.Length ; j++){
                        if (hit.collider.gameObject.name == this.qdb.quests[atualQuest].objective[j].objeto.ToString())
                        {
							if(this.qdb.quests[atualQuest].objective[j].quantidade <= 1){ // 1 se refere ao total do item que o player contem
								this.qdb.quests[atualQuest].objective[j].completado = true;
								achouItem = false;
								break;
							}
						}
					}
				}
			}
		}
	}

	void OnGUI(){
		GUILayout.BeginArea(TxtBox);
		GUILayout.Box(text);
		GUILayout.EndArea();
	}
}
