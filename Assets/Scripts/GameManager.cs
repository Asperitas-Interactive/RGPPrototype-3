using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    float resetTimer;
    bool reset;
    private void Update()
    {
        resetTimer -= Time.deltaTime;

        if(reset && resetTimer<0.0f)
        {
            Time.timeScale = 1.0f;
        }
    }
    // Start is called before the first frame update
    public void time(float fac, float res)
    {
        Time.timeScale = fac;
        resetTimer = res;
        reset = true;
    }
}
