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

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0;  i < audioDetection.HeardObjects.Count; i++)
        {
            if(audioDetection.HeardObjects[i].tag == "Player")
            {
                Debug.Log("noted");
            }
        }

        for(int i = 0; i < viewDetection.SightedObjects.Count; i++)
        {
            if (audioDetection.HeardObjects[i].tag == "Player")
            {
                Debug.Log("On sight");
            }
        }
    }
}
