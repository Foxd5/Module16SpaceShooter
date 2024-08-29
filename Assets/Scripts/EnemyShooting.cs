using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject EnemyBulletPrefab;       
    public Transform EnemyfirePoint;           
    public float bulletSpeed = 5f;        
    public float fireRate = 1.5f;         
    private float nextFireTime = 0f;      

    void Update()
    {
        
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot() //copied my shoot funciton from shipshooting for player's ship
    {
        GameObject enemybullet = Instantiate(EnemyBulletPrefab, EnemyfirePoint.position, EnemyfirePoint.rotation);
      
        Rigidbody2D rb = enemybullet.GetComponent<Rigidbody2D>();

        Rigidbody2D shipRb = GetComponent<Rigidbody2D>();
      
        Vector2 bulletVelocity = new Vector2(EnemyfirePoint.right.x, EnemyfirePoint.right.y) * bulletSpeed; 
        rb.velocity = bulletVelocity;
      
    }
}
