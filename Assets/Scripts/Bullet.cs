using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 5f; 
    public int damage = 1; 

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyChase enemy = collision.GetComponent<EnemyChase>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }


            Destroy(gameObject);
        }
    }
}
