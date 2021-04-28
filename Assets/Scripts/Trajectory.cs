using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trajectory : MonoBehaviour
{
    //Tutorials used:
    //https://www.youtube.com/watch?v=4VUmhuhkELk
    //https://www.youtube.com/watch?v=GLu1T5Y2SSc

    private Scene mainScene;
    private Scene physicsScene;

    public PhysSceneCreator creator;

    public GameObject objectsToSpawn;
    public LineRenderer lr;

    public DistractionMove item;

    GameObject g;

    Vector3 ogpos;

    private void Start()
    {
        Physics.autoSimulation = false;
        mainScene = SceneManager.GetActiveScene();
        physicsScene = creator.physScene;

        PreparePhysicsScene();
    }

    private void FixedUpdate()
    {
        ogpos = objectsToSpawn.transform.position;
        if (Input.GetKey(KeyCode.Space) && item.attached == true)
        {
            lr.enabled = true;
            ShowTrajectory();
        } else
        {
            //lr.enabled = false;
        }

        mainScene.GetPhysicsScene().Simulate(Time.fixedDeltaTime);
    }

    public void PreparePhysicsScene()
    {
        SceneManager.SetActiveScene(physicsScene);
        g = GameObject.Instantiate(objectsToSpawn);
        g.transform.name = "ReferenceItem";
        Destroy(g.GetComponent<MeshRenderer>());
        Destroy(g.GetComponent<Trajectory>());
        SceneManager.SetActiveScene(mainScene);
    }

    public void ShowTrajectory()
    {
        g.transform.rotation = objectsToSpawn.transform.rotation;
        g.GetComponent<Rigidbody>().useGravity = true;
        g.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 5.0f, 0.0f), ForceMode.Impulse);
        g.GetComponent<Rigidbody>().AddForce(transform.forward * 15.0f, ForceMode.Impulse);

        int steps = (int)(1f / Time.fixedDeltaTime);
        lr.positionCount = steps;
        for(int i = 0; i < steps; i++)
        {
            physicsScene.GetPhysicsScene().Simulate(Time.fixedDeltaTime);
            lr.SetPosition(i, g.transform.position);
        }

        g.GetComponent<Rigidbody>().velocity = Vector3.zero;
        g.transform.position = ogpos;
    }
}
