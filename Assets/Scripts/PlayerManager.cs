using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerManager : MonoBehaviour
{
    public float Speed;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float AngularSpeed;
    [SerializeField]
    private float health;
    [SerializeField]
    private Slider HealthSlider;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Transform startBullet;
    [SerializeField]
    private float forseBullet;
    [SerializeField]
    private float TimeStart;
    [SerializeField]
    private float interval;
    [SerializeField]
    private GameObject target;
    private Vector3 Move;
    GameObject[] targets;
    [SerializeField]
    private UnityEvent Death;


    void Start()
    {
        HealthSlider.value = health;
        targets = GameObject.FindGameObjectsWithTag("Enemy");
        target = gameObject;
    }
    private void Update()
    {
        float DistancetoClosesEnemy = Mathf.Infinity;
        foreach (GameObject currentEnemy in targets)
        {
            float DistanceEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if (DistanceEnemy < DistancetoClosesEnemy)
            {
                DistancetoClosesEnemy = DistanceEnemy;
                target = currentEnemy;

            }
            Debug.DrawLine(this.transform.position, target.transform.position);
        }
        if (!Joystick.isJoystick)
        {
            if (target != null)
            {
                if (Time.time > TimeStart)
                {
                    projectile();
                    TimeStart = Time.time + interval;
                }
            }
            else
                return;

        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (Joystick.isJoystick)
        {
            Move = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal") * Speed, 0f, CrossPlatformInputManager.GetAxis("Vertical") * Speed);
            transform.eulerAngles = new Vector3(0, Mathf.Atan2(Move.x, Move.z) * 180 / Mathf.PI, 0);
            rb.velocity = Move;
        }
        else
        {
            var vector = target.transform.position - transform.position;
            transform.eulerAngles = new Vector3(0, Mathf.Atan2(vector.x, vector.z) * 180 / Mathf.PI, 0);
        }        
        
    }
    
    public void TakeDamage(float amount)
    {
        health -= amount;
        HealthSlider.value = health;
        if (health <= 0)
        {
            Death.Invoke();
            Destroy(this.gameObject);
        }
    }
    public void projectile()
    {
        if (!Joystick.isJoystick)
        {
            GameObject Bullet = Instantiate(bullet, startBullet.position, startBullet.rotation);
            Bullet.GetComponent<Rigidbody>().AddForce(startBullet.transform.forward * forseBullet, ForceMode.VelocityChange);
        }
    }
}
