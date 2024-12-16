using UnityEngine;

public class Coin : MonoBehaviour
{
  
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.CollectCoin();  // Llama al método CollectCoin() en el script del jugador
            }
            Destroy(gameObject);  // Destruye la moneda
        }
    }


}
