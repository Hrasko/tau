using UnityEngine;
using System.Collections;

public class player_attack : MonoBehaviour
{
    Ray ray;
    public float distance;
	public bool ataqueHabilitado = false;
    // Use this for initialization
    void Start()
    {
        distance = 1.0f;
    }

	string debugador = "";

	void OnTriggerStay(Collider other)
	{
		if (other.name != transform.parent.name && other.tag == "Player")
		{
			Debug.Log(name + " encostando em " + other.name);
			if (Input.GetButtonDown("Fire1"))
			{
				other.gameObject.transform.FindChild("Attack").SendMessage("MataPersonagem",gameObject.transform.parent.gameObject);
			}
		}
	}

    void MataPersonagem(GameObject defensorGO)
    {
		GameObject atacanteGo = gameObject.transform.parent.gameObject;
		Inventory inventorioAtacante = atacanteGo.GetComponent<Inventory>();
		player atacante = atacanteGo.GetComponent<player>();
        player defensor = defensorGO.GetComponent<player>();

		debugador = "atacante: "; 
		debugador += atacante.tipo.ToString() ;
		debugador += atacanteGo.name + " defensor: " ;
		debugador += defensor.tipo.ToString() ;
		debugador += defensorGO.name;

        if (atacante.tipo == tipoPlayer.vampiro || atacante.tipo == tipoPlayer.fantasma)
        {
            if (defensor.tipo == tipoPlayer.humano)
            {
				defensor.SendMessage("morreHumano",atacanteGo);
            }
        }
        else
        {
            string arma = "mao";
            if (inventorioAtacante.lista.Count > 0)
            {
                arma = inventorioAtacante.lista[0].name;
            }
			debugador += " arma: " + arma;
            if (defensor.tipo == tipoPlayer.vampiro)
            {
                if (arma == ItemEnum.Garlic.ToString() ||
                    arma == ItemEnum.HolyWater.ToString() ||
                    arma == ItemEnum.WoodenStake.ToString())
                {
					defensor.SendMessage("morreOmen",atacanteGo);
                }
            }
            else if (defensor.tipo == tipoPlayer.fantasma)
            {
                if (arma == ItemEnum.Crowbar.ToString() ||
                    arma == ItemEnum.HolyWater.ToString() ||
                    arma == ItemEnum.OldTalisman.ToString())
                {
					defensor.SendMessage("morreOmen",atacanteGo);
                }
            }
        }
		Debug.Log(debugador);
    }

	void OnGUI()
	{
		GUILayout.BeginArea(new Rect(0f,Screen.height-60f,Screen.width,50f));
		GUILayout.Box(debugador);
		GUILayout.EndArea();
	}

}
