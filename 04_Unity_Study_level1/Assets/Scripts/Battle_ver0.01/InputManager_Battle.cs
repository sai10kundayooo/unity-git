using UnityEngine;
using System.Collections;

public class InputManager_Battle : MonoBehaviour {
	
	
	//Ray
	private Ray ray;
	private Ray UIray;
	
	//Raycasts
	private RaycastHit hit;
	private RaycastHit defaultHit;
	private RaycastHit UIhit;
	
	// Use this for initialization
	void Start () {
	
	}
	
	void Update(){
#if UNITY_ANDROID || UNITY_IPHONE
		TapInput();	
#endif
		
#if  UNITY_WEBPLAYER
		MouseInput();
#endif
	}
	
	void TapInput(){
		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Began)
			{
				
			}
		}
	}
	
	
	void UtilityRayCast(){
		//if UIPanle input
		if(Physics.Raycast(UIray , out UIhit, 100.0f)){
				
			
		}else{		
			
		}
	}
	
}
