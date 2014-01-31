using UnityEngine;
using System.Collections;

public class NetItem : NetStuff {

	// Use this for initialization
	void Start()
	{
		if (name.IndexOf('(') > 0){
			name = name.Substring(0,name.IndexOf('('));
		}
	}
    
}