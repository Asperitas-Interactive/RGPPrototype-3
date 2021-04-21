using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sniperCamera : MonoBehaviour
{
    [SerializeField]
    private float sensitivity = 100f;

    Vector3 defaultPos;
    float xRot = 0f;
    float yRot = 0f;

    public float zoomDistance = 30.0f;

    float currZoom = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        defaultPos = new Vector3();

        defaultPos = transform.position;
        Cursor.lockState = CursorLockMode.Locked;
        transform.Rotate(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;



        xRot -= mouseY;
        yRot += mouseX;

        xRot = Mathf.Clamp(xRot, -45.0f, 45.0f);

        transform.eulerAngles = new Vector3(xRot, yRot, 0.0f);

       // Debug.Log(mouseX);

        // transform.Rotate(Vector3.up * mouseX);

        if (Input.GetAxis("Mouse ScrollWheel") > 0.0f)
        {
            if (GetComponent<Camera>().fieldOfView > 6)

            {
                GetComponent<Camera>().fieldOfView -= 200.0f * Time.deltaTime;

            }
            else
            {
                GetComponent<Camera>().fieldOfView = 6;

            }
            
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0.0f)
        {
            if (GetComponent<Camera>().fieldOfView < 60)

            {
                GetComponent<Camera>().fieldOfView += 200.0f * Time.deltaTime;

            }
            else
            {
                GetComponent<Camera>().fieldOfView = 60;

            }
        }



        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Vector3 rayOrigin =transform.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            Physics.Raycast(rayOrigin, transform.forward, out hit, 1000);

            //Debug.Log("fire");
            if (hit.collider.isTrigger)
            {
                hit.collider.gameObject.GetComponent<AudioDetection>().hasHeard = true;
                Physics.Raycast(hit.point, transform.forward, out hit, 100);
            }
            if (hit.collider.CompareTag("Enemy"))
            {
                Destroy(hit.collider.gameObject);
            }
           
        }

    }
}
