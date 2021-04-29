using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public GameObject otherPos;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<PlayerControl>().winCon == true)
            {
                SceneManager.LoadScene(3);
            }
            other.gameObject.GetComponent<PlayerControl>().Teleport(otherPos.transform.GetChild(0).gameObject.transform.position);
        }
    }
}
