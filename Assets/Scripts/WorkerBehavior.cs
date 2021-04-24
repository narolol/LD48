using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerBehavior : MonoBehaviour
{
    Vector3 movementVector;

    public float stepMultiplier;
    public float maxSpeed;

    private Rigidbody rb;

    public enum WorkerState
    {
        Idle,
        Seeking,
        Attacking
    }

    public WorkerState state;

    void Start()
    {
        state = WorkerState.Idle;

        rb = GetComponentInChildren<Rigidbody>();        

        if (Random.value >= 0.5f)
        {
            movementVector = new Vector3(1,0,0);
        }
        else
        {
            movementVector = new Vector3(-1,0,0);
        }

        state = WorkerState.Seeking;
    }

    // Update is called once per frame
    void Update()
    {   
        switch (state)
        {
            case WorkerState.Idle:
            
            break;

            case WorkerState.Seeking:

                AddMovementForce();

            break;

            
            default:
            break;
        }     
        
        
    }

    void FixedUpdate() 
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }    
    }

    void AddMovementForce()
    {
        rb.AddForce(movementVector*stepMultiplier, ForceMode.Acceleration);

    }
    
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Tiles")
        {
            state = WorkerState.Attacking;
            print("state ATK");
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.tag == "Tiles")
        {
            print("stay");
        }
    }
    

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.tag == "Tiles")
        {
            state = WorkerState.Seeking;
            print("state SEEK");
        }
    }
}
