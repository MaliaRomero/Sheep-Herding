using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 15f;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Ensures Rigidbody is assigned
    }
    
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical).normalized;

        rb.velocity = movement * speed;

    }
}