using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Actor : MonoBehaviour {

    public Desease desease;

    public float timeWithSick;

    public float timeToDie;

    public float[] probabilityToGeSick;


    public float movementSpeed;

    public bool isSick;

    public bool isDecreased;

    private float timeStun;

    private bool isStun;



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ent" && desease==null)
        {
           
            desease = other.gameObject.GetComponent<Actor>().desease;
            if(CheckInfection())
            {
                isSick = true;
                Debug.Log("Contagiado");
            }else
            {
                desease = null;
                Debug.Log("Salvado");
            }
            
        }

       

        if(other.gameObject.tag == "Cure")
        {
            Cure tempCure = other.gameObject.GetComponent<Cure>();
            float tempProbability = probabilityToGeSick[tempCure.deseaseIdcure] - tempCure.inmunity;
            if (tempProbability > 0.5f)
            {
                probabilityToGeSick[tempCure.deseaseIdcure] -= tempCure.inmunity;
            }
            else
            {
                probabilityToGeSick[tempCure.deseaseIdcure] = 0.5f;
            }


            if (desease != null && desease.virusId == tempCure.deseaseIdcure)
            {
                Cure();
            }
            other.gameObject.SetActive(false);
        }

        if(other.gameObject.tag == "CureZone")
        {
            Cure();
        }

        if(other.gameObject.tag == "Medicina")
        {
            timeWithSick -= 15f;

            timeToDie -= 15f;
        }
    }

    public bool CheckInfection()
    {
        float tempRandom = Random.Range(0f, 1f);
        if(tempRandom>= probabilityToGeSick[desease.virusId])
        {
            return false;
        }else
        {
            return true;
        }
        
    }


    public void SickUpdate()
    {
        if (!isSick) return;

        if (timeWithSick <= desease.onSetTime)
        {
            timeWithSick += Time.deltaTime;
            
        }
        else if (timeToDie <= desease.timeUntilDeath)
        {
            if(!isDecreased)
            {
                GetComponent<MeshRenderer>().material.color = desease.sickColor;
                isDecreased = true;
                if (gameObject.tag == "Player")
                {
                    PlayerSlowSpeed();
                }else
                    DeacreaseSpeed();
            }
            Stun();
            timeToDie += Time.deltaTime;
        }
        else
        {
            gameObject.SetActive(false);
        }

    }

    public void  DeacreaseSpeed()
    {
        float tempSpeed = movementSpeed * (1 - desease.movementPercent);
        gameObject.GetComponent<NavMeshAgent>().speed = tempSpeed;
    }

    void Stun()
    {
        timeStun += Time.deltaTime;
        if(timeStun>=desease.timeToStun && !isStun)
        {
            float tempRandom = Random.Range(0f, 1f);
            if(tempRandom>=desease.probabilityToStun)
            {
                
                timeStun = 0;
                return;
            }
            isStun = true;

            if(gameObject.tag == "Player")
            {
                PlayerNoSpeed();
            }else
            gameObject.GetComponent<NavMeshAgent>().speed = 0f;
        }else if(timeStun >= desease.timeToStun+2)
        {
            timeStun = 0;
            isStun = false;
            if (gameObject.tag == "Player")
            {
                PlayerSlowSpeed();
            }else
            {
                float tempSpeed = movementSpeed * (1 - desease.movementPercent);
                gameObject.GetComponent<NavMeshAgent>().speed = tempSpeed;
            }
              
        }


    }

    public void PlayerSlowSpeed()
    {
        float tempMass = movementSpeed;
        tempMass = tempMass * (1 + desease.movementPercent);
        GetComponent<Rigidbody>().mass = tempMass;
    }
    
    public void PlayerNoSpeed()
    {
        GetComponent<Rigidbody>().mass = 2000f;
    }

    public void NormalPlayerSpeed()
    {
        GetComponent<Rigidbody>().mass = movementSpeed;
    }

    


    public void Cure()
    {
        timeWithSick = 0;
        timeToDie = 0;
        isDecreased = false;
        isStun = false;
        timeStun = 0;
        isSick = false;
        if(gameObject.tag =="Player")
        {

        }else
        {
            gameObject.GetComponent<NavMeshAgent>().speed = movementSpeed;
        }
        

    }


}
