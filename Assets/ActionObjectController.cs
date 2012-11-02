using UnityEngine;
using System.Collections;

public class ActionObjectController : MonoBehaviour {
	
	// all different action types
	public enum enObjectAction {NoAction, Look, Use, PickUp, Talk};
	
	// all different action type textures
	
	// Display when selected ActionObject
	// what is displayed depends from object
	private bool mousePressed = false;
	private float clickStartTime = 0.0f;
	private bool displayObjectActionMenu = false;
	private Vector3 clickedMousePosition;
	private static float DISPLAY_MENU_TIME = 0.18f;
	
	public enObjectAction defaultAction = enObjectAction.Look;
	public enObjectAction[] availableActions = new enObjectAction[] {enObjectAction.Look, enObjectAction.PickUp};
	
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
			print("default action " + defaultAction);
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
			enObjectAction selectedAction = DisplaySelectActionMenu(clickedMousePosition, availableActions);
			if (selectedAction != enObjectAction.NoAction) {
				print("selected action " + selectedAction);
			}
		}
    }
	
	public enObjectAction DisplaySelectActionMenu(Vector3 actionMenuCenter, enObjectAction[] actions) {
		int positionChangeX = 15;
		int positionChangeY = 0;
		foreach (enObjectAction action in actions) {
			if (DisplaySelectActionButton(new Rect(actionMenuCenter.x+positionChangeX, Screen.height-actionMenuCenter.y+positionChangeY, 100, 20), new GUIContent(action.ToString()), "Button")) {
				return action;
			}
			positionChangeY += 25;
		}
		return enObjectAction.NoAction;
	}
	
	//http://answers.unity3d.com/questions/315724/loading-gui-in-middle-of-mouse-and-make-it-work-ra.html
	public bool DisplaySelectActionButton(Rect aRect, GUIContent aContent, GUIStyle aStyle) {
		Event e = Event.current;
    	bool isOver = aRect.Contains(e.mousePosition);
    	if (e.type == EventType.Repaint)
        	aStyle.Draw(aRect, aContent, isOver, true, true, false);
    	else if (isOver && e.type == EventType.MouseUp)
        	return true;
    	return false;
	}
}
