using UnityEngine;
using UnityEngine.UI;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Text lifeText; // Referencia al componente de texto para las vidas
    public Player player; // Referencia al script del jugador

    void Start()
    {
        // Si no has asignado el texto de las vidas en el Inspector, búscalo
        if (lifeText == null)
        {
            lifeText = GameObject.Find("LifeText").GetComponent<Text>();
        }

        // Si no has asignado el jugador en el Inspector, búscalo
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
    }

    void Update()
    {
        // Actualiza el texto de las vidas
        lifeText.text = "HEARTS: " + player.lifeCount;
    }
}



