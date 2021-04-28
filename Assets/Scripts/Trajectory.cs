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

    public GameObject objectsToSpawn;
    public LineRenderer lr;

    GameObject g;

    private void Start()
    {
        Physics.autoSimulation = false;
        mainScene = SceneManager.GetActiveScene();
        physicsScene = SceneManager.CreateScene("PhysicsScene", new CreateSceneParameters(LocalPhysicsMode.Physics3D));

        PreparePhysicsScene();
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("ah");
            ShowTrajectory();
        }

        mainScene.GetPhysicsScene().Simulate(Time.fixedDeltaTime);
    }

    public void PreparePhysicsScene()
    {
        SceneManager.SetActiveScene(physicsScene);
        g = GameObject.Instantiate(objectsToSpawn);
        g.transform.name = "ReferenceItem";
        Destroy(g.GetComponent<MeshRenderer>());

        SceneManager.SetActiveScene(mainScene);
    }

    public void ShowTrajectory()
    {
        g.transform.rotation = objectsToSpawn.transform.rotation;
        g.GetComponent<Rigidbody>().useGravity = true;
        g.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 5.0f, 0.0f), ForceMode.Impulse);
        g.GetComponent<Rigidbody>().AddForce(transform.forward * 15.0f, ForceMode.Impulse);

        int steps = (int)(3f / Time.fixedDeltaTime);
        lr.positionCount = steps;
        for(int i = 0; i < steps; i++)
        {
            physicsScene.GetPhysicsScene().Simulate(Time.fixedDeltaTime);
            lr.SetPosition(i, g.transform.position);
        }
    }
}
