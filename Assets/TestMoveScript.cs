using UnityEngine;
using System.Collections;

public class TestMoveScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("TestPath"), "orienttopath", true, "time", 5, "easetype", iTween.EaseType.easeInOutSine));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
