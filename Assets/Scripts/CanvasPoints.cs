using UnityEngine;
using UnityEngine.UI;

public class CanvasPoints : MonoBehaviour
{
    public Text pointsText; // Referencia al componente de texto para los puntos
    public Player player; // Referencia al script del jugador

    void Start()
    {
        // Si no has asignado el texto de los puntos en el Inspector, búscalo
        if (pointsText == null)
        {
            pointsText = GameObject.Find("PointsText").GetComponent<Text>();
        }

        // Si no has asignado el jugador en el Inspector, búscalo
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
    }

    void Update()
    {
        // Actualiza el texto de los puntos
        pointsText.text = "POINTS: " + player.points;
    }
}


