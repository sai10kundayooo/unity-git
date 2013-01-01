using UnityEngine;
using System.Collections;

public class InputManager_ThirdPerson : MonoBehaviour {
	
	
	private Vector3 screenPoint;
    private Vector3 offset;
	public static InputManager_ThirdPerson instance;
	
	//Other GameObjects
	private GameObject player;
	private GameObject enemy;
	private GameObject cursor;
	private GameObject cursor_p;
	private GameObject red_mark_enemy;
	
	//Ray
	private Ray ray;
	private Ray UIray;
	//Raycasts
	private RaycastHit hit;
	private RaycastHit defaultHit;
	private RaycastHit UIhit;
	private LayerMask layerMask = 1 << 8;
	private LayerMask UIlayerMask = 1 << 9;
	private LayerMask EnemylayerMask = 1 << 10;
	
	private Camera UICamera;
		
	void Awake(){
		instance = this;
		UICamera = GameObject.Find("UICamera").camera;
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
		player = GameObject.Find("Player");
		enemy = GameObject.Find("Enemy");
		cursor = GameObject.Find("cursor");
		cursor_p = GameObject.Find("cursor_p");
		red_mark_enemy = GameObject.Find("RedMark");
	}
	
	//IF webplayer call
	void MouseInput(){
		
		//Debug.Log ("mouse position = "+Input.mousePosition);
		//Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		UIray = UICamera.ScreenPointToRay (Input.mousePosition);
		if(Physics.Raycast(ray , out hit, 100.0f,layerMask)){
			if(hit.collider.gameObject.name ==  "Plane"){
				//Debug.DrawRay(UIray.origin, UIray.direction * 10, Color.red);
				cursor.transform.position = new Vector3(hit.point.x,hit.point.y,hit.point.z);
			}
		}
			
		if(Input.GetMouseButtonDown(0))
		{
			UtilityRayCast();
		}
	}
	
	void TapInput(){
		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Began)
			{
				ray = Camera.main.ScreenPointToRay (touch.position);
				UIray = UICamera.ScreenPointToRay (touch.position);
		
				UtilityRayCast();
			}
		}
	}
	
	
	void UtilityRayCast(){
		//if UIPanle input
		if(Physics.Raycast(UIray , out UIhit, 100.0f)){
				
			//If NGUI_Button Pushed
			if(UIhit.collider.gameObject.name ==  "ViewButton"){
				Debug.DrawRay(UIray.origin, UIray.direction * 10, Color.red);
			}else if(UIhit.collider.gameObject.name ==  "ActionCommand1"){
				Debug.DrawRay(UIray.origin, UIray.direction * 10, Color.red);
				Debug.Log("action1");
			}else if(UIhit.collider.gameObject.name ==  "ActionCommand2"){
				Debug.DrawRay(UIray.origin, UIray.direction * 10, Color.red);
				Debug.Log("action2");
			}else if(UIhit.collider.gameObject.name ==  "ActionCommand3"){
				Debug.DrawRay(UIray.origin, UIray.direction * 10, Color.red);
				Debug.Log("action3");
			}else if(UIhit.collider.gameObject.name ==  "GuardButton"){
				Debug.DrawRay(UIray.origin, UIray.direction * 10, Color.red);
				player.SendMessage("Guard");
			}else if(UIhit.collider.gameObject.name ==  "BackStepButton"){
				Debug.DrawRay(UIray.origin, UIray.direction * 10, Color.red);
			}
			
			
				
		}else{		
			if(Physics.Raycast(ray , out hit, 100.0f, EnemylayerMask)){
				Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
				if(hit.collider.gameObject.tag ==  "enemy"){
					Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
					Debug.Log("EnemyHit!");
					//red_mark_enemy.transform.position = new Vector3(hit.point.x,hit.point.y + 1.5f,hit.point.z);
					enemy.SendMessage("changeTargetState");	
				}	
			}else if(Physics.Raycast(ray , out hit, 100.0f, layerMask)){		
				if(hit.collider.gameObject.name ==  "Plane"){
					Vector3 hitPlanePos = hit.transform.position;
					cursor_p.transform.position = new Vector3(hit.point.x,hit.point.y,hit.point.z);
					player.SendMessage("MovePlayerTo",new Vector3(hit.point.x,player.transform.position.y,hit.point.z));
				}
			}
		}
	}
		
		


	
}
