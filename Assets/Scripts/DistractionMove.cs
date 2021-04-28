using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractionMove : MonoBehaviour
{
    bool inZone = false;
    PlayerControl player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C) && inZone == true)
        {
            player.setRB(gameObject.GetComponent<Rigidbody>());
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inZone = true;
            player = collision.gameObject.GetComponent<PlayerControl>();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inZone = false;
        }
    }
}
