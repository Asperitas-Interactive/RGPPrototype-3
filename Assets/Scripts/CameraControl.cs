using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private float sensitivity = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.Rotate(0.0f, -45.0f, 0.0f, Space.Self);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.Rotate(0.0f, 45.0f, 0.0f, Space.Self);
        }
    }
}
