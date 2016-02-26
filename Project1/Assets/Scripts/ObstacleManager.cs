using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleManager : MonoBehaviour
{
    public object[] Obstacles = new object[0];
    public GameManager gm;
    public List<GameObject> ActiveBlockades = new List<GameObject>(0); // Holds All Active Objects on Screen
    public int objectCount = 0; // Current Object on Screen Count

    //2d array to hold the positions
    public bool[,] emptySpaces = new bool[21, 5] { { true, false, true, false, false } ,
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
            { false, true, false, true, true },
            { true, true, false, true, false },
            { false, false, true, false, false },
            { false, false, true, false, true },
            { false, false, false, true, false },
            { false, false, false, false, true },
            { true, false, false, false, false }
        };
    public GameObject collectablePrefab = null;
    public GameObject doubleScorePrefab = null;
    public GameObject shieldPrefab = null;
    private float[] lanePositions = new float[5] { -3.0f, -1.5f, 0.0f, 1.5f, 3.0f };

    // Use this for initialization
    void Start()
    {
        Obstacles = Resources.LoadAll("Obstacles"); // Loads all prefabs in obstacle folder to this array
        GenerateObstacles(4.0f, 10, .05f, 12.0f);// Start initial generation
    }


    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Obstacle").Length < 10)
        {
            if (gm)
            {
                float time = gm.GetTime();
                if (time > 150)
                {
                    GenerateObstacles(10.0f, 40, .5f, 96.0f);
                }
                else if (time > 100)
                {
                    GenerateObstacles(8.0f, 30, .3f, 48.0f);
                }
                else if (time > 50)
                {
                    GenerateObstacles(6.0f, 20, .15f, 24.0f);
                }
                else
                {
                    GenerateObstacles(4.0f, 10, .05f, 12.0f);
                }
            }
        }
    }

    //TODO: Create function for block creation
    void GenerateObstacles(float dist, int numObjects, float speed, float start) // For now only going three random numbers deep
    {
        for (int i = 0; i < numObjects; i++) // Go through and make objects
        {

            int x = Random.Range(0, Obstacles.Length); //Generate Number for first wall
            ((GameObject)Obstacles[x]).transform.position = new Vector3(0.0f, start);
            ActiveBlockades.Add((GameObject)Obstacles[x]);
            GameObject.Instantiate((UnityEngine.Object)Obstacles[x]);

            //Generating collectables
            int obstacle = Random.Range(0, 7);
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

                if (emptySpace.Count == 2)
                {
                    Instantiate(collectablePrefab, new Vector3(lanePositions[emptySpace[0]], start), Quaternion.identity);
                    Instantiate(collectablePrefab, new Vector3(lanePositions[emptySpace[1]], start), Quaternion.identity);
                }
                else
                {
                    Instantiate(shieldPrefab, new Vector3(lanePositions[emptySpace[Random.Range(0, emptySpace.Count)]], start), Quaternion.identity);
                }
            }
            else //multiplier
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
                    Instantiate(doubleScorePrefab, new Vector3(lanePositions[emptySpace[Random.Range(0, emptySpace.Count)]], start), Quaternion.identity);
                }
            }

            start += dist; //append to start so that nlocks are spaced
        }
    }
    
}
