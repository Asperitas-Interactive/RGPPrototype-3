using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DistractionControl : MonoBehaviour
{
    Scene PredictionScene;
    PhysicsScene PredictionPhysScene;
    Scene CurrentScene;
    PhysicsScene CurrentPhysScene;
    GameObject clone;
    public LineRenderer lr;

    int Maxiteration = 5;

    Vector3 force = new Vector3(0.0f, -10.0f, 0.0f);

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
            clone = Instantiate(this.gameObject);
            SceneManager.MoveGameObjectToScene(clone, PredictionScene);
        }

        lr = GetComponent<LineRenderer>();
        lr.positionCount = Maxiteration;

        clone.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        clone.transform.position = transform.position;

    }

    void FixedUpdate()
    {
        if (CurrentPhysScene.IsValid())
        {
            CurrentPhysScene.Simulate(Time.fixedDeltaTime);
        }

        for (int i = 0; i < Maxiteration; i++)
        {
            PredictionPhysScene.Simulate(Time.fixedDeltaTime);
            lr.SetPosition(i, clone.transform.position);
        }
    }
}
