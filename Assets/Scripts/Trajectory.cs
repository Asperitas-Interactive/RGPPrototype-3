using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trajectory : MonoBehaviour
{
    private Scene mainScene;
    private Scene physicsScene;

    public GameObject objectsToSpawn;

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
            ShowTrajectory();
        }

        mainScene.GetPhysicsScene().Simulate(Time.fixedDeltaTime);
    }

    public void PreparePhysicsScene()
    {
        SceneManager.SetActiveScene(physicsScene);
        g = GameObject.Instantiate(objectsToSpawn);
        g.transform.name = "ReferenceItem";
        g.GetComponent<DistractionItem>().isReference = true; 
        Destroy(g.GetComponent<MeshRenderer>());

        SceneManager.SetActiveScene(mainScene);
    }

    public void ShowTrajectory()
    {
        g.transform.rotation = objectsToSpawn.transform.rotation;
        g.GetComponent<Rigidbody>().useGravity = true;
        g.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 15.0f, 0.0f), ForceMode.Impulse);
        g.GetComponent<Rigidbody>().AddForce(transform.forward * 5.0f, ForceMode.Impulse);

        int steps = (int)(2f / Time.fixedDeltaTime);
        for(int i = 0; i < steps; i++)
        {
            physicsScene.GetPhysicsScene().Simulate(Time.fixedDeltaTime);
        }
    }
}
