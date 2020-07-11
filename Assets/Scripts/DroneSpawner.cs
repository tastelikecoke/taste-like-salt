using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneSpawner : MonoBehaviour
{
    [SerializeField]
    private DroneController originalDrone;
    private DroneController currentDrone;
    private List<DroneController> oldDrones;
    public static DroneSpawner m_instance;
    public static DroneSpawner instance
    {
        get
        {
            if(m_instance == null) m_instance = new DroneSpawner();
            return m_instance;
        }
    }
    void Awake()
    {
        m_instance = this;
        oldDrones = new List<DroneController>();
    }
    
    void Start()
    {
        originalDrone.gameObject.SetActive(false);
        Restart();
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
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }
}
