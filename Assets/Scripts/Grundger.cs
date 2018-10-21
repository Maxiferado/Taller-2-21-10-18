using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Grundger : Actor {
    public Vector3 runGoal;

    public Transform baseEnemy;

    public Color healthyColor;

    private int currentWayPoint;

    private NavMeshAgent agent;

    private Transform target;

    private Material myMaterial;

    // Use this for initialization
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        myMaterial = GetComponent<MeshRenderer>().material;
        healthyColor = myMaterial.color ;
        
    }

    // Update is called once per frame
    void Update()
    {
        SickUpdate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target = other.transform;
            SetGoal();
            StartCoroutine(CheckTarget());
        }
    }



    void SetGoal()
    {
        // tempRandom = Random.Range(0, goals.Length);

        runGoal = target.position;

        if(Vector3.Distance(runGoal, transform.position)>10f)
        {
            StopAllCoroutines();
            runGoal = baseEnemy.position;
        }

        agent.SetDestination(runGoal);

        // goal = goals[tempRandom];
    }

   

    void CheckPosition()
    {
        Vector3 positionGoal = runGoal;
        Vector3 selfPosition = transform.position;
        //Debug.Log("distance" + Vector3.Distance(positionGoal, positionPlayer));
        if (Vector3.Distance(positionGoal, selfPosition) < 3f)
        {
            Debug.Log("PlayerTouched");
        }
    }

    IEnumerator CheckTarget()
    {
        for (int i = 0; i >= 0; i++)
        {

            yield return new WaitForSeconds(0.3f);

            CheckPosition();

            SetGoal();

        }

    }
}
