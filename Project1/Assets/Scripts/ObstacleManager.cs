using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleManager : MonoBehaviour
{
    public object[] Obstacles = new object[0];
    public GameManager gm;
    int level = 0;
    GameObject levelText;
    public List<GameObject> ActiveBlockades = new List<GameObject>(0); // Holds All Active Objects on Screen
    public int objectCount = 0; // Current Object on Screen Count

    //2d array to hold the positions
    public bool[,] emptySpaces = new bool[19, 5] { 
            { true, false, true, false, false } ,
            { true, true, false, true, true } ,
            { true, false, true, true, true } ,
            { true, true, true, false, true },
            { false, true, false, false, false } ,
            { false, true, false, true, false },
            { false, true, true, true, false },
            { false, true, false, true, true },
            { true, true, false, true, false },
            { true, true, false, true, true },
            { true, false, true, true, true },
            { true, true, true, false, true },
            { false, true, false, true, false },
            { false, true, true, true, false },
            { false, false, true, false, false },
            { false, false, true, false, true },
            { false, false, false, true, false },
            { false, false, false, false, true },
            { true, false, false, false, false }
        };
    public GameObject collectablePrefab = null;
    public GameObject doubleScorePrefab = null;
    public GameObject shieldPrefab = null;
    public GameObject slowPrefab = null;
    private float[] lanePositions = new float[5] { -3.0f, -1.5f, 0.0f, 1.5f, 3.0f };

    // Use this for initialization
    void Start()
    {
        Obstacles = Resources.LoadAll("Obstacles"); // Loads all prefabs in obstacle folder to this array
        level = 0;
        levelText = GameObject.FindWithTag("Level");
        GenerateObstacles(8.0f, 10, 12.0f);// Start initial generation
        
    }


    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Obstacle").Length < 2)
        {
            if (gm)
            {
                float time = gm.GetTime();
                GenerateObstacles(8.0f, 10, 12.0f);
            }
        }
    }

    //TODO: Create function for block creation
    void GenerateObstacles(float dist, int numObjects, float start) // For now only going three random numbers deep
    {
        level++;
        levelText.GetComponent<TextMesh>().text = "Level " + level;
        StopAllCoroutines();
        StartCoroutine(fadeText(3.0f));

        bool multiplier = false;
        bool shield = false;
        bool slow = false;

        for (int i = 0; i < numObjects; i++) // Go through and make objects
        {

            int x = Random.Range(0, Obstacles.Length); //Generate Number for first wall
            ((GameObject)Obstacles[x]).transform.position = new Vector3(0.0f, start);
            ActiveBlockades.Add((GameObject)Obstacles[x]);
            GameObject.Instantiate((UnityEngine.Object)Obstacles[x]);

            //Generating collectables
            int obstacle = Random.Range(0, 8);
            if (obstacle < 5)
            {
                List<int> emptySpace = new List<int>();
                for (int j = 0; j < 5; j++)
                {
                    if (emptySpaces[x, j])
                    {
                        emptySpace.Add(j);
                    }
                }

                if (emptySpace.Count == 2)
                {
                    Instantiate(collectablePrefab, new Vector3(lanePositions[emptySpace[0]], start), Quaternion.identity);
                    Instantiate(collectablePrefab, new Vector3(lanePositions[emptySpace[1]], start), Quaternion.identity);
                }
                else
                {
                    Instantiate(collectablePrefab, new Vector3(lanePositions[emptySpace[Random.Range(0, emptySpace.Count)]], start), Quaternion.identity);
                }
            }
            else if (obstacle == 5)//shield
            {
                List<int> emptySpace = new List<int>();
                for (int j = 0; j < 5; j++)
                {
                    if (emptySpaces[x, j])
                    {
                        emptySpace.Add(j);
                    }
                }
                
                //limit to only one shield per level
                if (shield)
                {
                    Instantiate(collectablePrefab, new Vector3(lanePositions[emptySpace[Random.Range(0, emptySpace.Count)]], start), Quaternion.identity);
                }
                else
                {
                    Instantiate(shieldPrefab, new Vector3(lanePositions[emptySpace[Random.Range(0, emptySpace.Count)]], start), Quaternion.identity);
                    shield = true;
                }
            }
            else if (obstacle == 6)//multiplier
            {
                List<int> emptySpace = new List<int>();
                for (int j = 0; j < 5; j++)
                {
                    if (emptySpaces[x, j])
                    {
                        emptySpace.Add(j);
                    }
                }
                
                if (multiplier)
                {
                    Instantiate(collectablePrefab, new Vector3(lanePositions[emptySpace[Random.Range(0, emptySpace.Count)]], start), Quaternion.identity);
                }
                else
                {
                    Instantiate(doubleScorePrefab, new Vector3(lanePositions[emptySpace[Random.Range(0, emptySpace.Count)]], start), Quaternion.identity);
                    multiplier = true;
                }
            }
            else //slow
            {
                List<int> emptySpace = new List<int>();
                for (int j = 0; j < 5; j++)
                {
                    if (emptySpaces[x, j])
                    {
                        emptySpace.Add(j);
                    }
                }

                if (slow || level < 3) //dont show before level 3
                {
                    Instantiate(collectablePrefab, new Vector3(lanePositions[emptySpace[Random.Range(0, emptySpace.Count)]], start), Quaternion.identity);
                }
                else
                {
                    Instantiate(slowPrefab, new Vector3(lanePositions[emptySpace[Random.Range(0, emptySpace.Count)]], start), Quaternion.identity);
                    slow = true;
                }
            }

            start += dist; //append to start so that nlocks are spaced
        }
    }

    IEnumerator fadeText(float inTime)
    {
        Vector3 start = new Vector3(1.0f, 0, 0);
        for (float t = 0f; t < 1f; t += Time.deltaTime / inTime)
        {
            levelText.GetComponent<Renderer>().material.color = new Color(0,0,255, Vector3.Lerp(start, Vector3.zero, t).x);
            yield return null;
        }
    }

}
