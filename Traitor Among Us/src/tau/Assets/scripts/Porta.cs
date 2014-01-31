using UnityEngine;
using System.Collections;

public class Porta : MonoBehaviour
{

    public GameObject entrada, saida;

    bool doorIsOpen = false;
    float doorTimer = 0.0f;
    public float doorOpenTime = 3.0f;
    public AudioClip doorOpenSound;
    public AudioClip doorShutSound;

    void Start()
    {
        doorTimer = 0.0f;
    }

    void Update()
    {
        if (doorIsOpen)
        {
            doorTimer += Time.deltaTime;
            if (doorTimer > doorOpenTime)
            {
                Door(doorShutSound, false, "doorshut");
                doorTimer = 0.0f;
            }
        }
    }

    void OpenDoor(GameObject opener)
    {
        if (!doorIsOpen)
        {
            Door(doorOpenSound, true, "dooropen");
            opener.SendMessage("setFinalTarget", saida.transform.position);
            opener.SendMessage("setFirstTarget", entrada.transform.position);
        }
    }

    void Door(AudioClip aClip, bool openCheck, string animName)
    {
        //audio.PlayOneShot(aClip);
        doorIsOpen = openCheck;
        transform.Translate(0, 5, 0);
        //transform.parent.gameObject.animation.Play(animName);
    }
}


