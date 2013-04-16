using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {
	
	private int ScreenWidth;
	private int ScreenHeight;
	
	// Use this for initialization
	void Start () {
	
	}
	
	void Awake(){
		this.ScreenWidth = Screen.currentResolution.width;
		this.ScreenHeight = Screen.currentResolution.height;
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
