using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewDetection : MonoBehaviour
{
    public List<GameObject> SightedObjects;
    public float radius = 2.0f;

    private void Update()
    {
        //Vector3 rayOrigin = transform.position;
        //RaycastHit hit;
        //if (Physics.Raycast(rayOrigin, transform.forward, out hit, 100))
        //{
        //    Debug.Log(hit.collider.gameObject);
        //    //    GetComponentInParent<AIControl>().collision(other);
        //}
    }



    private void OnTriggerEnter(Collider other)
    {
        GetComponentInParent<AIControl>().collision(other);
    }

    private void OnTriggerExit(Collider other)
    {

    }
}
