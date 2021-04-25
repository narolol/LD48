using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTime : MonoBehaviour
{
  public bool isInTutorial;

    void Start()
    {
      isInTutorial = true;      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) 
    {
      if (other.gameObject.tag == "Player")
      {
        isInTutorial = false;
      }
    }
}
