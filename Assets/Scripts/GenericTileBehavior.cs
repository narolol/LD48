using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericTileBehavior : MonoBehaviour
{
    // Start is called before the first frame update

  public float HP;
  public GameObject lootPrefab;
   



  void Update() 
  {
      
  }
  public void TakeDamage(float d) 
  {
    HP -= d;

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
      GameObject _loot = Instantiate(lootPrefab, transform.position, Quaternion.identity);
      _loot.GetComponent<Rigidbody>().AddForce(new Vector3(16f * (Random.value + 0.2f), 33f * (Random.value +1f), 0));
      _loot.GetComponent<Rigidbody>().AddTorque(new Vector3(16f * (Random.value + 0.2f), 33f * (Random.value + 0.2f), 16f * (Random.value + 0.2f)));
    }
    transform.position = new Vector3(0,-1000,0);
  }
    
}
