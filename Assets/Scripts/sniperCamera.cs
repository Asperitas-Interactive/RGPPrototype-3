using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sniperCamera : MonoBehaviour
{
    [SerializeField]
    private float sensitivity = 100f;

    Vector3 defaultPos;
    float xRot = 0.0f;
    float yRot = -90.0f;

    [SerializeField]
    private LayerMask layerIgnore;

    private LineRenderer lRender;

    public float zoomDistance = 30.0f;

    float currZoom = 0.0f;

    [SerializeField]
    private int bulletCount = 8;

    private float reloadTime = 0.0f;

    [SerializeField]
    private RectTransform reticle;

    [SerializeField]
    private GameObject[] ammoUI;

    //Idle Sway Variable
    //Used code from:
    //https://forum.unity.com/threads/making-an-object-move-in-a-figure-8-programatically.38007/
    /*private float speed = 1.0f;
    private float xScale = 1.0f;
    private float yScale = 1.0f;
 
    private Vector3 pivot;
    private Vector3 pivotOffset;
    private float phase;
    private bool invert = false;
    private float PI2 = Mathf.PI* 2;*/

    bool resetRecoil = false;
    float recoilTimer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        defaultPos = new Vector3();

        defaultPos = transform.position;
        lRender = gameObject.GetComponent<LineRenderer>();
        Cursor.lockState = CursorLockMode.Locked;

        //pivot = reticle.localPosition;
        ammoUI = GameObject.FindGameObjectsWithTag("AmmoIcons");
    }

    void recoil()
    {
        transform.position += -transform.forward * 4.0f;
        recoilTimer = 0.2f;
        resetRecoil = true;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRot -= mouseY;
        yRot += mouseX;

        xRot = Mathf.Clamp(xRot, -45.0f, 45.0f);
        yRot = Mathf.Clamp(yRot, -180, 0.0f);

        transform.eulerAngles = new Vector3(xRot, yRot, 0.0f);

        reloadTime -= Time.deltaTime;
        recoilTimer -= Time.deltaTime;

        if(resetRecoil && recoilTimer < 0.0f)
        {
            transform.position -= -transform.forward * 4.0f;
            resetRecoil = false;
        }

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

        //Idle Sway
        /*pivotOffset = Vector3.up * 2 * yScale;

        phase += speed * Time.deltaTime;
        if (phase > PI2)
        {
            invert = !invert;
            phase -= PI2;
        }
        if (phase < 0) phase += PI2;

        reticle.localPosition = pivot + (invert ? pivotOffset : Vector3.zero);
        reticle.localPosition += new Vector3(reticle.localPosition.x + Mathf.Sin(phase) * xScale, reticle.localPosition.y + Mathf.Cos(phase) * (invert ? -1 : 1) * yScale, 0.0f);*/


        if (Input.GetButtonDown("Fire1"))
        {
            if (bulletCount > 0)
            {
                if (reloadTime <= 0.0f)
                {
                    RaycastHit hit;
                    Vector3 rayOrigin = transform.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

                    Vector3 forward = transform.TransformDirection(Vector3.forward) * 10000;
                    Debug.DrawRay(transform.position, forward, Color.green);

                    Debug.Log("fire");
                    if (Physics.Raycast(rayOrigin, transform.forward, out hit, 10000, ~layerIgnore))
                    {
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

                    Debug.Log(hit.collider.gameObject);

                    ammoUI[bulletCount - 1].GetComponent<Image>().enabled = false;
                    bulletCount--;
                    //GetComponent<Camera>().fieldOfView = 60;
                    reloadTime = 2.0f;

                    recoil();
                }
            }
        }

        if (Input.GetButton("TargetReticle"))
        {
            lRender.enabled = true;
            Vector3 forward = transform.TransformDirection(Vector3.forward) * 10000;
            Vector3 RayDisjoint = new Vector3(transform.position.x, transform.position.y - 0.01f, transform.position.z);
            lRender.SetPosition(0, RayDisjoint);
            lRender.SetPosition(1, forward);
        } else
        {
            lRender.enabled = false;
        }

    }
}
