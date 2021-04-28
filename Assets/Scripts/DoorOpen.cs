using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public AIControl LinkedEnemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            LinkedEnemy.state = AIControl.State.follow;
            LinkedEnemy.followDir = GameObject.FindGameObjectWithTag("Player").transform.position;
            GetComponent<Animator>().SetBool("Open", true);
            GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().time(0.1f, 2.0f);
        }
    }
}
