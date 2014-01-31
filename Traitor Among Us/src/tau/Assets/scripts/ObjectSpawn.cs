using UnityEngine;
using System.Collections.Generic;

public enum tipoObj
{
    generico,
    quest,
    chave,
    arma,
    holy,
    omen
}

public class ObjectSpawn : MonoBehaviour {

    public static List<ObjectSpawn> all = new List<ObjectSpawn>();
    public static Dictionary<tipoObj, List<int>> dicionario = new Dictionary<tipoObj, List<int>>();
    public tipoObj[] tipos;
    public int objectsSpawned = 0;
    public Sala sala;

    public int id;

    void Awake()
    {
        id = all.Count;
        all.Add(this);
        
        for (int i = 0; i < tipos.Length; i++)
        {
            if (!dicionario.ContainsKey(tipos[i]))
            {
                List<int> lista = new List<int>();
                dicionario.Add(tipos[i], lista);
            }
            dicionario[tipos[i]].Add(id);
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void spawn(string nome)
    {

        Vector3 pos = transform.position + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
        GameObject go =  PhotonNetwork.Instantiate(nome, pos, transform.rotation, 0);	
		go.name = nome;
        objectsSpawned++;
    }
}
