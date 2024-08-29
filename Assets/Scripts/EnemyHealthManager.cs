using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public float healthAmount = 100f;
    public GameObject enemyexplodePrefab;
    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        Debug.Log("Enemy Health: " + healthAmount);

        if(healthAmount <= 0)
        {

            Die();
        }

    }

    void Die()
    {
        Instantiate(enemyexplodePrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    
}
