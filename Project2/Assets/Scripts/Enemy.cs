using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    GameObject player;
    Rigidbody body;
    public float speed = 25.0f;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        body = this.GetComponentInParent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        body.AddForce((player.transform.position - this.transform.position).normalized * speed);
	}
}
