using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour {

    public Text winTex;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Player tempPlayer = collision.gameObject.GetComponent<Player>();
            if(!tempPlayer.isSick && tempPlayer.isFree)
            {
                winTex.text = "You Win";
                gameObject.SetActive(false);
            }
        }
    }

}
