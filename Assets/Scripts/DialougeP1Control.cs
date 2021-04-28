using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialougeP1Control : MonoBehaviour
{
    public Text dialouge;
    public TextHolder holder;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dialouge.text = holder.texts[0];
    }
}
