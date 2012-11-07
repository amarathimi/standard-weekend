using UnityEngine;
using System.Collections;

public class TestMoveScript : MonoBehaviour {
	
	public GameObject currentLocation;
	GameObject currentDestination = null;
	
	// Use this for initialization
	void Start () {
		//iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("TestPath"), "orienttopath", true, "time", 5, "easetype", iTween.EaseType.easeInOutSine));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void SetMoveDestination(GameObject goalPoint) {
		if (currentLocation.name.Equals(goalPoint.name)) {
			// no need to move
			return;
		}
		
		currentDestination = goalPoint;
		
		string pathName = currentLocation.name + "to" + goalPoint.name;
		string reversePathName = goalPoint.name + "to" + currentLocation.name;
		
		Vector3[] path = iTweenPath.GetPath(pathName);
		Vector3[] reversePath = iTweenPath.GetPathReversed(reversePathName);
		
		if (path != null) {
			iTween.MoveTo(gameObject, iTween.Hash("path", path, "orienttopath", true, "time", 5, "easetype", iTween.EaseType.easeInOutSine, "oncomplete", "UpdateCurrentLocation"));
		} else if (reversePath != null) {
			iTween.MoveTo(gameObject, iTween.Hash("path", reversePath, "orienttopath", true, "time", 5, "easetype", iTween.EaseType.easeInOutSine, "oncomplete", "UpdateCurrentLocation"));
		}
		
		//iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("TestPath"), "orienttopath", true, "time", 5, "easetype", iTween.EaseType.easeInOutSine, "oncomplete", "UpdateCurrentLocation"));
	}
	
	private void UpdateCurrentLocation() {
		currentLocation = currentDestination;
		currentDestination = null;
		print ("New location : " + currentLocation.name);
	}
}
