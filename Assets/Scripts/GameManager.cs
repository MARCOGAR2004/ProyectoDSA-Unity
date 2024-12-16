using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    public BoardManager boardScript;

    public GameObject introCanvas;
    public GameObject coinsCanvas;
    public GameObject livesCanvas;
    public GameObject pointsCanvas;
    
    private bool gameStarted = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();
       // InitGame();
       StartCoroutine(ShowIntro());

    }
    IEnumerator ShowIntro()
    {
        introCanvas.SetActive(true);
        coinsCanvas.SetActive(false);
        livesCanvas.SetActive(false);
        pointsCanvas.SetActive(false);

        yield return new WaitForSeconds(2f);
        introCanvas.SetActive(false);
        InitGame();
       
    }


    void InitGame()
    {
        gameStarted = true;
        boardScript.SetupScene();
        coinsCanvas.SetActive(true);
        livesCanvas.SetActive(true);
        pointsCanvas.SetActive(true);   
    }

}
