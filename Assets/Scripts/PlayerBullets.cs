using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullets : BaseBullets
{
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.transform.GetComponent<BaseEnemy>())
        {
            other.transform.GetComponent<BaseEnemy>().TakeDamage(20);
        }
    }
}
