using UnityEngine;
using System.Collections;

public class ObjectActionSelector : MonoBehaviour {
	
    // Display when selected ActionObject
	// what is displayed depends from object
	private bool mousePressed = false;
	private float clickStartTime = 0.0f;
	private bool displayObjectActionMenu = false;
	private Vector3 clickedMousePosition;
	private static float DISPLAY_MENU_TIME = 0.18f;
	
	// Allowed actions for object
	public enObjectAction defaultAction = enObjectAction.Look;
	public enObjectAction[] availableActions = new enObjectAction[] {enObjectAction.Look};
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (!displayObjectActionMenu && mousePressed && Time.time - clickStartTime >= DISPLAY_MENU_TIME) {
			displayObjectActionMenu = true;
			clickedMousePosition = Input.mousePosition;
		}
	}
	
	void OnMouseDown() {
		mousePressed = true;
		clickStartTime = Time.time;
	}
	
	void OnMouseUp() {
		// do default action if we didn't display action menu
		if (!displayObjectActionMenu) {
			Debug.Log("selected default action " + defaultAction);
		}
		mousePressed = false;
		clickStartTime = 0.0f;
	    displayObjectActionMenu = false;
	}
	
	void OnMouseEnter() {
		// change icon to default action?
	}
	
	void OnMouseExit() {
		// return default icon
	}
	
	void OnGUI() {
		if (displayObjectActionMenu) {
			GameObject manager = GameObject.FindWithTag("GameManager");
			enObjectAction selectedAction = manager.GetComponent<ObjectActionManager>().DisplaySelectActionMenu(clickedMousePosition, availableActions);
			if (selectedAction != enObjectAction.NoAction) {
				Debug.Log("selected action " + selectedAction);
			}
		}
    }
	
	
}
