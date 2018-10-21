using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CureGenerator : MonoBehaviour {

    public GameObject prefabCure;

    public Transform[] spawnPoint;

	// Use this for initialization
	void Start ()
    {

        for (int i = 0; i < 6; i++)
        {
            int tempNum = Random.Range(0, spawnPoint.Length-1);
            Instantiate(prefabCure, spawnPoint[tempNum].position, spawnPoint[tempNum].rotation);
        }
	}

}
