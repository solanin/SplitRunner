using UnityEngine;
using System.Collections;

public class PlayerSmall : MonoBehaviour {

	private GameManager gm;
	private Player player;
	
	void Start () {
		gm = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		player = GameObject.Find ("Player").GetComponent<Player> ();
	}
	
	void OnTriggerEnter (Collider col) {
		if (player.isSplit()) {
			Debug.Log ("PlayerSmall hit something");
			if (col.gameObject.tag == "Obstacle") {
				gm.EndGame ();
			}
            else if (col.gameObject.tag == "Collectable")
            {
                gm.addScore();
                Destroy(col.gameObject);
            }
        }
	}
}
