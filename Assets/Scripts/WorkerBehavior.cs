using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerBehavior : MonoBehaviour
{
    Vector3 movementVector;

    public float stepMultiplier;
    public float maxSpeed;
    public float damage;
    public float atkSpd;
    private float timeSinceAtk;
    
    private GameObject _workerTarget;

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

        timeSinceAtk = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        switch (state)
        {
            case WorkerState.Idle:
            
            break;

            case WorkerState.Seeking:

                AddMovementForce();
                timeSinceAtk = 0f;

            break;

            case WorkerState.Attacking:
                //
                timeSinceAtk += Time.deltaTime;

                if (timeSinceAtk >= atkSpd)
                {
                    Attack();
                    print("ataquei");
                    timeSinceAtk = 0f;
                }


            break;

            
            default:
            break;
        }     

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
            print("ATK tile");
            _workerTarget = other.gameObject;
            Attack();
            
        }

        if (other.gameObject.tag == "Enemy")
        {
            state = WorkerState.Attacking;
            print("ATK Enemy");
            _workerTarget = other.gameObject;
        }

        
    }

    private void OnTriggerStay(Collider other) 
    {
    }
    

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.tag == "Tiles")
        {
            state = WorkerState.Seeking;
            print("state SEEK");
            
        }

        if (other.gameObject.tag == "Enemy")
        {

        }
    }

    public void Attack()
    {
        if (_workerTarget.tag == "Tiles")
        {
            _workerTarget.GetComponentInParent<GenericTileBehavior>().TakeDamage(damage);
        }

        if (_workerTarget.tag == "Enemy")
        {

        }
    }
}
