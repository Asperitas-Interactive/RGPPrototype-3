using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sniperCamera : MonoBehaviour
{
    float horizontal;
    float vertical = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Mouse X");
        vertical = -Input.GetAxis("Mouse Y");

       transform.Rotate(vertical, horizontal, 0);

        if(Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Vector3 rayOrigin = GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            Physics.Raycast(rayOrigin, transform.forward, out hit, 1000);

            Debug.Log("fire");
            if(hit.collider.CompareTag("Enemy"))
            {
                Destroy(hit.collider.gameObject);
            }
        }

    }
}
