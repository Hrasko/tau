using UnityEngine;
using System.Collections;

public enum tipoPlayer
{
    humano,vampiro,fantasma
}

public class player : MonoBehaviour
{
    public tipoPlayer tipo = tipoPlayer.humano;

    public Renderer myRender;

    public Material[] materiais;

	public static 

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        switch (tipo)
        {
            case tipoPlayer.vampiro: myRender.material = materiais[1]; break;
            case tipoPlayer.fantasma: myRender.material = materiais[2]; break;
            default: myRender.material = materiais[0]; break;
        }
    }

    public void transforme(tipoPlayer vireIsso_)
    {
        tipo = vireIsso_;

    }

	/*
    void MataPersonagem(GameObject atacanteGO)
    {
        Inventory inventorioAtacante = atacanteGO.GetComponent<Inventory>();
        player atacante = atacanteGO.GetComponent<player>();
        Debug.Log(name + ": fui atacado" );
        morre(atacanteGO);

        /*
        string arma = "mao";
        if (inventorioAtacante.lista.Count > 0)
        {
            arma = inventorioAtacante.lista[0].name;
        }
        
        if (atacante.tipo == tipoPlayer.vampiro || atacante.tipo == tipoPlayer.fantasma)
        {
            morreHumano(atacante.tipo, atacanteGO);
        }
        else if (tipo == tipoPlayer.vampiro)
        {
            if (arma == ItemEnum.Garlic.ToString() ||
                arma == ItemEnum.HolyWater.ToString() ||
                arma == ItemEnum.WoodenStake.ToString())
            {
                morreOmen(atacanteGO);
            }
        }
        else if (tipo == tipoPlayer.fantasma)
        {
            if (arma == ItemEnum.Crowbar.ToString() ||
                arma == ItemEnum.HolyWater.ToString() ||
                arma == ItemEnum.OldTalisman.ToString())
            {
                morreOmen(atacanteGO);
            }
        }


        //TODO: 
        //Animação de morte
        //Dropa os itens
        //Controle de Ress
        /* 
         * if (tipo = humano)
         * {
         *      if (quem_matou.tipo == 'fantasma')
         *      {
         *          transforma em fantasma;
         *      } 
         *      else if (quem_matou.tipo == 'vampiro')
         *      {
         *        transforma em vampiro;
         *      }
         * }
         * else if (tipo == vampiro)
         * {
         *      morre
         * }
         * else if (tipo == fantasma)
         * {
         *      if (already_ressed == true)
         *      {
         *          morre de vez
         *      }
         *      else
         *      {
         *          dá um tempo, transforma em humano         
         *      }
         * }
         * }
         * 

        //dá um tempo
    }*/

	void OnGUI()
	{
		if (tipo == tipoPlayer.vampiro){
			GUILayout.BeginArea(new Rect(100f,Screen.height-60f,Screen.width-100f,50f));
			GUILayout.Box("You are now a vampire! Kill the lambs!");
			GUILayout.EndArea();
		}
	}

}
