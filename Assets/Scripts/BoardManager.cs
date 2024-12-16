using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using System.Collections.Specialized;
using System.Security.Cryptography;

public class BoardManager : MonoBehaviour
{
    public class Count
    {
        public int maximum;
        public int minimum;
        public Count (int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    public int columns = 50;
    public int rows = 50;
    public GameObject[] floorTiles;
    public GameObject[] outerWallTiles;
    public GameObject[] enemyTiles;
    public GameObject[] monedaTiles;
    public GameObject[] chestTiles;
    public Count monedaCount = new Count(10,15);
    public Count chestCount = new Count(5,7);
    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();



    void InitialiseList()
    {
        gridPositions.Clear();

        for (int x = 1; x < columns - 1; x++)
        {
            for (int y = 1; y < rows - 1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;
        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                if (x == -1 || x == columns || y == -1 || y == rows)
                {
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                }
                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    Vector3 RandomPosition()
    {
      int randomIndex = Random.Range(0, gridPositions.Count);
      Vector3 randomPosition = gridPositions[randomIndex];
      gridPositions.RemoveAt(randomIndex);
            return randomPosition;
    }

    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
        {
            int objectCount = Random.Range(minimum, maximum+ 1);

            for (int i = 0; i < objectCount; i++)
            {
                Vector3 randomPosition= RandomPosition();
                GameObject tileChoice = tileArray[Random.Range (0, tileArray.Length)];
                Instantiate(tileChoice, randomPosition, Quaternion.identity);
           
            }
        }


    public void SetupScene()
    {
        BoardSetup();
        InitialiseList();

        int enemyCount = 10;
        

        LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);
        LayoutObjectAtRandom(monedaTiles, monedaCount.minimum, monedaCount.maximum);
        LayoutObjectAtRandom(chestTiles, chestCount.minimum, chestCount.maximum);

        InvokeRepeating("SpawnRandomEnemies", 5f, 5f);
    }
    void SpawnRandomEnemies()
    {
        LayoutObjectAtRandom(enemyTiles, 1, 3); // Cambia el rango según lo que necesites
    }


}
