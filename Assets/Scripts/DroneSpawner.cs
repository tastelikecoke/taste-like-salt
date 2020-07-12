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
    public int lostDrones = 0;
    void Awake()
    {
        oldDrones = new List<DroneController>();
    }
    
    public void Initialize()
    {
        originalDrone.gameObject.SetActive(false);
        Restart();
        lostDrones = 0;
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
        newDrone.transform.SetParent(this.transform);
        newDrone.transform.position = this.transform.position;
        if(currentDrone != null)
        {
            currentDrone.Stop(newDrone);
            oldDrones.Add(currentDrone);
            int fadecount = 0;
            
            foreach(DroneController droneController in oldDrones)
            {
                droneController.SetDeadAlpha(Mathf.Clamp(1.0f - oldDrones.Count * 0.09f + fadecount * 0.10f, 0.0f, 1.0f));
                if(droneController != null)
                    Physics2D.IgnoreCollision(newDrone.GetComponent<Collider2D>(), droneController.gameObject.GetComponent<Collider2D>());
                fadecount += 1;
            }
            if(oldDrones.Count > 10)
            {
                Destroy(oldDrones[0].gameObject);
                oldDrones.RemoveAt(0);
            }
        }
        currentDrone = newDrone.GetComponent<DroneController>();
        currentDrone.GetComponent<Rigidbody2D>().velocity += initialVelocity;
        lostDrones += 1;
        AudioController.instance.JustBeep();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            AudioController.instance.Fall();
            Restart();
        }
    }
}
