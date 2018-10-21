using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cure : MonoBehaviour {

    public int deseaseIdcure;

    public float inmunity;

    private void Start()
    {

        int tempInt = Random.Range(0, 2);
        float tempFloat = Random.Range(0f, 0.2f);
        deseaseIdcure = tempInt;
        inmunity = tempFloat;

    }

}
