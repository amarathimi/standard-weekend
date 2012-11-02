using UnityEngine;
using System.Collections;

public class ClickController : MonoBehaviour {
	
	private bool objectSelected = false;
	private Vector3 clickedMousePosition;
	//private Vector3 clickedPoint;
	//private GameObject clickedObject;
	private float clickStartTime = 0.0f;
	
	private bool displayObjectActionMenu = false;
	private static float DISPLAY_MENU_TIME = 0.15f;
	//public Texture actionIcon;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit)) {
				// select clicked object
				if (!objectSelected) {
					clickStartTime = Time.time;
					clickedMousePosition = Input.mousePosition;
					//clickedPoint = hit.point;
					//clickedObject = hit.transform.gameObject;
				}
				objectSelected = true;
			}
			
		} else if (Input.GetMouseButtonUp(0)) {
			// perform default click action if action was not selected from menu
			if (!displayObjectActionMenu) {
				// default action
				print("Default action");
			} else {
				// menu action or no action
			}
			
			// release held object
			clickStartTime = 0.0f;
			objectSelected = false;
			displayObjectActionMenu = false;
		}
		
		if (objectSelected) {			
			// display action menu if mouse is held long enough
			if (Time.time - clickStartTime >= DISPLAY_MENU_TIME) {
				displayObjectActionMenu = true;
			}
		}
		
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
