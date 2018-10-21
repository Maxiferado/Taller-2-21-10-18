using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Survivor : Actor {
    public Vector3 runGoal;

    public Transform[] myPath;

    public Color healthyColor;

    private int currentWayPoint;

    private NavMeshAgent agent;

    private GameObject currentCureObject;

    private Material myMaterial;

    // Use this for initialization
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        myMaterial = GetComponent<MeshRenderer>().material;
        healthyColor = myMaterial.color;
        agent = GetComponent<NavMeshAgent>();
        SetGoal();
        StartCoroutine(CheckTarget());
       
    }

    // Update is called once per frame
    void Update()
    {
        SickUpdate();
    }



    void SetGoal()
    {
        // tempRandom = Random.Range(0, goals.Length);

        runGoal = GetTargetLocation();

        agent.SetDestination(runGoal);

        // goal = goals[tempRandom];
    }

    public Vector3 GetTargetLocation()
    {

        currentWayPoint++;
        if (currentWayPoint >= myPath.Length)
        {
            currentWayPoint = 0;
        }
        return myPath[currentWayPoint].position;

    }

    void CheckPosition()
    {
        Vector3 positionGoal = runGoal;
        Vector3 selfPosition = transform.position;
        //Debug.Log("distance" + Vector3.Distance(positionGoal, positionPlayer));
        if (Vector3.Distance(positionGoal, selfPosition) < 2f)
        {
            SetGoal();
        }
    }

    void CheckCureTaken()
    {
        Vector3 positionGoal = runGoal;
        Vector3 selfPosition = transform.position;
        //Debug.Log("distance" + Vector3.Distance(positionGoal, positionPlayer));

        if (Vector3.Distance(positionGoal, selfPosition) < 3f)
        {


            Cure tempCure = currentCureObject.GetComponent<Cure>();
            float tempProbability;
            tempProbability = probabilityToGeSick[tempCure.deseaseIdcure] - tempCure.inmunity;
            if (tempProbability>0.5f)
            {
                probabilityToGeSick[tempCure.deseaseIdcure] -= tempCure.inmunity;
            }else
            {
                probabilityToGeSick[tempCure.deseaseIdcure] = 0.5f;
            }
            

            if(desease!=null && desease.virusId == tempCure.deseaseIdcure)
            {
                Cure();
                myMaterial.color = healthyColor;
            }
            currentCureObject.SetActive(false);
            StopAllCoroutines();
            SetGoal();
            StartCoroutine(CheckTarget());
        }
    }

    void CheckCure()
    {
        RaycastHit hit;

        Vector3 p1 = transform.position;

        if(Physics.SphereCast(p1,5f,transform.forward, out hit))
        {
            if(hit.collider.gameObject.tag == "Cure")
            {
                currentCureObject = hit.collider.gameObject;
                StopAllCoroutines();
                runGoal = hit.transform.position;
                agent.SetDestination(runGoal);
                StartCoroutine(GoToCure());
            }
        }
    }

    IEnumerator CheckTarget()
    {
        for (int i = 0; i >= 0; i++)
        {

            yield return new WaitForSeconds(0.3f);

            CheckPosition();
            CheckCure();
            
        }

    }

    IEnumerator GoToCure()
    {
        for (int i = 0; i >= 0; i++)
        {

            yield return new WaitForSeconds(0.3f);

            CheckCureTaken();

        }
    }
}
