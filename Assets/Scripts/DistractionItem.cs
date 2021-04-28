using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractionItem : MonoBehaviour
{
    public bool isStatic;
    public bool isReference;

    private void Start()
    {
        if (isReference)
        {
            var TrajectoryScript = FindObjectOfType<Trajectory>();
        }
    }
}
