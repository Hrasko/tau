using UnityEngine;
using System.Collections;

public class SalaSpawn : MonoBehaviour
{

    public int[] salasPossiveis;
    public int[] peso;
    public int total;
    System.Random random = new System.Random();

    public bool podeSpawnnar = true;

    void Awake()
    {
        total = 0;
        for (int i = 0; i < peso.Length; i++)
        {
            total += peso[i];
        }

    }

    public int sorteiaSala()
    {
        int sorteio = random.Next(total);
        int indice = 0;
        int walk = peso[indice];
        while (sorteio >= walk)
        {
            indice++;
            if (indice < peso.Length)
            {
                walk += peso[indice];
            }
        }
        return salasPossiveis[indice];
    }

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.up, 2))
        {
            podeSpawnnar = false;
        }

        if (podeSpawnnar)
        {
            Debug.DrawRay(transform.position, transform.up, Color.green);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.up, Color.red);
        }
    }

    public void desativeSpawnner()
    {
        Destroy(gameObject);
    }

    void OnTriggerStay(Collider collision)
    {
        podeSpawnnar = false;
    }

    void OnTriggerEnter(Collider collision)
    {
        podeSpawnnar = false;
    }

}
