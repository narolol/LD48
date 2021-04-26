using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericTileBehavior : MonoBehaviour
{
    // Start is called before the first frame update

  public float HP;
  public GameObject lootPrefab;
  public int lootMultiplier;


  [FMODUnity.EventRef]
  public string takeDamageEvent;
  [FMODUnity.EventRef]  
  public string breakEvent;

    void Update() 
  {
      
  }
  public void TakeDamage(float d) 
  {
    HP -= d;
    GetComponent<Animator>().Play("BlockTakeDamage");
    FMODUnity.RuntimeManager.PlayOneShot(takeDamageEvent, transform.position);

    if (HP <= 0f)
    {           
      //gameObject.SetActive(false);
      Die();
    }

  }

  public void Die()
  {       
    print(" morreu");
    
    if (lootPrefab != null)
    {  
      FMODUnity.RuntimeManager.PlayOneShot(breakEvent, transform.position);
      for (int i = 0; i < lootMultiplier; i++)
      {
        GameObject _loot = Instantiate(lootPrefab, transform.position, Quaternion.identity);
        _loot.GetComponent<Rigidbody>().AddForce(new Vector3(16f * (Random.value + 0.2f), 33f * (Random.value +1f), 0));
        _loot.GetComponent<Rigidbody>().AddTorque(new Vector3(16f * (Random.value + 0.2f), 33f * (Random.value + 0.2f), 16f * (Random.value + 0.2f)));          
      }
    }
    transform.position = new Vector3(0,-1000,0);
  }

  private void OnCollisionEnter(Collision other) 
  {
    if (other.gameObject.tag == "Player") 
    {
      //other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.down * 100f, ForceMode.Impulse);
    }
  }
    
}
