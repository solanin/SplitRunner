using UnityEngine;
using System.Collections;

public class PlayerBig : MonoBehaviour {

	private GameManager gm;
	private Player player;

	void Start () {
		gm = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		player = GameObject.Find ("Player").GetComponent<Player> ();
	}

	void OnTriggerEnter (Collider col) {
		if (!player.isSplit()) {
			Debug.Log ("PlayerBig hit something");
			if (col.gameObject.tag == "Obstacle") {
				gm.EndGame ();
			}
            else if (col.gameObject.tag == "Collectable"){
                gm.addScore();
                Destroy(col.gameObject);
            }
		}
	}
}
