using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    private bool split;
    private GameObject smallLeft;
    private GameObject smallRight;
    private GameObject bigCenter;
    private int path;
    private float[] paths = { -3.0f, -1.5f, 0.0f, 1.5f, 3.0f }; //position of the paths
    private int position;

    // Use this for initialization
    void Start () {
        split = false;

        //get all the separate game objects
        smallLeft = GameObject.Find("PlayerSmallLeft");
        smallRight = GameObject.Find("PlayerSmallRight");
        bigCenter = GameObject.Find("PlayerBig");

        //set renderers
        smallLeft.GetComponentInChildren<MeshRenderer>().enabled = false;
        smallRight.GetComponentInChildren<MeshRenderer>().enabled = false;
        bigCenter.GetComponentInChildren<MeshRenderer>().enabled = true;

        //set initial position
        position = 2;
        transform.position = new Vector3(paths[position], -4.0f, 0.0f);
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void toggleSplit()
    {
        split = !split;
        
        if (split)
        {
            //set renderers
            smallLeft.GetComponentInChildren<MeshRenderer>().enabled = true;
            smallRight.GetComponentInChildren<MeshRenderer>().enabled = true;
            bigCenter.GetComponentInChildren<MeshRenderer>().enabled = false;

            //correct positioning for splitting
            if (position < 2)
            {
                position = 1;
            }
            else if (position > 2)
            {
                position = 3;
            }
            transform.position = new Vector3(paths[position], -4.0f, 0.0f);
        }
        else
        {
            //set renderers
            smallLeft.GetComponentInChildren<MeshRenderer>().enabled = false;
            smallRight.GetComponentInChildren<MeshRenderer>().enabled = false;
            bigCenter.GetComponentInChildren<MeshRenderer>().enabled = true;
        }
    }

    public void MoveLeft()
    {
        if (split)
        {
            if(position > 1 && position < 4)
            {
                position--;
            }
        }
        else
        {
            if (position > 0 && position < 5)
            {
                position--;
            }
        }
        
        transform.position = new Vector3(paths[position], -4.0f, 0.0f);
    }

    public void MoveRight()
    {
        if (split)
        {
            if (position > 0 && position < 3)
            {
                position++;
            }
        }
        else
        {
            if (position > -1 && position < 4)
            {
                position++;
            }
        }

        transform.position = new Vector3(paths[position], -4.0f, 0.0f);
    }

    public float GetXPosition()
    {
		return paths [position];
	}
}
