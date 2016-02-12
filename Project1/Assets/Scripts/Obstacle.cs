using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

    //TODO: Make Getter/Setter
	public float speed = 0.01f; //Need access to this variable so that we can increase/decrease difficulty

	private GameManager gm;

	// Use this for initialization
	void Start () {
		gm = GameObject.Find ("GameManager").GetComponent<GameManager>();
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
