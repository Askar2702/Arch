using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemystill : BaseEnemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        this.Player = GameObject.FindGameObjectWithTag("Player"); 
        Player.GetComponent<PlayerManager>().AddEnemy(this.gameObject);
        InvokeRepeating("projectile", TimeStart, interval);
    }


    // Update is called once per frame
    protected override void FixedUpdate()
    {
        if (!Player) return;
        Vector3 vectorToTarget = Player.transform.position - transform.position;
        transform.eulerAngles = new Vector3(0, Mathf.Atan2(vectorToTarget.x, vectorToTarget.z) * 180 / Mathf.PI, 0);
    }
}
