using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleManager : MonoBehaviour {
    public object[] Obstacles = new object[0];
    public List<GameObject> ActiveBlockades = new List<GameObject>(0); // Holds All Active Objects on Screen
    public int objectCount = 0; // Current Object on Screen Count

	// Use this for initialization
	void Start () {
        Obstacles = Resources.LoadAll("Obstacles"); // Loads all prefabs in obstacle folder to this array
        GenerateObstacles(3.0f, 10);// Start initial generation
    }
	
	// Update is called once per frame
	void Update () {
        if (GameObject.FindGameObjectsWithTag("Obstacle").Length < 5)
        {
            GenerateObstacles(3.0f, 10);
        }
	}

    //TODO: Create function for block creation
    void GenerateObstacles(float dist, int numObjects) // For now only going three random numbers deep
    {
        float start = 10.0f;
        for (int i = 0; i < numObjects; i++) // Go through and make objects
        {
            int x = Random.Range(0, Obstacles.Length); //Generate Number for first wall

            if (((GameObject)Obstacles[x]).name == "1") //Block size 1
            {
                int y = Random.Range(0, 5); // Block of size 1 can be in 5 locations, Pick one
                if (y == 0)
                {
                    ((GameObject)Obstacles[x]).transform.position = new Vector3(-3.0f, start); //
                    ActiveBlockades.Add((GameObject)Obstacles[x]);                             // BRING OBJECT TO LIFE
                    GameObject.Instantiate((UnityEngine.Object)Obstacles[x]);                  //

                    int z = Random.Range(0, 3); // Another block of size one can fit in three places.
                    if (z == 0)
                    {
                        ((GameObject)Obstacles[x]).transform.position = new Vector3(0.0f, start);
                        ActiveBlockades.Add((GameObject)Obstacles[x]);
                        GameObject.Instantiate((UnityEngine.Object)Obstacles[x]);
                    }
                    else if (z == 1)
                    {
                        ((GameObject)Obstacles[x]).transform.position = new Vector3(1.5f, start);
                        ActiveBlockades.Add((GameObject)Obstacles[x]);
                        GameObject.Instantiate((UnityEngine.Object)Obstacles[x]);
                    }
                    else if (z == 2)
                    {
                        ((GameObject)Obstacles[x]).transform.position = new Vector3(3.0f, start);
                        ActiveBlockades.Add((GameObject)Obstacles[x]);
                        GameObject.Instantiate((UnityEngine.Object)Obstacles[x]);
                    }
                }
                else if (y == 1)
                {
                    ((GameObject)Obstacles[x]).transform.position = new Vector3(-1.5f, start); //
                    ActiveBlockades.Add((GameObject)Obstacles[x]);                             // BRING OBJECT TO LIFE
                    GameObject.Instantiate((UnityEngine.Object)Obstacles[x]);                  //

                    int z = Random.Range(0, 2); // Another block of size one can fit in two places.
                    if (z == 0)
                    {
                        ((GameObject)Obstacles[x]).transform.position = new Vector3(3.0f, start);
                        ActiveBlockades.Add((GameObject)Obstacles[x]);
                        GameObject.Instantiate((UnityEngine.Object)Obstacles[x]);
                    }
                    else
                    {
                        ((GameObject)Obstacles[x]).transform.position = new Vector3(1.5f, start);
                        ActiveBlockades.Add((GameObject)Obstacles[x]);
                        GameObject.Instantiate((UnityEngine.Object)Obstacles[x]);
                    }
                }
                else if (y == 2)
                {
                    ((GameObject)Obstacles[x]).transform.position = new Vector3(0.0f, start);  //
                    ActiveBlockades.Add((GameObject)Obstacles[x]);                             // BRING OBJECT TO LIFE
                    GameObject.Instantiate((UnityEngine.Object)Obstacles[x]);                  //

                    int z = Random.Range(0, 2); // Another block of size one can fit in two places.
                    if (z == 0)
                    {
                        ((GameObject)Obstacles[x]).transform.position = new Vector3(3.0f, start);
                        ActiveBlockades.Add((GameObject)Obstacles[x]);
                        GameObject.Instantiate((UnityEngine.Object)Obstacles[x]);
                    }
                    else
                    {
                        ((GameObject)Obstacles[x]).transform.position = new Vector3(-3.0f, start);
                        ActiveBlockades.Add((GameObject)Obstacles[x]);
                        GameObject.Instantiate((UnityEngine.Object)Obstacles[x]);
                    }
                }
                else if (y == 3)
                {
                    ((GameObject)Obstacles[x]).transform.position = new Vector3(1.5f, start);
                    ActiveBlockades.Add((GameObject)Obstacles[x]);                             // BRING OBJECT TO LIFE
                    GameObject.Instantiate((UnityEngine.Object)Obstacles[x]);

                    int z = Random.Range(0, 2); // Another block of size one can fit in two places.
                    if (z == 0)
                    {
                        ((GameObject)Obstacles[x]).transform.position = new Vector3(-1.5f, start);
                        ActiveBlockades.Add((GameObject)Obstacles[x]);
                        GameObject.Instantiate((UnityEngine.Object)Obstacles[x]);
                    }
                    else
                    {
                        ((GameObject)Obstacles[x]).transform.position = new Vector3(-3.0f, start);
                        ActiveBlockades.Add((GameObject)Obstacles[x]);
                        GameObject.Instantiate((UnityEngine.Object)Obstacles[x]);
                    }

                }
                else if (y == 4)
                {
                    ((GameObject)Obstacles[x]).transform.position = new Vector3(3.0f, start);  // 
                    ActiveBlockades.Add((GameObject)Obstacles[x]);                             // BRING OBJECT TO LIFE
                    GameObject.Instantiate((UnityEngine.Object)Obstacles[x]);                  //

                    int z = Random.Range(0, 3); // Another block of size one can fit in three places.
                    if (z == 0)
                    {
                        ((GameObject)Obstacles[x]).transform.position = new Vector3(0.0f, start);
                        ActiveBlockades.Add((GameObject)Obstacles[x]);
                        GameObject.Instantiate((UnityEngine.Object)Obstacles[x]);
                    }
                    else if (z == 1)
                    {
                        ((GameObject)Obstacles[x]).transform.position = new Vector3(-1.5f, start);
                        ActiveBlockades.Add((GameObject)Obstacles[x]);
                        GameObject.Instantiate((UnityEngine.Object)Obstacles[x]);
                    }
                    else if (z == 2)
                    {
                        ((GameObject)Obstacles[x]).transform.position = new Vector3(-3.0f, start);
                        ActiveBlockades.Add((GameObject)Obstacles[x]);
                        GameObject.Instantiate((UnityEngine.Object)Obstacles[x]);
                    }
                }

            } else if (((GameObject)Obstacles[x]).name == "2")
            {
                int y = Random.Range(0, 3); // Block of size 1 can be in 3 locations, Pick one
                if (y == 0)
                {
                    ((GameObject)Obstacles[x]).transform.position = new Vector3(-1.5f, start); //
                    ActiveBlockades.Add((GameObject)Obstacles[x]);                             // BRING OBJECT TO LIFE
                    GameObject.Instantiate((UnityEngine.Object)Obstacles[x]);                  //
                } else if (y == 1)
                {
                    ((GameObject)Obstacles[x]).transform.position = new Vector3(1.5f, start); //
                    ActiveBlockades.Add((GameObject)Obstacles[x]);                             // BRING OBJECT TO LIFE
                    GameObject.Instantiate((UnityEngine.Object)Obstacles[x]);                  //
                } else if (y == 2)
                {
                    ((GameObject)Obstacles[x]).transform.position = new Vector3(0.0f, start); //
                    ActiveBlockades.Add((GameObject)Obstacles[x]);                             // BRING OBJECT TO LIFE
                    GameObject.Instantiate((UnityEngine.Object)Obstacles[x]);                  //
                }

            } else if (((GameObject)Obstacles[x]).name == "3")
            {
                int y = Random.Range(0, 3); // Block of size 1 can be in 3 locations, Pick one
                if (y == 0)
                {
                    ((GameObject)Obstacles[x]).transform.position = new Vector3(-1.5f, start); //
                    ActiveBlockades.Add((GameObject)Obstacles[x]);                             // BRING OBJECT TO LIFE
                    GameObject.Instantiate((UnityEngine.Object)Obstacles[x]);                  //
                }
                else if (y == 1)
                {
                    ((GameObject)Obstacles[x]).transform.position = new Vector3(1.5f, start); //
                    ActiveBlockades.Add((GameObject)Obstacles[x]);                             // BRING OBJECT TO LIFE
                    GameObject.Instantiate((UnityEngine.Object)Obstacles[x]);                  //
                }
                else if (y == 2)
                {
                    ((GameObject)Obstacles[x]).transform.position = new Vector3(0.0f, start); //
                    ActiveBlockades.Add((GameObject)Obstacles[x]);                             // BRING OBJECT TO LIFE
                    GameObject.Instantiate((UnityEngine.Object)Obstacles[x]);                  //
                }

            } else if (((GameObject)Obstacles[x]).name == "4")
            {
                int y = Random.Range(0, 2); // Block of size 1 can be in 2 locations, Pick one
                if (y == 0)
                {
                    ((GameObject)Obstacles[x]).transform.position = new Vector3(-2.0f, start); //
                    ActiveBlockades.Add((GameObject)Obstacles[x]);                             // BRING OBJECT TO LIFE
                    GameObject.Instantiate((UnityEngine.Object)Obstacles[x]);                  //
                }
                else if (y == 1)
                {
                    ((GameObject)Obstacles[x]).transform.position = new Vector3(2.0f, start); //
                    ActiveBlockades.Add((GameObject)Obstacles[x]);                             // BRING OBJECT TO LIFE
                    GameObject.Instantiate((UnityEngine.Object)Obstacles[x]);                  //
                }
            } else
            {
                int y = Random.Range(0, 2); // Block of size 1 can be in 2 locations, Pick one
                if (y == 0)
                {
                    ((GameObject)Obstacles[x]).transform.position = new Vector3(-2.0f, start); //
                    ActiveBlockades.Add((GameObject)Obstacles[x]);                             // BRING OBJECT TO LIFE
                    GameObject.Instantiate((UnityEngine.Object)Obstacles[x]);                  //
                }
                else if (y == 1)
                {
                    ((GameObject)Obstacles[x]).transform.position = new Vector3(2.0f, start); //
                    ActiveBlockades.Add((GameObject)Obstacles[x]);                             // BRING OBJECT TO LIFE
                    GameObject.Instantiate((UnityEngine.Object)Obstacles[x]);                  //
                }
            }
            start += dist; //append to start so that nlocks are spaced
        }
    }
}
