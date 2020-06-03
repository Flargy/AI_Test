using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : MonoBehaviour
{

    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    
    void Update()
    {

        if (agent.hasPath == false)
        {
            Vector3 movement = new Vector3(15, 0, 0);
            agent.SetDestination(agent.gameObject.transform.position + movement);
        }



    }
}
