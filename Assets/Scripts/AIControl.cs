using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class AIControl : MonoBehaviour
{
    public GameObject dest;
    public NavMeshAgent agent;
    public AudioDetection audioDetection;
    public ViewDetection viewDetection;
    Vector3 defaultPos;
    public float patrolDistance= 2.0f;
    enum State
    {
        follow,
        patrol,
    };

    State state = State.patrol;
    Vector3 followDir;
    float followTimer;
    public float maxFollowTimer;



    // Start is called before the first frame update
    void Start()
    {
        defaultPos = new Vector3();
        defaultPos = transform.position;
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.SetDestination((transform.forward * patrolDistance + transform.position));
    }

    // Update is called once per frame
    void Update()
    {
        followTimer -= Time.deltaTime;

        if(state == State.follow && followTimer < 0.0f)
        {
            state = State.patrol;
            agent.SetDestination(defaultPos);
        }

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

        
        if (agent.remainingDistance < 0.1f && state == State.patrol)
        {
            agent.transform.Rotate(0f, 180f, 0f);
            agent.SetDestination((transform.forward * patrolDistance + transform.position));
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(1);
        }

        if(other.CompareTag("Diversion"))
        {
            state = State.follow;
            followTimer = maxFollowTimer;
        }


        
    }
}
