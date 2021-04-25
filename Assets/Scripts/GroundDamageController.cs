using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDamageController : MonoBehaviour
{
  public GameObject _playerRef;

  public float timeToDoDamage;
  private float timeElapsed;

  public float damagePerSecond;
  private float timeSinceLastDamage;

  public GameObject particles;
  public Material groundFire;

  public GameObject tutorialTime;
  private bool canCount;
  void Start()
  {
    canCount = false;
    timeElapsed = 0f;
    timeSinceLastDamage = 0f;
  }

  // Update is called once per frame
  void Update()
  { 
    canCount = !tutorialTime.GetComponent<TutorialTime>().isInTutorial;

    if (canCount)
    {
      timeElapsed += Time.deltaTime;



      if (timeElapsed > timeToDoDamage)
      {
        if (!particles.active)
        {
          particles.SetActive(true);
          GetComponent<MeshRenderer>().material = groundFire;
        }
        timeSinceLastDamage += Time.deltaTime;

        if (timeSinceLastDamage >= 1f)
        {
          DealDamage();
          timeSinceLastDamage = 0f;
        }
      }
    }
  }

  private void OnCollisionEnter(Collision other) 
  {
    if (other.gameObject.tag == "Player")
    {
      other.gameObject.GetComponent<PlayerController>().canRefreshJump = true;
    }
  }

  void OnTriggerEnter(Collider other) {
    if (other.gameObject.tag == "Player")  
    {
      _playerRef = other.gameObject;
    }
  }

  private void OnTriggerExit(Collider other) 
  {
    if (other.gameObject.tag == "Player")  
    {
      _playerRef = null;
    }
  }

  private void OnCollisionExit(Collision other) 
  {
    if (other.gameObject.tag == "Player")
    {
      other.gameObject.GetComponent<PlayerController>().canRefreshJump = false;
    }
  }

  void DealDamage()
  {
    if (_playerRef != null)
    {
      _playerRef.GetComponent<PlayerController>().TakeDamage(damagePerSecond);
    }
  }


}
