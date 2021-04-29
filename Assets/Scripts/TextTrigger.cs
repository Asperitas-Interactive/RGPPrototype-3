using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    public int textnum = 0;
    public DialougeP1Control dialouge;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            dialouge.AssignText(textnum);
        }
    }
}
