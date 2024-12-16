using UnityEngine;
using System.Collections;

public class Chest : MonoBehaviour
{
    private bool isOpen = false;
    private Animator animator;
    public GameObject coinPrefab;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isOpen)
        {
            OpenChest();
        }
    }

    void OpenChest()
    {
        // Activa el trigger del Animator para abrir el cofre
        animator.SetTrigger("OpenChestTrigger");

        GenerateCoinsAroundChest(8);

        isOpen = true;
       

        // Inicia la corutina para esperar 2 segundos y luego destruir el cofre
        StartCoroutine(DestroyChestAfterDelay(2f));
    }

    IEnumerator DestroyChestAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
        
    }
    void GenerateCoinsAroundChest(int numberOfCoins)
    {
        float radius = 1.0f; // Radio del círculo
        for (int i = 0; i < numberOfCoins; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfCoins;
            Vector3 coinPosition = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0) + transform.position;
            Instantiate(coinPrefab, coinPosition, Quaternion.identity);
        }
    }
}
