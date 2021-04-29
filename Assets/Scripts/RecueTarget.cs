using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RecueTarget : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;
    public DialougeP1Control p1;
    public DialougeP1Control p2;
    public DialougeP1Control res;
    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(agent.enabled == true)
        {
            agent.SetDestination(player.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            agent.enabled = true;
            player = other.gameObject;
            other.gameObject.GetComponent<PlayerControl>().SetWin();
            p1.AssignText(5);
            p2.AssignText(18);
            res.AssignText(0);
        }
    }
}
