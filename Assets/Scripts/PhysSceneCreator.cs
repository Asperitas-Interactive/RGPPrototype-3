using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhysSceneCreator : MonoBehaviour
{
    public Scene physScene;
    // Start is called before the first frame update
    void Start()
    {
        physScene = SceneManager.CreateScene("PhysicsScene", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
    }
}
