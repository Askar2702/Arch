using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField]
    protected GameObject bullet;
    [SerializeField]
    protected Transform startBullet;
    [SerializeField]
    protected float TimeStart;
    [SerializeField]
    protected float interval;
    [SerializeField]
    protected Rigidbody rb;
   
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float forseBullet;
    protected GameObject Player;

  
    [SerializeField]
    protected float StopDistance;
    [SerializeField]
    protected float retreatDistance;
    [SerializeField]
    protected float health;
    [SerializeField]
    protected Slider HealthSlider;

    protected virtual void Start()
    {
        this.Player = GameObject.FindGameObjectWithTag("Player");
        Player.GetComponent<PlayerManager>().AddEnemy(this.gameObject);
        InvokeRepeating("projectile", TimeStart, interval);
        InvokeRepeating("RandomMove", 1, 1f);
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        if (Player != null)
        {
            if (Vector2.Distance(transform.position, Player.transform.position) > StopDistance)
            {
                rb.transform.position = Vector3.MoveTowards(rb.transform.position, Player.transform.position, speed * Time.deltaTime);
            }
            else if (Vector3.Distance(transform.position, Player.transform.position) < StopDistance && (Vector3.Distance(transform.position, Player.transform.position) > retreatDistance))
            {
                rb.transform.position = this.rb.transform.position;
            }
            else if (Vector3.Distance(transform.position, Player.transform.position) < retreatDistance)
            {
                rb.transform.position = Vector3.MoveTowards(rb.transform.position, Player.transform.position, -speed * Time.deltaTime);
            }

            Vector3 vectorToTarget = Player.transform.position - transform.position;
            
            transform.eulerAngles = new Vector3(0, Mathf.Atan2(vectorToTarget.x, vectorToTarget.z) * 180 / Mathf.PI, 0);
        }
    }

    public void projectile()
    {
        if (Player != null)
        {
            GameObject Bullet = Instantiate(bullet, startBullet.position, startBullet.rotation);
            Bullet.GetComponent<Rigidbody>().AddForce(startBullet.transform.forward * forseBullet, ForceMode.VelocityChange);
        }
    }

    public void RandomMove()
    {
        int one;
        one = Random.Range(0, 4);
        if (one == 1)
            rb.velocity = new Vector3(rb.transform.position.x,0f, speed);
        else if (one == 2)
            rb.velocity = new Vector3(rb.transform.position.x, 0f, -speed);
        else if (one == 3)
            rb.velocity = new Vector3(speed, 0, rb.transform.position.z);
        else if (one == 4)
            rb.velocity = new Vector3(-speed, 0, rb.transform.position.z);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        HealthSlider.value = health;
        if (health <= 0)
        {
            Player.GetComponent<PlayerManager>().RemoveEnemy(this.gameObject);
            Destroy(this.gameObject);
        }

    }
}
