using System.Diagnostics;
using UnityEngine;
using System.Collections.Generic;
using Unity.Collections;
using System.Security.Cryptography;

public class Gun : MonoBehaviour
{
    public Transform player; 
    public GameObject bulletPrefab; 
    public Transform firePoint; 
    public float bulletSpeed = 10f; 
    public float maxDistance = 15f; 
    private Vector2 previousPosition;
    Vector2 offset = new Vector2(0f, -0.25f);
    private float timeSinceLastShot = 0f;
    public float fireRate = 1f;
    

    void Update()
    {
        transform.position = (Vector2)player.position + offset;

        
        if (transform.position.x > previousPosition.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (transform.position.x < previousPosition.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        previousPosition = transform.position;

        
        timeSinceLastShot += Time.deltaTime;

        if (timeSinceLastShot >= fireRate)
        {
            Shoot();
            timeSinceLastShot = 0f; 
        }
    }

    void Shoot()
    {
        
        Transform nearestEnemy = FindNearestEnemy();
        if (nearestEnemy == null)
        {
            
            return;
        }

        
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        
        Vector2 direction = (nearestEnemy.position - firePoint.position).normalized;

        
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed;
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (transform.localScale.x < 0) 
        {
            angle += 180f; 
        }

        firePoint.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    Transform FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            EnemyChase enemyChase = enemy.GetComponent<EnemyChase>();

            if (enemyChase != null && enemyChase.IsAlive())
            { 
                 float distance = Vector2.Distance(firePoint.position, enemy.transform.position);
                if (distance < shortestDistance && distance <= maxDistance)
                {
                   shortestDistance = distance;
                   nearestEnemy = enemy.transform;
                }
            }
        }

        return nearestEnemy;
    }
}
