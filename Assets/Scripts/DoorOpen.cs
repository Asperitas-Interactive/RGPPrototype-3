using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorOpen : MonoBehaviour
{
    //This code is from Prototype 1, but modified
    private bool isOpening = false;
    [SerializeField]
    private bool bClockwise = true;
    [SerializeField]
    private BoxCollider bcTrigger;
    private float fClockwiseComponent = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        if(bClockwise == true)
        {
            fClockwiseComponent = 1.0f;
        } else
        {
            fClockwiseComponent = -1.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isOpening == true)
        {
            //Rotates based on if clockwise or counter clockwise
            gameObject.transform.Rotate(0.0f, 150.0f * fClockwiseComponent * Time.deltaTime, 0.0f, 0f);
        }

        //Debug.Log(gameObject.transform.rotation.eulerAngles.y);

        //Now checks for it its counter clock wise or clockwise
        if (bClockwise == true)
        {
            if (gameObject.transform.rotation.eulerAngles.y >= 350.0f)
            {
                isOpening = false;
            }
        }
        else
        {
            if (gameObject.transform.rotation.eulerAngles.y >= 180.0f)
            {
                isOpening = false;
            }
        }
    }

    public void OpenDoor()
    {
        isOpening = true;
        bcTrigger.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            OpenDoor();
        }
    }
}
