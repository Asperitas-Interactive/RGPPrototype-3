using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorOpen : MonoBehaviour
{
    public Image ibuttonPrompt;
    private bool isOpening = false;
    [SerializeField]
    private bool bClockwise = true;
    [SerializeField]
    private BoxCollider bcTrigger;
    private float fClockwiseComponent = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        ibuttonPrompt.enabled = false;
        //Check if we want the door to be opened by the player or not
        if(gameObject.tag == "OpenableDoor")
        {
            bcTrigger.enabled = true;
        } else if (gameObject.tag == "LockedDoor")
        {
            bcTrigger.enabled = false;
        }

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
            if (gameObject.transform.rotation.eulerAngles.y >= 90.0f)
            {
                isOpening = false;
            }
        }
        else
        {
            if (gameObject.transform.rotation.eulerAngles.y <= 270.0f)
            {
                isOpening = false;
            }
        }
    }

    public void OpenDoor()
    {
        isOpening = true;
        ibuttonPrompt.enabled = false;
        bcTrigger.enabled = false;
    }

    public void CloseDoor()
    {
        bcTrigger.enabled = true;
    }



    //you know how interaction works in this game by now
    void OnTriggerStay(Collider other)
    {
        //Check if the player is in the area
        if (other.tag == "Player")
        {
            //General practice to prevent a crash if no script is put on
            if (other.GetComponent<playerMovement>() != false)
            {
                //Show the button prompt
                ibuttonPrompt.enabled = true;

                //Now we can allow the player to activate his button press in movement
                other.GetComponent<playerMovement>().enableDoorInteract(this);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        //Check if the player left the interactible area
        if (other.tag == "Player")
        {
            //General practice to prevent a crash if no script is put on
            if (other.GetComponent<playerMovement>() != false)
            {
                //Remove the button promp
                ibuttonPrompt.enabled = false;

                //We want to turn off the ability to use the interact button too
                other.GetComponent<playerMovement>().disableDoorInteract();
            }
        }
    }
}
