using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    CharacterController controller;
    [SerializeField]
    float speed, gravity;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float velX = Input.GetAxis("Horizontal");
        float velZ = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * velX + transform.forward * velZ;

        controller.Move(movement * speed * Time.deltaTime);
    }
}
