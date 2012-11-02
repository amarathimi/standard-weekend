using UnityEngine;
using System.Collections;

public class ObjectClickController : MonoBehaviour {
	
	public Texture icon;
	private bool displayActionMenu;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
    void OnGUI() {
		if (displayActionMenu) {
        	//GUI.Button(new Rect(0, 0, 100, 20), new GUIContent("Click me", icon));
		}
    }
	
	// called when user clicks over an object
	void OnMouseDown() {
		print("Object clicked");
		displayActionMenu = true;
	}
	
	void OnMouseUp() {
		displayActionMenu = false;
	}
	
	// mouseEnter and mouseExit
}
