using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    CharacterController controller;
    [SerializeField]
    float speed, gravity;

    public Rigidbody item;

    /*Scene PredictionScene;
    PhysicsScene PredictionPhysScene;
    Scene CurrentScene;
    PhysicsScene CurrentPhysScene;
    GameObject clone;
    public LineRenderer lr;*/

    private Vector3 Velocity = new Vector3(0.0f, 0.0f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        /*CreateSceneParameters parameters = new CreateSceneParameters(LocalPhysicsMode.Physics3D);
        PredictionScene = SceneManager.CreateScene("PhysPredict", parameters);
        PredictionPhysScene = PredictionScene.GetPhysicsScene();
        CurrentScene = SceneManager.GetActiveScene();
        CurrentPhysScene = CurrentScene.GetPhysicsScene();
        Physics.autoSimulation = false;


        if (clone == null)
        {
            Debug.Log("ah");
            clone = Instantiate(this.gameObject);
            SceneManager.MoveGameObjectToScene(clone, PredictionScene);
        }

        lr = GetComponent<LineRenderer>();
        lr.positionCount = 100;*/
    }

    // Update is called once per frame
    void Update()
    {
        float velX = Input.GetAxis("Horizontal");
        float velZ = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * velX + transform.forward * velZ;

        controller.Move(movement * speed * Time.deltaTime);

        Velocity.y += gravity * Time.deltaTime;

        controller.Move(Velocity * Time.deltaTime);

        if (Input.GetKeyUp(KeyCode.Space))
        {
            item.useGravity = true;
            item.AddForce(new Vector3(0.0f, 10.0f, 0.0f), ForceMode.Impulse);
            item.AddForce(transform.forward * 10.0f, ForceMode.Impulse);
            item = null;
        }
    }

    /*void FixedUpdate()
    {
        if (CurrentPhysScene.IsValid())
        {
            CurrentPhysScene.Simulate(Time.fixedDeltaTime);
        }

        Vector3 force = new Vector3(10.0f, 10.0f, 0.0f);

        int Maxiteration = 100;

        clone.transform.position = transform.position;
        clone.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);

        for (int i = 0; i < Maxiteration; i++)
        {
            PredictionPhysScene.Simulate(Time.fixedDeltaTime);
            lr.SetPosition(i, clone.transform.position);
        }
    }*/
}
