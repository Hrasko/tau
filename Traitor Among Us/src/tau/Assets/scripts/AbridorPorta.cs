using UnityEngine;
using System.Collections;

public class AbridorPorta : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction, Color.red);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 3))
            {
                Debug.Log(hit.collider.name);
				if (hit.collider.gameObject.tag == "porta" || hit.collider.gameObject.tag == "portaFinal")
                {
                    hit.collider.gameObject.SendMessage("OpenDoor", gameObject);
                }
            }
        }
    }

    public void teleporte(Transform target)
    {
		transform.position = target.position;
		transform.rotation = target.rotation;
    }

}
