using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : BaseBullets
{
    
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<PlayerManager>())
        {
            other.transform.GetComponent<PlayerManager>().TakeDamage(20);
        }
        base.OnTriggerEnter(other);

    }
}
