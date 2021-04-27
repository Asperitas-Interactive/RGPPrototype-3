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
    // Start is called before the first frame update
    void Start()
    {
        CreateSceneParameters parameters = new CreateSceneParameters(LocalPhysicsMode.Physics3D);
        PredictionScene = SceneManager.CreateScene("PhysPredict", parameters);
        PredictionPhysScene = PredictionScene.GetPhysicsScene();
        CurrentScene = SceneManager.GetActiveScene();
        CurrentPhysScene = CurrentScene.GetPhysicsScene();
        Physics.autoSimulation = false;


        if (clone == null)
        {
            Debug.Log("ah");
            clone = Instantiate(CloneBase.gameObject);
            SceneManager.MoveGameObjectToScene(clone, PredictionScene);
            //clone.GetComponent<Rigidbody>().useGravity = false;
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

        Vector3 force = new Vector3(10.0f, 10.0f, 0.0f);

        int Maxiteration = 100;

        clone.transform.position = transform.position;
        clone.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);

        for (int i = 0; i < Maxiteration; i++)
        {
           PredictionPhysScene.Simulate(Time.fixedDeltaTime);
           lr.SetPosition(i, clone.transform.position);
        }

        clone.transform.position = transform.position;
    }
}
