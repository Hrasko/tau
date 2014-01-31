using UnityEngine;
using System.Collections;

[System.Serializable]
public class Quest{
	public string name;
	public int id;
	public string type;
	public string[] quest;
	public string[] passiveQuest;
	public Objective[] objective;
	public StartItem[] startItem;
}
