using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnPool : MonoBehaviour
{
    public int columnPoolSize = 32;
    public GameObject colunmPrefab;
    public float spawnRate = 8f;
    public float columnMin = -2.5f;
    public float columnMax = 2f;

    private Rigidbody2D rb2 = null;
    private float yMin = -2.5f;
    private float yMax = 2.5f;
    private GameObject[] columns;
    private Vector2 objectPoolPosition = new Vector2(-15f, -25f);
    private float timeSinceLastSpawned;
    private float spawnXPosition = 20f;
    private int currentColumn = 0;

    private float xDistMultiplier = 1f;


    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastSpawned = 0f;

        columns = new GameObject[columnPoolSize];
        for (int i = 0; i < columnPoolSize; i++)
        {
            columns[i] = (GameObject)Instantiate(colunmPrefab, objectPoolPosition, Quaternion.identity);
        }
        rb2 = columns[currentColumn].GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawned += Time.deltaTime;
        rb2 = columns[currentColumn].GetComponent<Rigidbody2D>();
        if (GameControl.instance.gameOver == false && timeSinceLastSpawned >= spawnRate)
        {
            SpawnColumns();
        }

        if(GameControl.instance.gameOver == false)
        {
            rb2 = columns[currentColumn].GetComponent<Rigidbody2D>();
            if (GameControl.instance.GetCurrentScore() == 0) //v ez
            {
                spawnRate = 8f;
                columnMin = -2.5f;
                columnMax = 1f;
               // rb2.velocity = new Vector2(-1f, ScrollingObjectY());
            }
            else if (GameControl.instance.GetCurrentScore() == 3) //med
            {
                spawnRate = 4f;
                columnMin = -2f;
                columnMax = 1.5f;
            }
            else if (GameControl.instance.GetCurrentScore() == 5) //hard
            {
                spawnRate = 2f;
                columnMin = -2.5f;
                columnMax = 1f;
            }
            else if (GameControl.instance.GetCurrentScore() == 8) //v v hard
            {
                spawnRate = 1f;
                columnMin = -2.25f;
                columnMax = 1.25f;
            }
            else if (GameControl.instance.GetCurrentScore() >= 13) //impossible
            {
                spawnRate = .5f;
                columnMin = -3.5f;
                columnMax = 3f;
                //rb2.velocity = Vector2.zero;
               // rb2.velocity = new Vector2(0f, ScrollingObjectY());
            }
        }

    }

    private void SpawnColumns()
    {
        timeSinceLastSpawned = 0f;
        float spawnYPosition = Random.Range(columnMin, columnMax);
        columns[currentColumn].transform.position = new Vector2(spawnXPosition * xDistMultiplier, spawnYPosition);
        currentColumn++;
        if (currentColumn >= columnPoolSize)
        {
            currentColumn = 0;
        }
    }

    private void IncreaseColumns(int sizeMultiplier)
    {
        columnPoolSize = columnPoolSize * sizeMultiplier;

        GameObject[] columnsNew = new GameObject[(columnPoolSize)];
        //GameObject temp = null;
        for (int i = 0; i < columnPoolSize; i++)
        {
            if(i <= (columnPoolSize / sizeMultiplier))
            {
                //temp = columns[i];
                columnsNew[i] = columns[i];
            }
            else
            {
                columnsNew[i] = (GameObject)Instantiate(colunmPrefab, objectPoolPosition, Quaternion.identity);
            }
            
        }
        columns = columnsNew;
    }

    public float ScrollingObjectY()
    {
        if (rb2.transform.position.y <= yMin)
        {
           // Debug.Log("if 1 --- " + rb2.transform.position.y);
            return 2.5f;
        }
        else if (rb2.transform.position.y >= yMax)
        {
            //Debug.Log("if 2--- " + rb2.transform.position.y);
            return -2.5f;
        }
        else
        {
         //   Debug.Log("else --- " + rb2.transform.position.y);
            return 0f;
        }
    }
        
}
