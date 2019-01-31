using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController2 : MonoBehaviour {

    public float lookRadius = 10f;

    Transform target;
    NavMeshAgent pathfinder;

    bool hasTarget;

    // Use this for initialization
    void Start () {
        pathfinder = GetComponent<NavMeshAgent>();

        if (GameObject.FindGameObjectsWithTag("Player") != null)
        {
            hasTarget = true;

            target = GameObject.FindGameObjectWithTag("Player").transform;
            StartCoroutine(UpdatePath());
        }
            
    }
	
	// Update is called once per frame
	void Update () {
       
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);

    }

    IEnumerator UpdatePath()
    {
        float refreshRate = .25f;

        while (hasTarget)
        {       
            
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                Vector3 targetPosition = target.position - dirToTarget;
               
                    pathfinder.SetDestination(target.position);
                
            
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
