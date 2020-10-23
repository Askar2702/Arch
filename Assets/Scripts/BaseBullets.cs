using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullets : MonoBehaviour
{
    protected void Start()
    {
        Destroy(gameObject, 1.5f);
    }
    
    

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
            Destroy(this.gameObject);
    }
}
