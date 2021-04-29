using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public AIControl LinkedEnemy;
    public bool slomo = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(LinkedEnemy.CompareTag("Dead"))
        {
            GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().time(1.0f, 0.0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (LinkedEnemy != null)
            {
                LinkedEnemy.dest = GameObject.FindGameObjectWithTag("Player");
                LinkedEnemy.state = AIControl.State.follow;
                LinkedEnemy.state = AIControl.State.patrol;
                LinkedEnemy.state = AIControl.State.follow;

            }
            GetComponentInParent<Animator>().SetBool("Open", true);
            if(slomo)
                GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().time(0.1f, 5.0f);
        }
    }
}
