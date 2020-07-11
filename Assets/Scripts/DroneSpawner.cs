using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneSpawner : MonoBehaviour
{
    [SerializeField]
    private DroneController originalDrone;
    private DroneController currentDrone;
    private List<DroneController> oldDrones;
    public Vector2 initialVelocity;
    void Awake()
    {
        oldDrones = new List<DroneController>();
    }
    
    void Start()
    {
        originalDrone.gameObject.SetActive(false);
        Restart();
    }
    public void Die()
    {
        oldDrones.Add(currentDrone);
        foreach(DroneController controller in oldDrones)
        {
            Destroy(controller.gameObject);
        }
    }
    public void Restart()
    {
        GameObject newDrone = Instantiate(originalDrone.gameObject);
        newDrone.SetActive(true);
        newDrone.transform.position = this.transform.position;
        if(currentDrone != null)
        {
            currentDrone.Stop(newDrone);
            oldDrones.Add(currentDrone);
            foreach(DroneController droneController in oldDrones)
            {
                Physics2D.IgnoreCollision(newDrone.GetComponent<Collider2D>(), droneController.gameObject.GetComponent<Collider2D>());
            }
        }
        currentDrone = newDrone.GetComponent<DroneController>();
        currentDrone.GetComponent<Rigidbody2D>().velocity += initialVelocity;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }
}
