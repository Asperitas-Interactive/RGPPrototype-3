using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AIStationary : MonoBehaviour
{
    float watchTimer = 5.0f;
    bool quit;
    private GameObject target;
    public bool death = false;
    public float alarmTimer = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (gameObject.tag == "Dead")
        {
            transform.GetChild(0).GetComponent<Animator>().SetBool("death", true);
            return;
        }
        watchTimer -= Time.deltaTime;

        if(quit && watchTimer < 0.0f)
            SceneManager.LoadScene(1);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "Dead")
            return;

        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(1);
        }

        

        if (other.gameObject.tag == "Dead")
        {

            quit = true;
            watchTimer = alarmTimer;

        }

           // Debug.Log("Ene");



    }
}
