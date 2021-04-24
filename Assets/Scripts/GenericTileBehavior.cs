using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericTileBehavior : MonoBehaviour
{
    // Start is called before the first frame update

    public float HP;


    void Update() 
    {
        if (HP <= 0f)
        {
            print(" morreu");
            gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }
    public void TakeDamage(float d) 
    {
        HP -= d;
    }
    
}
