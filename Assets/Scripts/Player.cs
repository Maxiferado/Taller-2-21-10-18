using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Actor {

    public bool isFree;
    public Text contagioText;


    void Start()
    {
      
    }

    void Update()
    {
        SickUpdate();

        if(isSick)
        {
            contagioText.gameObject.SetActive(true);  
        }
        else
        {
            contagioText.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Key")
        {
            collision.gameObject.SetActive(false);
            isFree = true;
        }
    }

}
