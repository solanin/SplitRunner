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
			((GameObject)Obstacles[x]).transform.position = new Vector3(0.0f, start);
			ActiveBlockades.Add((GameObject)Obstacles[x]);
			GameObject.Instantiate((UnityEngine.Object)Obstacles[x]);
            start += dist; //append to start so that nlocks are spaced
        }
    }
}
