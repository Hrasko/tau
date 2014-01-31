using UnityEngine;
using System.Collections.Generic;

public class Sala : MonoBehaviour
{
    public static List<Sala> all = new List<Sala>();
	public int id;
    public SalaSpawn[] salaSpawnners;
    public ObjectSpawn[] spawnners;

    void Awake()
    {
		id = all.Count;
        all.Add(this);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
