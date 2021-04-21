using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour
{
    public GameObject dest;
    public NavMeshAgent agent;
    public AudioDetection audioDetection;
    public ViewDetection viewDetection;
    Vector3 defaultPos;
    public float patrolDistance= 5.0f;
    enum State
    {
        follow,
        patrol,
    };

    State state = State.patrol;

    // Start is called before the first frame update
    void Start()
    {
        defaultPos = new Vector3();
        defaultPos = transform.position;
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.SetDestination(transform.forward * patrolDistance);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0;  i < audioDetection.HeardObjects.Count; i++)
        {
            if(audioDetection.HeardObjects[i].tag == "Player")
            {
               // dest = audioDetection.HeardObjects[i];
            }
        }

        for(int i = 0; i < viewDetection.SightedObjects.Count; i++)
        {
            if (audioDetection.HeardObjects[i].tag == "Player")
            {
                Debug.Log("On sight");
            }
        }

        if (dest != null)
        {
        //    agent.SetDestination(dest.transform.position);
        }

        if (agent.remainingDistance < 0.1f)
        {
            agent.transform.Rotate(0f, 180f, 0f);
            agent.SetDestination(transform.forward * patrolDistance);
        }
    }
}
