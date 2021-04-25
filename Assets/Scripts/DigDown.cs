using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigDown : MonoBehaviour
{
  public Transform destination;

  public GameObject _player;

  public TMPro.TextMeshPro text;


    // Start is called before the first frame update
    void Start()
    {
        _player = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
          TeleportDown();
        }
    }

    void TeleportDown()
    {
      if (_player != null)
      {
        _player.transform.position = destination.position;
        _player.GetComponent<PlayerController>().Teleport();
        
      }

    }

    private void OnTriggerEnter(Collider other) 
    {
      if (other.gameObject.tag == "Player")  
      {
        _player = other.gameObject;
        _player.GetComponent<PlayerController>().canTeleport = true;
        text.gameObject.SetActive(true);
      }
    }

    private void OnTriggerExit(Collider other) 
    {
      if (other.gameObject.tag == "Player")  
      {
        _player.GetComponent<PlayerController>().canTeleport = false;
        _player = null;
        text.gameObject.SetActive(false);
      }
    }
}
