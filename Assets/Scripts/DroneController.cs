using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    private const float terminalV = 6.0f;
    private const float maxA = 1.0f;
    private Rigidbody2D rigidbody2d;
    private Vector2 acceleration;
    private int isJammed;
    void Awake()
    {
        rigidbody2d = this.GetComponent<Rigidbody2D>();
        isJammed = 0;
    }
    void Update()
    {
        if(isJammed <= 0)
            acceleration = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * maxA;

        rigidbody2d.velocity += acceleration;

        rigidbody2d.velocity *= (terminalV - maxA) / terminalV;
        //rigidbody2d.velocity += (rigidbody2d.velocity) * (- rigidbody2d.velocity.magnitude / terminalV);
    }

    void OnTriggerEnter2D (Collider2D collider)
    {
        Obstacle colliderObstacle = collider.gameObject.GetComponent<Obstacle>();
        if(colliderObstacle != null && colliderObstacle.type == "jam")
        {
            isJammed += 1;
        }
    }
    
    IEnumerator Unjam()
    {
        yield return null;
        yield return new WaitForSeconds(0.10f);
        isJammed -= 1;
    }

    
    void OnTriggerExit2D (Collider2D collider)
    {
        Obstacle colliderObstacle = collider.gameObject.GetComponent<Obstacle>();
        if(colliderObstacle != null && colliderObstacle.type == "jam")
        {
            StartCoroutine(Unjam());
        }
    }
}
