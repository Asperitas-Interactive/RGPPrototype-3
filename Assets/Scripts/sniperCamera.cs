using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sniperCamera : MonoBehaviour
{
    public AudioSource GunShot;
    public AudioSource Scope;
    public AudioSource Reload;

    public DialougeP1Control dialouge;

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

    private bool playReloadSound = false;

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

    float minRot = -180.0f;
    float maxRot = 0.0f;

    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        defaultPos = new Vector3();

        cam = transform.GetChild(0).GetComponent<Camera>();

        defaultPos = transform.position;
        lRender = gameObject.GetComponent<LineRenderer>();
        Cursor.lockState = CursorLockMode.Locked;

        //pivot = reticle.localPosition;
        ammoUI = GameObject.FindGameObjectsWithTag("AmmoIcons");
    }

    void recoil()
    {
        GetComponent<Animator>().SetBool("recoil", true);
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
        yRot = Mathf.Clamp(yRot, minRot, maxRot);

        transform.eulerAngles = new Vector3(xRot, yRot, 0.0f);

        reloadTime -= Time.deltaTime;
        recoilTimer -= Time.deltaTime;

        if(resetRecoil && recoilTimer < 0.0f)
        {
            GetComponent<Animator>().SetBool("recoil", false);
            resetRecoil = false;
        }

        // Debug.Log(mouseX);

        // transform.Rotate(Vector3.up * mouseX);

        if (Input.GetAxis("Mouse ScrollWheel") > 0.0f)
        {
            if (cam.fieldOfView > 4)

            {
                cam.fieldOfView -= 200.0f * Time.deltaTime;

            }
            else
            {
                cam.fieldOfView = 4;

            }

            if (Scope.isPlaying == false)
            {
                Scope.Play();
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0.0f)
        {
            if (cam.fieldOfView < 60)

            {
                cam.fieldOfView += 200.0f * Time.deltaTime;

            }
            else
            {
                cam.fieldOfView = 60;

            }
            if (Scope.isPlaying == false)
            {
                Scope.Play();
            }
        }

        if (Input.GetButtonDown("SniperLeft"))
        {
            transform.position = new Vector3(63.5f, transform.position.y, 5.0f);
            yRot = -90.0f;
            minRot = -180.0f;
            maxRot = 0.0f;
        }

        if (Input.GetButtonDown("SniperRight"))
        {
            transform.position = new Vector3(1.5f, transform.position.y, 64.0f);
            yRot = -180.0f;
            minRot = -270.0f;
            maxRot = -90.0f;
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

        if (reloadTime <= 0.5f && playReloadSound == true)
        {
            Reload.Play();
            playReloadSound = false;
        }


        if (Input.GetButtonDown("Fire1"))
        {
            if (bulletCount > 0)
            {
                if (reloadTime <= 0.0f)
                {
                    RaycastHit hit;
                    Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

                    Vector3 forward = transform.TransformDirection(Vector3.forward) * 10000;
                    Debug.DrawRay(transform.position, forward, Color.green);

                    Debug.Log("fire");
                    if (Physics.Raycast(rayOrigin, transform.forward, out hit, 10000, ~layerIgnore))
                    {
                        Debug.Log(hit.collider.gameObject);

                        if (hit.collider.isTrigger)
                        {
                            hit.collider.gameObject.GetComponent<AudioDetection>().hasHeard = true;
                            Physics.Raycast(hit.point, transform.forward, out hit, 100);
                        }
                        if (hit.collider.CompareTag("Enemy"))
                        {
                            AIControl ai;
                            AIStationary aiS;
                           
                           if(hit.collider.gameObject.transform.parent.TryGetComponent<AIControl>(out ai))
                            {
                                ai.death = true;
                                ai.tag = "Dead";
                                ai.transform.GetChild(2).tag = "Dead";

                                dialouge.AssignText(19);
                            }
                           else if(hit.collider.gameObject.transform.parent.TryGetComponent<AIStationary>(out aiS))
                            {
                                aiS.transform.GetChild(0).tag = "Dead";

                                aiS.death = true;
                                aiS.tag = "Dead";
                                dialouge.AssignText(19);
                            }
                        }
                    }


                    GunShot.Play();

                    ammoUI[bulletCount - 1].GetComponent<Image>().enabled = false;
                    bulletCount--;

                    if(bulletCount == 0)
                    {
                        dialouge.AssignText(15);
                    }

                    //camera.fieldOfView = 60;
                    reloadTime = 2.0f;
                    playReloadSound = true;
                    recoil();
                }
            } else
            {
                dialouge.AssignText(14);
            }
        }

        if (Input.GetButton("TargetReticle"))
        {
            lRender.enabled = true;
            Vector3 forward = transform.TransformDirection(Vector3.forward) * 10000;
            Vector3 RayDisjoint = new Vector3(transform.position.x - 5f, transform.position.y - 10f, transform.position.z);


            lRender.SetPosition(0, RayDisjoint);

            RaycastHit hit;
            Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            if (Physics.Raycast(rayOrigin, transform.forward, out hit, 10000, ~layerIgnore))
            {
                lRender.SetPosition(1, hit.point);
            }
        } else
        {
            lRender.enabled = false;
        }

    }
}
