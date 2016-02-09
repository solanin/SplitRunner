using UnityEngine;
using System.Collections;

public class ObstacleManager : MonoBehaviour {
    [SerializeField] Object[] Obstacles = new Object[30];
    GameObject[] ActiveBlockades = new GameObject[30];
    int objectCount = 0;

	// Use this for initialization
	void Start () {
        Obstacles = Resources.LoadAll("Obstacles");
        ActiveBlockades[0] = (GameObject)Obstacles[0];
        GenerateObstacles(3.0f, 10);
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < Obstacles.Length; i++)
        {
            GameObject temp = (GameObject)Obstacles[i];
            if (temp.transform.position.y < -5.0f)
            {
                DestroyObject(Obstacles[i]);
                Obstacles[i] = null;
                print(i);
                objectCount--;
            }
        }
        if (objectCount < 10)
        {
            GenerateObstacles(3.0f, 10);
        }
	}

    void GenerateObstacles(float dist, int numObjects)
    {
        float start = 10.0f;
        for (int i = 0; i < numObjects; i++)
        {
            int x = Random.Range(0, Obstacles.Length);
            ActiveBlockades[i] = (GameObject)Obstacles[x];
            ActiveBlockades[i].transform.position = new Vector3(0.0f, start);
            start += dist;
            GameObject.Instantiate(ActiveBlockades[i]);
            objectCount++;
        }
    }
}
