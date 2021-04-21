using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private float sensitivity = 100f;

    private float xRot = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0.0f, -45.0f * Time.deltaTime, 0.0f, Space.Self);
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0.0f, 45.0f * Time.deltaTime, 0.0f, Space.Self);
        }
    }
}
