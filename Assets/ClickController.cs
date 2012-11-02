using UnityEngine;
using System.Collections;

public class ClickController : MonoBehaviour {
	
	// selected ActionObject info
	private bool mouseHeldOverActionObject = false;
	private Vector3 clickedMousePosition;
	private GameObject clickedObject;
	private float clickStartTime = 0.0f;
	
	// Display when selected ActionObject
	// what is displayed depends from object
	private bool displayObjectActionMenu = false;
	private static float DISPLAY_MENU_TIME = 0.15f;
	
	// possible actions
	private enum enCharacterAction {NoAction, Walk, Look, Use, PickUp};
	private enCharacterAction selectedAction;
	
	// Action icons
	//public Texture actionIcon;
	
	// Use this for initialization
	void Start () {
		selectedAction = enCharacterAction.NoAction;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetMouseButtonDown(0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit)) {
				if (hit.transform.gameObject.tag.Contains("ActionObject")) {
					// save clicked object
					mouseHeldOverActionObject = true;
					clickedObject = hit.transform.gameObject;
					clickStartTime = Time.time;
					clickedMousePosition = Input.mousePosition;
				} else {
					// just set moving destination
					//hit.point
					selectedAction = enCharacterAction.Walk;
				}
			}
			
		} else if (Input.GetMouseButtonUp(0)) {
			// perform default click action if action was not selected from menu
			if (!displayObjectActionMenu) {
				// default action
				//	print("Default action");
			}
			
			// release held object
			if (mouseHeldOverActionObject) {
				clickStartTime = 0.0f;
				mouseHeldOverActionObject = false;
				displayObjectActionMenu = false;
			}
		}
		
		if (mouseHeldOverActionObject) {			
			// display action menu if mouse is held long enough
			if (Time.time - clickStartTime >= DISPLAY_MENU_TIME) {
				displayObjectActionMenu = true;
			}
		}
		
		print (selectedAction);
		
	}
	
	void OnGUI() {
		if (displayObjectActionMenu) {
			//mousePos.x,Screen.height - mousePos.y,cursorImage.width,cursorImage.height
			//Vector2 actionMenuCenter = Camera.main.WorldToViewportPoint(clickedPoint);
        	if (SelectActionButton(new Rect(clickedMousePosition.x, Screen.height-clickedMousePosition.y, 100, 20), new GUIContent("Button1"), "Button")) {
				print("Action selected from menu");
			}
		}
		//GUI.Button(new Rect(0, 0, 100, 20), new GUIContent(icon));
    }
	
	//http://answers.unity3d.com/questions/315724/loading-gui-in-middle-of-mouse-and-make-it-work-ra.html
	bool SelectActionButton(Rect aRect, GUIContent aContent, GUIStyle aStyle) {
		Event e = Event.current;
    	bool isOver = aRect.Contains(e.mousePosition);
    	if (e.type == EventType.Repaint)
        	aStyle.Draw(aRect,aContent, isOver, true, true, false);
    	else if (isOver && e.type == EventType.MouseUp)
        	return true;
    	return false;
	}
	
}
