using System.Diagnostics;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public LayerMask blockingLayer;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;
    public GameObject Gun;
    private bool isAlive = true;
    public int coinCount = 0;
    public int lifeCount = 3;
    public int points = 0; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

    }

    void Update()
    {
        if (isAlive)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");


            moveInput = new Vector2(horizontal, vertical).normalized;
            if (moveInput.magnitude > 0)
            {
                animator.SetTrigger("PlayerRun");
            }
            else
            {
                animator.ResetTrigger("PlayerRun");
            }
            if (horizontal < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (horizontal > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    void FixedUpdate()
    {
        if (isAlive)
        {
            Vector2 targetPosition = rb.position + moveInput * speed * Time.fixedDeltaTime;
            boxCollider.enabled = false;
            RaycastHit2D hit = Physics2D.Linecast(rb.position, targetPosition, blockingLayer);
            boxCollider.enabled = true;

            if (hit.transform == null)
            {
                rb.MovePosition(targetPosition);
            }
        }




    }


    public void CollectCoin()
    {
        coinCount++;
    }

    public void QuitLife()
    {
        lifeCount--;
        if (lifeCount > 0)
        {
            animator.SetTrigger("PlayerHit");
        }
        if (lifeCount == 0)
        {
            isAlive = false;
            animator.SetTrigger("PlayerDead");
            Destroy(Gun);
            
        }
    }

    public void SumarPuntos(int p)
    {
        points = points + p;
    }
}