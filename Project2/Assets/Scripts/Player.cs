using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    GameObject self;
    Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
        self = GameObject.FindGameObjectWithTag("Player");
        rigidBody = self.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A))
        {
            rigidBody.velocity = new Vector3(-10.0f, rigidBody.velocity.y);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rigidBody.velocity = new Vector3(10.0f, rigidBody.velocity.y);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, 50.0f);
        }
    }
}
