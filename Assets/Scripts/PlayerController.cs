using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 movementVector;
    public float stepMultiplier;
    Rigidbody rb;

    bool isHoldingRight;
    bool isHoldingLeft;
    public Vector3 jumpForceVector;

    public Transform projectileSource;
    public Transform projectileDestination;

    public Camera mainCam;

    public GameObject projectilePrefab;




    // Start is called before the first frame update
    void Start()
    {
        rb =  GetComponent<Rigidbody>();        
    }

    // Update is called once per frame
    void Update()
    {
        GetMovementVectorFromInput();

        AddMovementForce();

        GetProjectileDestination();


    }

    void GetMovementVectorFromInput()
    {
        

        movementVector = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.D))
        {
            isHoldingRight = true;        
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
           isHoldingLeft = true;
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            isHoldingRight = false;
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            isHoldingLeft = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddJumpForce();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
        


        movementVector = (isHoldingRight?new Vector3(1,0,0):Vector3.zero) + (isHoldingLeft?new Vector3(-1,0,0):Vector3.zero);

    }

    void AddMovementForce()
    {
        rb.AddForce(movementVector*stepMultiplier, ForceMode.Acceleration);

    }

    void AddJumpForce()
    {
        rb.AddForce(jumpForceVector, ForceMode.Impulse);
    }

    void GetProjectileDestination()
    {
        Vector3 mouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 7);
        

        Vector3 toWorld = mainCam.ScreenToWorldPoint(mouse);
        toWorld.z = 5;

        projectileDestination.position = toWorld;
    }

    void Fire()
    {
        Vector3 shootDirection =  projectileDestination.position - projectileSource.position;

        GameObject go = Instantiate(projectilePrefab, projectileSource.position, Quaternion.identity);

        go.GetComponent<ProjectileBehavior>().Init(shootDirection);

    }

    
}
