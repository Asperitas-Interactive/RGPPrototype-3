using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractionMove : MonoBehaviour
{
    bool activated = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(activated == true)
        {
            transform.Translate(Vector3.forward * 10 * Time.deltaTime, Space.World);
        }
    }

    public void ActivateDistraction()
    {
        activated = true;
        Debug.Log("ah");
    }
}
