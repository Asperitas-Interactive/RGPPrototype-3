using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    CharacterController controller;
    [SerializeField]
    float speed, gravity;

    public Rigidbody item;

    private bool equip;
    private Vector3 Velocity = new Vector3(0.0f, 0.0f, 0.0f);

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

        Velocity.y += gravity * Time.deltaTime;

        controller.Move(Velocity * Time.deltaTime);

        if (Input.GetKeyUp(KeyCode.Space) && item != null)
        {
            item.useGravity = true;
            item.AddForce(new Vector3(0.0f, 5.0f, 0.0f), ForceMode.Impulse);
            item.AddForce(transform.forward * 15.0f, ForceMode.Impulse);
            item.transform.SetParent(null);
            item = null;
        }
    }

    public void setRB(Rigidbody rb) {
        if (item == null)
        {
            item = rb;
            item.transform.SetParent(gameObject.transform);
        }
    }
}
