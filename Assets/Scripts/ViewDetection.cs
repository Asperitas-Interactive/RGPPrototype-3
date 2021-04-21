using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewDetection : MonoBehaviour
{
    public List<GameObject> SightedObjects;

    private void OnTriggerEnter(Collider other)
    {
        SightedObjects.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        SightedObjects.Remove(other.gameObject);
    }
}
