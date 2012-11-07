using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* 
 * List of all possible action object actions
 */
public enum enObjectAction {NoAction, Look, Use, PickUp, Talk};

/*
 * Keeps track of the possible actions.
 * Stores also textures for drawing each action icon.
 * 
 */
public class ObjectActionManager : MonoBehaviour {
	
	//public GUISkin gameSkin;
	private Dictionary<enObjectAction, GUIContent> objectActionToButton = new Dictionary<enObjectAction, GUIContent>();
	public GUIContent[] objectActionButtons = new GUIContent[4];
	public GUIStyle buttonStyle;
	
	// Use this for initialization
	void Start () {
		objectActionToButton.Add(enObjectAction.Look, objectActionButtons[0]);
		objectActionToButton.Add(enObjectAction.Use, objectActionButtons[1]);
		objectActionToButton.Add(enObjectAction.PickUp, objectActionButtons[2]);
		objectActionToButton.Add(enObjectAction.Talk, objectActionButtons[3]);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit)) {
				if (!hit.transform.gameObject.tag.Contains("ActionObject")) {
					// clicked something else than an action object or game character.
					GameObject closestGoalPoint = FindClosestGoalPoint(hit.point);
					if (closestGoalPoint != null) {
						// set player destination to goal point
						print (closestGoalPoint.name);
						GameObject player = GameObject.FindGameObjectWithTag("Player");
						player.GetComponent<TestMoveScript>().SetMoveDestination(closestGoalPoint); 
					}
				}
			}
			
		}
	}
	
	GameObject FindClosestGoalPoint(Vector3 location) {
		GameObject[] goalPoints = GameObject.FindGameObjectsWithTag("GoalPoint");
		
		GameObject closestGoalPoint = null;
		float distance = Mathf.Infinity;
		
		foreach (GameObject goalPoint in goalPoints) {
			float currentDistance = (goalPoint.transform.position - location).sqrMagnitude;
			if (currentDistance < distance) {
				closestGoalPoint = goalPoint;
				distance = currentDistance;
			}
		}
		
		return closestGoalPoint;
	}
	
	/**
	 * Draws action selection menu
	 */
	public enObjectAction DisplaySelectActionMenu(Vector3 actionMenuCenter, enObjectAction[] actions) {
		//GUI.skin = gameSkin;
		//int positionChangeX = 15;
		//int positionChangeY = 0;
		float r = 40;
		for (int i = 0; i < actions.Length; i++) {
			enObjectAction action = actions[i];
			float posX = actionMenuCenter.x;
			float posY = actionMenuCenter.y;
			switch (i) {
				case 1: 
					// up-right
        			posY += (float) System.Math.Sqrt(System.Math.Pow(r,2)-System.Math.Pow((r/2),2));
					posX += r/2;
        			break;
    			case 2:
					// right
        			posX += r;
        			break;
				case 3:
					// left
        			posX -= r;
        			break;
				case 4:
					// down-left
					posY -= (float) System.Math.Sqrt(System.Math.Pow(r,2)-System.Math.Pow((r/2),2));
					posX -= r/2;
        			break;
				case 5:
					// down-right
					posY -= (float) System.Math.Sqrt(System.Math.Pow(r,2)-System.Math.Pow((r/2),2));
					posX += r/2;
        			break;
    			default:
					// up-left
        			posY += (float) System.Math.Sqrt(System.Math.Pow(r,2)-System.Math.Pow((r/2),2));
					posX -= r/2;
        			break;
			}
			
			if (DisplaySelectActionButton(new Rect(posX-20, Screen.height-posY-20, 40, 40), objectActionToButton[action], buttonStyle)) {
				return action;
			}
			//positionChangeY += 35;
		}
		return enObjectAction.NoAction;
	}
	
	/*
	 * Draws action menu button and checks if mouse is released over it.
	 * reference: http://answers.unity3d.com/questions/315724/loading-gui-in-middle-of-mouse-and-make-it-work-ra.html
	 */
	private bool DisplaySelectActionButton(Rect aRect, GUIContent aContent, GUIStyle aStyle) {
		Event e = Event.current;
    	bool isOver = aRect.Contains(e.mousePosition);
    	if (e.type == EventType.Repaint)
        	aStyle.Draw(aRect, aContent, isOver, true, true, false);
    	else if (isOver && e.type == EventType.MouseUp)
        	return true;
    	return false;
	}
}
