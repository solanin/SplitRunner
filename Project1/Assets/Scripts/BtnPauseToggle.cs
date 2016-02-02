using UnityEngine;
using System.Collections;

public class BtnPauseToggle : MonoBehaviour {

	private GameManager gm; // access to the GameManager
	
	/// <summary>
	/// Initializes the pause button
	/// </summary>
	void Start () {
		// Set up the Game Manager
		GameObject main = GameObject.Find ("GameManager");
		gm = main.GetComponent<GameManager> ();
	}
	
	/// <summary>
	/// Raises the mouse up event.
	/// When clicked, pause the game
	/// </summary>
	void OnMouseUp() {
		if (gm.IsPaused && gm.GameOver == false) {
			gm.Play();	
		} 
		else { 
			gm.Pause();	
		}
	}
}
