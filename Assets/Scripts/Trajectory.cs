using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trajectory : MonoBehaviour
{
   Scene PredictionScene;
   PhysicsScene PredictionPhysScene;
   Scene CurrentScene;
   PhysicsScene CurrentPhysScene;
   public GameObject CloneBase;
   GameObject clone;
   public LineRenderer lr;

    Vector3 force = new Vector3(10.0f, 1000.0f, 0.0f);
    // Start is called before the first frame update
    void Start()
    {
        CreateSceneParameters parameters = new CreateSceneParameters(LocalPhysicsMode.Physics3D);
        PredictionScene = SceneManager.CreateScene("PhysPredict", parameters);
        PredictionPhysScene = PredictionScene.GetPhysicsScene();
        CurrentScene = SceneManager.GetActiveScene();
        CurrentPhysScene = CurrentScene.GetPhysicsScene();
        Physics.autoSimulation = false;

        Debug.Log(CurrentScene.GetPhysicsScene());

        if (clone == null)
        {
            Debug.Log("ah");
            clone = Instantiate(CloneBase.gameObject);
            SceneManager.MoveGameObjectToScene(clone, PredictionScene);
            clone.GetComponent<Rigidbody>().useGravity = false;
            clone.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        }

        lr = GetComponent<LineRenderer>();
        lr.positionCount = 100;
    }

    void FixedUpdate()
    {
        if (CurrentPhysScene.IsValid())
        {
            CurrentPhysScene.Simulate(Time.fixedDeltaTime);
        }

        int Maxiteration = 100;

        clone.transform.position = transform.position;

        for (int i = 0; i < Maxiteration; i++)
        {
           PredictionPhysScene.Simulate(Time.fixedDeltaTime);
           lr.SetPosition(i, clone.transform.position);
        }

        clone.transform.position = transform.position;
    }
}
