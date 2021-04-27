using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This code is from https://www.youtube.com/watch?v=GLu1T5Y2SSc&t=912s
//So we can realise proof of concept faster

public struct registeredItems
{
    public DistractionItem real;
    public DistractionItem hidden;
}

public class Trajectory : MonoBehaviour
{
    public static bool held;
    public GameObject distraction;
    public Transform referenceDistraction;

    private Scene mainScene;
    private Scene physicsScene;
    public GameObject marker;
    private List<GameObject> markers = new List<GameObject>();

    private Dictionary<string, registeredItems> allItems = new Dictionary<string, registeredItems>();

    public GameObject objectsToSpawn;

    private void Start()
    {
        Physics.autoSimulation = false;
        mainScene = SceneManager.GetActiveScene();
        physicsScene = SceneManager.CreateScene("PhysicsScene", new CreateSceneParameters(LocalPhysicsMode.Physics3D));

        PreparePhysicsScene();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ShowTrajectory();
        }

        mainScene.GetPhysicsScene().Simulate(Time.fixedDeltaTime);
    }

    public void RegisterDistraction(DistractionItem distraction)
    {
        if (!allItems.ContainsKey(distraction.gameObject.name)){
            allItems[distraction.gameObject.name] = new registeredItems();
        }

        var items = allItems[distraction.gameObject.name];

        if(string.Compare(distraction.gameObject.scene.name, physicsScene.name) == 0)
        {
            items.hidden = distraction;
        } else
        {
            items.real = distraction;
        }

        allItems[distraction.gameObject.name] = items;
    }

    public void PreparePhysicsScene()
    {
        SceneManager.SetActiveScene(physicsScene);
        GameObject g = GameObject.Instantiate(objectsToSpawn);
        g.transform.name = "ReferenceItem";
        g.GetComponent<DistractionItem>().isReference = true;
        Destroy(g.GetComponent<MeshRenderer>());

        SceneManager.SetActiveScene(mainScene);
    }

    public void CreateMarkers()
    {
        foreach(var ItemType in allItems)
        {
            var items = ItemType.Value;

            DistractionItem hidden = items.hidden;

            GameObject g = GameObject.Instantiate(marker, hidden.transform.position, Quaternion.identity);
            g.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            markers.Add(g);
        }
    }

    public void ShowTrajectory()
    {
        SyncItem();

        allItems["ReferenceItem"].hidden.transform.rotation = referenceDistraction.transform.rotation;
        allItems["ReferenceItem"].hidden.GetComponent<Rigidbody>().velocity = referenceDistraction.transform.TransformDirection(Vector3.up * 15f);
        allItems["ReferenceItem"].hidden.GetComponent<Rigidbody>().useGravity = true;

        int steps = (int)(2f / Time.fixedDeltaTime);
        for(int i = 0; i < steps; i++)
        {
            physicsScene.GetPhysicsScene().Simulate(Time.fixedDeltaTime);
            CreateMarkers();
        }
    }

    public void SyncItem()
    {
        foreach (var ItemType in allItems)
        {
            var items = ItemType.Value;

            DistractionItem visual = items.real;
            DistractionItem hidden = items.hidden;

            var rb = hidden.GetComponent<Rigidbody>();

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            hidden.transform.position = visual.transform.position;
            hidden.transform.rotation = visual.transform.rotation;
        }
    }
}
