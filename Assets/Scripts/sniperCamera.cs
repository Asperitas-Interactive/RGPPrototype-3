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

    [SerializeField]
    private LayerMask layerIgnore;

    private LineRenderer lRender;

    public float zoomDistance = 30.0f;

    float currZoom = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        defaultPos = new Vector3();

        defaultPos = transform.position;
        lRender = gameObject.GetComponent<LineRenderer>();
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

        if (Input.GetButtonDown("SniperLeft"))
        {
            if(transform.position.z > -15.0f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 20.0f);
            }
        }

        if (Input.GetButtonDown("SniperRight"))
        {
            if (transform.position.z < 25.0f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 20.0f);
            }
        }


        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Vector3 rayOrigin =transform.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            

            Vector3 forward = transform.TransformDirection(Vector3.forward) * 10000;
            Debug.DrawRay(transform.position, forward, Color.green);
            //Vector3 RayDisjoint = new Vector3(transform.position.x, transform.position.y - 0.01f, transform.position.z);
            //lRender.SetPosition(0, RayDisjoint);
            //lRender.SetPosition(1, forward);

            //Debug.Log("fire");
            if(Physics.Raycast(rayOrigin, transform.forward, out hit, 1000, ~layerIgnore))
            if (hit.collider.isTrigger)
            {
                hit.collider.gameObject.GetComponent<AudioDetection>().hasHeard = true;
                Physics.Raycast(hit.point, transform.forward, out hit, 100);
            }
            if (hit.collider.CompareTag("Enemy"))
            {
                Destroy(hit.collider.gameObject);
            }

            Debug.Log(hit.collider.gameObject);
           
        }

        if (Input.GetButton("TargetReticle"))
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward) * 10000;
            Vector3 RayDisjoint = new Vector3(transform.position.x, transform.position.y - 0.01f, transform.position.z);
            lRender.SetPosition(0, RayDisjoint);
            lRender.SetPosition(1, forward);
        }

    }
}
