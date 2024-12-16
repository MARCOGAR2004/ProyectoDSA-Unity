using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemyChase : MonoBehaviour
{
    public float speed;
    private Transform target;
    private Vector2 previousPosition;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isAlive = true;
    public int health = 3;
    private Animator playerAnimator;


    public bool IsAlive()
    {
       return isAlive;
    }

    void Start()
    {
        speed = Random.Range(1f, 4f);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        playerAnimator = player.GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        previousPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (isAlive)
        {
            AnimatorStateInfo stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("PlayerDead"))
            {
                return; 
            }

            Vector2 direction = ((Vector2)target.position - rb.position).normalized;
            Vector2 newPosition = rb.position + direction * speed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);

            if (newPosition.x > previousPosition.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (newPosition.x < previousPosition.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            previousPosition = newPosition;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isAlive)
        {
            if (other.CompareTag("Player"))
            {
                Player player = other.GetComponent<Player>();
                if (player != null)
                {
                    player.QuitLife();
                }
                Destroy(gameObject);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        animator.SetTrigger("EnemyHit");
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        isAlive = false;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Player playerScript = player.GetComponent<Player>();
        playerScript.SumarPuntos(50);
        animator.SetTrigger("EnemyDead");
        gameObject.tag = "Dead"; 
        StartCoroutine(DestroyChestAfterDelay(2f));
    }

    IEnumerator DestroyChestAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);

    }
}
