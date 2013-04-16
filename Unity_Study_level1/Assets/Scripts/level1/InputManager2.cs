using UnityEngine;
using System.Collections;

public class InputManager2 : MonoBehaviour {
	
	
	private Vector3 screenPoint;
    private Vector3 offset;
	public static InputManager2 instance;
	
	public GameObject player;
	private RaycastHit hit;
	private RaycastHit defaultHit;
	private LayerMask layerMask = 1 << 8;
	//public TouchManager instance;
	
//#define
	
	
	void Awake(){
		instance = this;
		//instance.hitPlane.renderer.enabled = false;
		
	}
	
	void Update(){
#if UNITY_ANDROID || UNITY_IPHONE
		TapInput();
#endif
		
#if  UNITY_WEBPLAYER
		MouseInput();
#endif
		
	}
	// Use this for initialization
	void Start () {
		//instance.hit.renderer.enabled = false;
		
		//
		player = GameObject.Find("Player");
		
	}
	
	//IF webplayer call
	void MouseInput(){
		if(Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			//hitPlane.transform.position = new Vector3(hitPlane.transform.position.x, hitPlane.transform.position.y, player.transform.position.z);
			//Vector3 hitPlanePos = hitPlane.transform.position;
			if(Physics.Raycast(ray , out hit, 100.0f/*, layerMask*/))
			{
				//Debug.Log("HIT!!");
				Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
				//Debug.Log(hit.collider.gameObject.name);
				if(hit.collider.gameObject.name == "hitPlane"){
					//Debug.Log("HIT!!");
					Vector3 hitPlanePos = hit.transform.position;
					//player.SendMessage("MovePlayer",hitPlanePos);
					//Debug.Log ("" +hit.point.x+","+hit.point.y+","+player.transform.position.z);
					//player.transform.position = new Vector3(hit.point.x,hit.point.y,player.transform.position.z);
					player.SendMessage("MovePlayerTo",new Vector3(hit.point.x,hit.point.y,player.transform.position.z));
				}
				//hit.collider.gameObject.SendMessage("MoveToGoal");
			}else{
				Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue);
			}
			
		}else if(Input.GetMouseButton(0)){
			//Debug.Log("PUSHING!!");
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if(Physics.Raycast(ray , out hit, 100.0f/*, layerMask*/))
			{
				//Debug.Log(hit.collider.gameObject.name);
				if(hit.collider.gameObject.name == "Player"){
					Debug.DrawRay(ray.origin, ray.direction * 10, Color.green);
					//Debug.Log("GUARD!!");
					Vector3 hitPlanePos = hit.transform.position;
					player.SendMessage("Guard");
				}
			}else{
				Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue);
			}
		}else if(Input.GetMouseButtonUp(0)){
			//Debug.Log("PUSHING!!");
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if(Physics.Raycast(ray , out hit, 100.0f/*, layerMask*/))
			{
				//Debug.Log(hit.collider.gameObject.name);
				if(hit.collider.gameObject.name == "Player"){
					Debug.DrawRay(ray.origin, ray.direction * 10, Color.green);
					Vector3 hitPlanePos = hit.transform.position;
					player.SendMessage("Act_NORMAL");
				}
			}else{
				Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue);
			}
		}
	}
	
	void TapInput(){
		
		//Touch
		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Began)
			{
				/*
				Ray ray = Camera.main.ScreenPointToRay (touch.position);
				//Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
				if(Physics.Raycast(ray , out hit, 100.0f, layerMask))
				{
					Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
					Debug.Log(hit.collider.gameObject.name);
					Debug.Log("hit!!");
					//hit.collider.gameObject.SendMessage("MoveToGoal");
					
				}else{
					Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue);
				}
				*/
				Ray ray = Camera.main.ScreenPointToRay (touch.position);
				//hitPlane.transform.position = new Vector3(hitPlane.transform.position.x, hitPlane.transform.position.y, player.transform.position.z);
				//Vector3 hitPlanePos = hitPlane.transform.position;
				if(Physics.Raycast(ray , out hit, 100.0f/*, layerMask*/))
				{
					//Debug.Log("HIT!!");
					Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
					//Debug.Log(hit.collider.gameObject.name);
					if(hit.collider.gameObject.name == "hitPlane"){
						//Debug.Log("HIT!!");
						Vector3 hitPlanePos = hit.transform.position;
						player.SendMessage("MovePlayerTo",new Vector3(hit.point.x,hit.point.y,player.transform.position.z));
					}
				//hit.collider.gameObject.SendMessage("MoveToGoal");
				}else{
					Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue);
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
	
	
	/*
	void OnMouseDown() {
        this.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        this.offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag() {
        Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + this.offset;
        transform.position = currentPosition;
    }*/
	
		


	
}
