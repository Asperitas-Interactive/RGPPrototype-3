using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialougeP1Control : MonoBehaviour
{
    public Text dialouge;
    public TextHolder holder;
    public int textNum = 0;
    public bool enable = false;

    private float enabledTimer = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enabledTimer -= Time.deltaTime;

        if(enabledTimer <= 0.0f)
        {
            enable = false;
        }

        if (enable)
        {
            dialouge.text = holder.texts[textNum];
        } else
        {
            dialouge.text = "";
        }
    }

    public void AssignText(int newText)
    {
        textNum = newText;
        enable = true;
        enabledTimer = 5.0f;
    }
}
