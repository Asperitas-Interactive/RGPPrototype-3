using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    public DistractionMove[] trajectoryItems;
    LineRenderer self;
    // Start is called before the first frame update
    void Start()
    {
        trajectoryItems = GameObject.FindObjectsOfType<DistractionMove>();
        self = gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        self.enabled = false;
        if (Input.GetKey(KeyCode.Space))
        {
            for(int i = 0; i < trajectoryItems.Length; i++)
            {
                if (trajectoryItems[i].attached == true)
                {
                    self.enabled = true;
                    break;
                }
            }
        }
    }
}
