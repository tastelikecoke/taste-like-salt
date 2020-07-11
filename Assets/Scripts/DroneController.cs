using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    private const float terminalV = 7.5f;
    private const float maxA = 0.025f;
    private Rigidbody2D rigidbody2d;
    private Vector2 acceleration;
    private bool isJammed;
    void Awake()
    {
        rigidbody2d = this.GetComponent<Rigidbody2D>();
        isJammed = false;
    }
    void Update()
    {
        if(!isJammed)
            acceleration = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * maxA;

        rigidbody2d.velocity += acceleration;

        rigidbody2d.velocity *= (terminalV - maxA) / terminalV;
        //rigidbody2d.velocity += (rigidbody2d.velocity) * (- rigidbody2d.velocity.magnitude / terminalV);
    }

    void OnTriggerEnter2D (Collider2D collider)
    {
        isJammed = true;
    }

    
    void OnTriggerExit2D (Collider2D collider)
    {
        isJammed = false;
    }
}
