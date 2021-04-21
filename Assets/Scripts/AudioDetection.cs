using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDetection : MonoBehaviour
{
    public List<GameObject> HeardObjects;
    public bool hasHeard = false;

    private void OnTriggerEnter(Collider other)
    {
        HeardObjects.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        HeardObjects.Remove(other.gameObject);
    }
}
