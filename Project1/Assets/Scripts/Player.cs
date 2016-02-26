using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    private bool split;
	public bool isSplit() { return split; }
    private GameObject smallLeft;
    private GameObject smallRight;
	private GameObject bigCenter;
	private GameObject smallLeftS;
	private GameObject smallRightS;
	private GameObject bigCenterS;
    private GameObject bigShield;
    private GameObject smallLeftShield;
    private GameObject smallRightShield;

    private int path;
    private float[] paths = { -3.0f, -1.5f, 0.0f, 1.5f, 3.0f }; //position of the paths
    private int position;
    public float speed = 40; // Left right movement

	private GameManager gm;

    private bool shieldOn = false;
    private float shieldFlashTimer = 0.0f;

    // Use this for initialization
    void Start () {
		gm = GameObject.Find ("GameManager").GetComponent<GameManager> ();

        split = false;

        //get all the separate game objects
        smallLeft = GameObject.Find("PlayerSmallLeft");
        smallRight = GameObject.Find("PlayerSmallRight");
		bigCenter = GameObject.Find("PlayerBig");
		smallLeftS = GameObject.Find("PlayerSmallLeftShaddow");
		smallRightS = GameObject.Find("PlayerSmallRightShaddow");
		bigCenterS = GameObject.Find("PlayerBigShaddow");

        smallLeftShield = GameObject.Find("PlayerSmallLeftShield");
        smallRightShield = GameObject.Find("PlayerSmallRightShield");
        bigShield = GameObject.Find("PlayerBigShield");

        //set renderers
        smallLeft.GetComponentInChildren<MeshRenderer>().enabled = false;
        smallRight.GetComponentInChildren<MeshRenderer>().enabled = false;
		bigCenter.GetComponentInChildren<MeshRenderer>().enabled = true;
		smallLeftS.GetComponentInChildren<MeshRenderer>().enabled = false;
		smallRightS.GetComponentInChildren<MeshRenderer>().enabled = false;
		bigCenterS.GetComponentInChildren<MeshRenderer>().enabled = true;
        smallLeftShield.GetComponentInChildren<MeshRenderer>().enabled = false;
        smallRightShield.GetComponentInChildren<MeshRenderer>().enabled = false;
        bigShield.GetComponentInChildren<MeshRenderer>().enabled = false;

        //set initial position
        position = 2;
        transform.position = new Vector3(paths[position], -4.0f, 0.0f);
    }
	
	// Update is called once per frame
	void Update () {
	    if (transform.position.x != paths[position])
        {
            if (transform.position.x < paths[position])
            {
                float newPos = transform.position.x + speed * Time.deltaTime;
                if (newPos > paths[position])
                {
                    newPos = paths[position];
                }
                transform.position = new Vector3(newPos, -4.0f, 0.0f);
            }
            else 
            {
                float newPos = transform.position.x - speed * Time.deltaTime;
                if (newPos < paths[position])
                {
                    newPos = paths[position];
                }
                transform.position = new Vector3(newPos, -4.0f, 0.0f);
            }
        }
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
			smallLeftS.GetComponentInChildren<MeshRenderer>().enabled = true;
			smallRightS.GetComponentInChildren<MeshRenderer>().enabled = true;
			bigCenterS.GetComponentInChildren<MeshRenderer>().enabled = false;

            if (gm.Shield())
            {
                smallLeftShield.GetComponentInChildren<MeshRenderer>().enabled = true;
                smallRightShield.GetComponentInChildren<MeshRenderer>().enabled = true;
                bigShield.GetComponentInChildren<MeshRenderer>().enabled = false;
            }

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
			smallLeftS.GetComponentInChildren<MeshRenderer>().enabled = false;
			smallRightS.GetComponentInChildren<MeshRenderer>().enabled = false;
			bigCenterS.GetComponentInChildren<MeshRenderer>().enabled = true;

            if (gm.Shield())
            {
                smallLeftShield.GetComponentInChildren<MeshRenderer>().enabled = false;
                smallRightShield.GetComponentInChildren<MeshRenderer>().enabled = false;
                bigShield.GetComponentInChildren<MeshRenderer>().enabled = true;
            }
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
        
        //transform.position = new Vector3(paths[position], -4.0f, 0.0f);
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

        //transform.position = new Vector3(paths[position], -4.0f, 0.0f);
    }

    public float GetXPosition()
    {
		return paths[position];
	}

    public void TurnOnShield()
    {
        if (split)
        {
            smallLeftShield.GetComponentInChildren<MeshRenderer>().enabled = true;
            smallRightShield.GetComponentInChildren<MeshRenderer>().enabled = true;
        }
        else
        {
            bigShield.GetComponentInChildren<MeshRenderer>().enabled = true;
        }

        shieldOn = true;
    }
    
    public void TurnOffShield()
    {
        smallLeftShield.GetComponentInChildren<MeshRenderer>().enabled = false;
        smallRightShield.GetComponentInChildren<MeshRenderer>().enabled = false;
        bigShield.GetComponentInChildren<MeshRenderer>().enabled = false;

        shieldOn = false;
    }

    public void FlashShield()
    {
        shieldFlashTimer -= Time.deltaTime;
        if (shieldFlashTimer <= 0.0f)
        {
            if (shieldOn)
            {
                TurnOffShield();
            }
            else
            {
                TurnOnShield();
            }
            shieldFlashTimer = 0.1f;
        }

    }
    public void SetUpFlash()
    {
        shieldFlashTimer = 0.1f;
    }
}
