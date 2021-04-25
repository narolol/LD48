using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    private Vector3 direction = Vector3.zero;

    public float speedMultiplyer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (direction.normalized * speedMultiplyer);
    }

    public void Init(Vector3 vector)
    {
        direction = vector;
    }


    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.tag == "Tiles")
        {
          other.gameObject.GetComponentInParent<GenericTileBehavior>().TakeDamage(1f);
          print("DMG !!!!");            
          Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "Ground")
        {
          Destroy(this.gameObject);
        }
    }
}
