using UnityEngine;
using System.Collections;

public enum ItemEnum
{
	Amulet,
	AshesUrn,
	Crowbar,
	Cruz,
	Faca,
	HolyWater,
	Garlic,
	IronKey,
	JarroPequeno,
	Jarro,
	OldTalisman,
	Ouija,
	Sarcofago,
	ShamanStaff,
	VampirePainting,
	WoodenStake
}

[System.Serializable]
public class ItemSage : System.Object {
    
    static tipoObj[] tipoGenerico = { tipoObj.generico };
    /*static tipoObj[] tipoArma = { tipoObj.arma };
    static tipoObj[] tipoArmaFinal = { tipoObj.arma, tipoObj.quest };
    static tipoObj[] tipoChaveFinal = { tipoObj.chave, tipoObj.quest };
    static tipoObj[] tipoOmen = { tipoObj.omen };*/

    public static tipoObj[] tipos (ItemEnum qual)
	{
        
        switch (qual)
        {
            default: return tipoGenerico;
        }
	}
    public static int indicePrefab (ItemEnum qual)
	{
        switch (qual)
        {
            case ItemEnum.IronKey: return 0;
			case ItemEnum.WoodenStake: return 1;
            case ItemEnum.Sarcofago: return 2;
            default: return 0;
        }
	}
}
