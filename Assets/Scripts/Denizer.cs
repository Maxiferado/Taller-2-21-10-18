using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Denizer : Actor {

    public Vector3 runGoal;

    public Transform[] myPath;

    public Color healthyColor;

    private int currentWayPoint;

    private NavMeshAgent agent;

    private Material myMaterial;


	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        myMaterial = GetComponent<MeshRenderer>().material;
        healthyColor = myMaterial.color;

        agent = GetComponent<NavMeshAgent>();
        SetGoal();
        StartCoroutine(CheckTarget());
        
	}
	
	// Update is called once per frame
	void Update () {
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

    IEnumerator CheckTarget()
    {
        for (int i = 0; i >= 0; i++)
        {

            yield return new WaitForSeconds(0.3f);

                CheckPosition();

        }

    }
}
