using UnityEngine;
using System.Collections;

public class TouchManager : MonoBehaviour {
	
	public static TouchManager instance;
	
	private RaycastHit hit;
	private RaycastHit defaultHit;
	private LayerMask layerMask = 1 << 8;
	
	private int target = 0;
	
	void Awake(){
		instance = this;
	}
	
	void Update () {
#if UNITY_ANDROID
		TapInput();
#endif
		
#if  UNITY_WEBPLAYER
		MouseInput();
#endif
	}
	
	//if WEBPLAYER call
	void MouseInput()
	{
		if(Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
			if(Physics.Raycast(ray , out hit, 100.0f, layerMask))
			{
				Debug.Log(hit.collider.gameObject.name);
			}	
		}
	}
	
	//if ANDROID call
	void TapInput()
	{
		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Began)
			{
				Ray ray = Camera.main.ScreenPointToRay (touch.position);
				Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
				if(Physics.Raycast(ray , out hit, 100.0f, layerMask))
				{
						Debug.Log(hit.collider.gameObject.name);
				}
			}
			/*
			else if(touch.phase == TouchPhase.Ended)
			{
				Debug.Log("touch end");
			}
			*/
		}	
	}
	
}
