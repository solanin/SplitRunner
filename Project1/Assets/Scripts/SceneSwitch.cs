using UnityEngine;
using System.Collections;

public class SceneSwitch : MonoBehaviour {

	public string gotoName; // The name of the scene to load
	
	/// <summary>
	/// Raises the mouse down event.
	/// When clicked, loads level
	/// </summary>
	void OnMouseDown(){		
		Application.LoadLevel(gotoName);
	}
}
