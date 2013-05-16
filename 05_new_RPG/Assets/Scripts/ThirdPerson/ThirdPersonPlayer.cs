using UnityEngine;
using System.Collections;

public class ThirdPersonPlayer : MonoBehaviour {
	
	private float PlayerSpeed = 6.0f;
	
	
	//states
	private int state =0;
	private int MOVE = 1;
	private int STOP = 2;
	
	//Move Goal 
	private Vector3 start_v3 = Vector3.zero;
	private Vector3 goal_v3 = Vector3.zero;
	private Vector3 v = Vector3.zero;
	private float speedlate = 0.05f;
	
	//Third person Camera
	public Camera MainCamera;
	
	
	//Debug Ray
	private Ray ray,Cray;
	private RaycastHit hit,Chit;
	private LayerMask EnemylayerMask =  1 << 10;
	private LayerMask ObjlayerMask =  1 << 11;
	
	private GameObject enemy;
	private GameObject InputManager;
	
	private GameObject distractObj;
	private Material m_wall_sp;
	
	private float distance = 10.0f;
	
	
	void Awake(){
		state = state;
		this.MainCamera = GameObject.Find("Main Camera").camera;
		enemy = GameObject.Find("Enemy");
		InputManager = GameObject.Find("InputManager");
		
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		//For Debug
		Vector3 VtoC = this.transform.position - this.MainCamera.transform.position;
		Vector3 VtoCforCamera = this.transform.position - this.MainCamera.transform.position;
		VtoC.y = 0.0f;
		VtoC.Normalize();
		VtoCforCamera.Normalize();
		
		//Player Ray
		ray = new Ray(this.transform.position,this.transform.forward);
		//Cray = new Ray(this.MainCamera.transform.position,this.MainCamera.transform.forward);
		Cray = new Ray(this.MainCamera.transform.position,VtoCforCamera);
		Debug.DrawRay(ray.origin, ray.direction * 10, Color.white);
		Debug.DrawRay(Cray.origin, Cray.direction * 10, Color.red);
		
		if(Physics.Raycast(ray , out hit, 100.0f, EnemylayerMask)){
			//Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
			if(hit.collider.gameObject.tag ==  "enemy"){
				Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
				Debug.Log("EnemyHit!");
				
			}else if(hit.collider.gameObject.tag ==  "NPC"){
				Debug.Log("HitsObj:"+ hit.collider.gameObject.name);
				Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
				InputManager.SendMessage("kaiwaButton_enabled",true);
				//InputManager.SendMessage("kaiwaButton_enabled");
				
			}else{
				InputManager.SendMessage("kaiwaButton_enabled",false);
			}
		}else{
			InputManager.SendMessage("kaiwaButton_enabled",false);
		}
		
		if(Physics.Raycast(Cray , out  Chit, distance, ObjlayerMask)){
			if( Chit.collider.gameObject.tag ==  "Object"){
				Debug.DrawRay( Cray.origin, Cray.direction * 10, Color.blue);
				distractObj = GameObject.Find(Chit.collider.gameObject.name);
				//distractObj.renderer.material.color =se; Color.white;
				//distractObj.renderer.enabled = false;
				distractObj.SendMessage("transparentize");
				Debug.Log("HitsObj:"+ Chit.collider.gameObject.name);
			}
		}
		
		
		
		
		
		//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(VtoC), 0.1f);
		//transform.rotation = Quaternion.LookRotation(VtoC);
	}
	
	void MovePlayerTo(Vector3 pos){
		//change position
        start_v3 = transform.position;
		v = pos - transform.position;
		v.Normalize();
		goal_v3 = pos;
		state = MOVE;
	}
	
	void Guard(){
		//change position
        state = STOP;
	}
	
	void MoveFront(){
		transform.Translate(Vector3.forward* speedlate *PlayerSpeed);
		//transform.rotation	
	}
	
	void MoveBack(){
		transform.Translate(Vector3.back* speedlate *PlayerSpeed);
	}
	
	void MoveRight(){
		transform.Translate(Vector3.right* speedlate *PlayerSpeed);
	}
	
	void MoveLeft(){
		transform.Translate(Vector3.left* speedlate *PlayerSpeed);
	}
	
	void RotateRight(){
		//transform.rotation = new Vector3(transform.rotation.x+1,transform.rotation.y,transform.rotation.z);
		//transform.Rotate(0.0f,  1.0f, 0);
		// planet to spin on it's own axis
		//transform.Rotate(transform.up * 25.0f * Time.deltaTime);
	}
	
	void RotateLeft(){
		//transform.rotation = new Vector3(transform.rotation.x+1,transform.rotation.y,transform.rotation.z);
		transform.Rotate(0.0f, -1.0f, 0);
	}
}
