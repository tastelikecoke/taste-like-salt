﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    private const float terminalV = 10.0f;
    private const float maxA = 0.25f;
    private Rigidbody2D rigidbody2d;
    private Vector2 acceleration;
    public Transform droneSprite;
    public Transform droneDirectioner;
    private int isJammed;
    private bool isStopped;
    public bool IsStopped
    {
        get { return isStopped; }
    }

    void Awake()
    {
        rigidbody2d = this.GetComponent<Rigidbody2D>();
        isJammed = 0;
    }

    void FixedUpdate()
    {
        if(isStopped)
        {
            droneSprite.GetComponent<SpriteRenderer>().color = Color.gray;
            droneSprite.transform.localRotation = Quaternion.Euler(new Vector3(rigidbody2d.velocity.y, -rigidbody2d.velocity.x, 0) * 5f);
            rigidbody2d.velocity  *= 0.95f;
            return;
        }

        
        droneSprite.GetComponent<SpriteRenderer>().color = Color.white;
        if(acceleration.magnitude >= 0.0001)
        {
            droneDirectioner.gameObject.SetActive(true);
            Quaternion rotation = Quaternion.LookRotation(new Vector3(acceleration.x, acceleration.y, 0), Vector3.back);
            rotation.x = 0;
            rotation.y = 0;
            droneDirectioner.localRotation = rotation;
        }
        else
        {
            droneDirectioner.gameObject.SetActive(false);
        }

        if(isJammed <= 0)
        {
            acceleration = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * maxA;
            if(Input.GetKey(KeyCode.Space))
            {
                acceleration *= 2.50f;
            }
        }
        else
        {
            Color spriteColor = droneSprite.GetComponent<SpriteRenderer>().color;
            spriteColor.a = 0.5f;
            droneSprite.GetComponent<SpriteRenderer>().color = spriteColor;
        }

        rigidbody2d.velocity += acceleration;
        //rigidbody2d.AddForce(acceleration);
        Debug.Log(acceleration);

        droneSprite.transform.localRotation = Quaternion.Euler(new Vector3(rigidbody2d.velocity.y, -rigidbody2d.velocity.x, 0) * 5f);
        rigidbody2d.velocity  *= (terminalV - maxA) / terminalV;
        //rigidbody2d.velocity += (rigidbody2d.velocity) * (- rigidbody2d.velocity.magnitude / terminalV);
    }

    void OnTriggerEnter2D (Collider2D collider)
    {
        Obstacle colliderObstacle = collider.gameObject.GetComponent<Obstacle>();
        if(colliderObstacle != null && colliderObstacle.type == "jam")
        {
            StartCoroutine(Jam());
        }
        if(colliderObstacle != null && colliderObstacle.type == "spike")
        {
            Scoring.instance.CurrentLevel.spawner.Restart();
        }
    }
    
    public void Stop(GameObject newDrone)
    {
        isStopped = true;
    }
    
    IEnumerator Jam()
    {
        yield return null;
        yield return new WaitForSeconds(0.10f);
        isJammed += 1;
    }
    IEnumerator Unjam()
    {
        yield return null;
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
