using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

    //TODO: Make Getter/Setter
	public float speed = 0.05f; //Need access to this variable so that we can increase/decrease difficulty

	private GameManager gm;

    // Use this for initialization
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        float time = gm.GetTime();
        speed = time * .00125f;
        if (speed < 0.05f)
        {
            speed = 0.05f;
        } else if (speed > 0.17f)
        {
            speed = 0.17f;
        }
    }
	
	// Update is called once per frame
	void Update () {
		if (!gm.IsPaused) {
			// Move Down
			float newy = transform.position.y - speed;
			transform.position = new Vector3 (transform.position.x, newy, transform.position.z);
		}
        if (transform.position.y < -6.0f)
        {
            Destroy(this.gameObject);   
        }
	}
}
