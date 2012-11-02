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
			float posX = actionMenuCenter.x-15.0f;
			float posY = Screen.height-actionMenuCenter.y;
			switch (i) {
    			case 1: 
        			posX += (float) System.Math.Sqrt(System.Math.Pow(r,2)-System.Math.Pow((r/2),2));
					posY -= r/2;
        			break;
    			case 2:
        			posX += r;
        			break;
				case 3:
        			posX -= r;
        			break;
				case 4:
					posX -= (float) System.Math.Sqrt(System.Math.Pow(r,2)-System.Math.Pow((r/2),2));
					posY += r/2;
        			break;
				case 5:
					posX += (float) System.Math.Sqrt(System.Math.Pow(r,2)-System.Math.Pow((r/2),2));
					posY += r/2;
        			break;
    			default:
        			posX -= (float) System.Math.Sqrt(System.Math.Pow(r,2)-System.Math.Pow((r/2),2));
					posY -= r/2;
        			break;
			}
			
			if (DisplaySelectActionButton(new Rect(posX, posY, 40, 35), objectActionToButton[action], buttonStyle)) {
				//DisplaySelectActionButton(new Rect(actionMenuCenter.x+positionChangeX, Screen.height-actionMenuCenter.y+positionChangeY, 40, 35), objectActionToButton[action], buttonStyle)
				return action;
			}
			//positionChangeY += 35;
		}
		return enObjectAction.NoAction;
	}
	
	//http://answers.unity3d.com/questions/315724/loading-gui-in-middle-of-mouse-and-make-it-work-ra.html
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
