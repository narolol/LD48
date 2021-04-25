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

  [Space(10)]
  public float maxMana;
  public float currentMana;
  public GameObject manaBar;
  public float manaRegen;
  public float regenTickTimer;
  private float timeSinceManaTick;
  public float shootManaCost;
  
  [Space(5)]
  public float raiseUndeadManaCost;
  public float raiseUndeadRange;
  public GameObject undeadPrefab;

  [Space(5)]
  public float maxHealth;
  public float currentHealth;
  public GameObject HealthBar;
  [SerializeField]
  private float uiBarSize;
  
  [Space(5)]
  public int goldAmount;
  public TMPro.TextMeshPro goldUI;

  Plane _plane;



  // Start is called before the first frame update
  void Start()
  {
    rb =  GetComponent<Rigidbody>();        
    timeSinceManaTick = 0f;

  }

  void Update() 
  {
    GetMovementVectorFromInput();
    GetProjectileDestination();
    UpdateStatus();
    UpdateUI();
  }
  void FixedUpdate()
  { 
    AddMovementForce();
  }

  void UpdateStatus()
  {
    timeSinceManaTick += Time.deltaTime;

    if (timeSinceManaTick >= regenTickTimer)
    {
      currentMana += manaRegen;

      currentMana = currentMana>maxMana?maxMana:currentMana;

      timeSinceManaTick = 0f;
    }
  
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

    if (Input.GetMouseButtonDown(1))
    {
      RaiseUndead();
      // print("click");
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

    if (currentMana - shootManaCost >= 0)
    {
      currentMana -= shootManaCost;
      Vector3 shootDirection =  projectileDestination.position - projectileSource.position;
      GameObject go = Instantiate(projectilePrefab, projectileSource.position, Quaternion.identity);
      go.GetComponent<ProjectileBehavior>().Init(shootDirection);
    }  
  }

  void RaiseUndead()
  {
    if (currentMana - raiseUndeadManaCost >= 0)
    {
      // print("raise cost");

      Collider[] colliders = Physics.OverlapSphere(transform.position, raiseUndeadRange, 1 << 11, QueryTriggerInteraction.UseGlobal);

      foreach (Collider col in colliders)
      {
        // print("foreach raise");
          if (col.gameObject.tag == "Bones")
          {
            Vector3 zombieSpawnPosition = new Vector3(col.transform.position.x, col.transform.position.y + 0.5f, 5);
            Instantiate(undeadPrefab, zombieSpawnPosition, Quaternion.identity);
            Destroy(col.gameObject);
            // print("RISE FROM YOUR GRAVE");
            break;
          }
      }
    }
  }

  void UpdateUI()
  {
    HealthBar.transform.localScale = new Vector3(HealthBar.transform.localScale.x, (currentHealth/maxHealth) * uiBarSize, 0);
    manaBar.transform.localScale = new Vector3(manaBar.transform.localScale.x, (currentMana/maxMana) * uiBarSize, 0);
    goldUI.text = "Gold: " + goldAmount.ToString();
  }

  
}
