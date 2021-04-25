using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBehavior : MonoBehaviour
{
  [FMODUnity.EventRef]
  public string goldEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) 
    {
      if (other.gameObject.tag == "Player")  
      {
        FMODUnity.RuntimeManager.PlayOneShot(goldEvent, transform.position);
        other.GetComponent<PlayerController>().goldAmount += 1;
        Destroy(this.gameObject);
      }
    }
}
