using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewDetection : MonoBehaviour
{
    public List<GameObject> SightedObjects;
    public float radius = 2.0f;
    

    
    
    
    private void OnTriggerEnter(Collider other)
    {
        GetComponentInParent<AIControl>().collision(other);
    }

    private void OnTriggerExit(Collider other)
    {

    }
}
