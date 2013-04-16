using UnityEngine;
using System.Collections;

public class CameraManager_ver2 : MonoBehaviour {
	
	//
	Vector3 vectorToCamera;
	
	private GameObject targetObject;	//
	private GameObject[] distractObj;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
	private Vector3 viewPoint;
	
	//CameraPositon
    private Vector3 wantedPosition;
	private Vector3 xzVectorToCamera;
	private float distance , yHeight , damping , angle;
	
	private Ray ray;
	private RaycastHit hit;
	private LayerMask ObjMask =  1 << 11;
	
	// Use this for initialization
	void Start () {
		Initialize();
	}
	
	 void Initialize(){
		targetObject = GameObject.Find("Player");
    	targetPosition = new Vector3();
    	targetRotation = new Quaternion();
		
		distractObj = GameObject.FindGameObjectsWithTag("Object");
		
    	viewPoint = new Vector3();
		
		wantedPosition = new Vector3();
		xzVectorToCamera = new Vector3();
		
		distance = 10.0f;
		yHeight = 5.0f;
		damping = 100.0f;
		angle = 0.00f;
		
		Vector3 pos = targetObject.transform.position;
		transform.position = new Vector3(pos.x + Mathf.Cos(angle) * distance , pos.y + yHeight, pos.z + Mathf.Sin(angle) * distance);
    }
	
	// Update is called once per frame
	void Update () {
		//Calclate Camera Positon
		CalcCameraPosition();
		
		CalcCameraRotate();
		
		//transparentizeObj();
	}
	
	/*
	private void transparentizeObj(){
		if(Physics.Raycast(ray , out hit, 100.0f, ObjMask)){
			 Debug.DrawRay(ray.origin, ray.direction * 10, Color.green);
			//Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
			if(hit.collider.gameObject.tag ==  "Object"){
				//hit.collider.gameObject
				Debug.Log("HitsObj:"+hit.collider.gameObject.name);
			}	
		}
	}
	*/
	
	private void CalcCameraPosition(){
		vectorToCamera = this.transform.position - targetObject.transform.position;
		
		xzVectorToCamera = new Vector3(vectorToCamera.x,0.0f,vectorToCamera.z);
		xzVectorToCamera.Normalize();
			
		wantedPosition = targetObject.transform.position + xzVectorToCamera*distance;
		wantedPosition.y += yHeight;
		transform.position = Vector3.Lerp(transform.position,wantedPosition,damping*Time.deltaTime);
	}
	
	private void CalcCameraRotate(){
		targetRotation = Quaternion.LookRotation(-vectorToCamera);
		transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation,damping*Time.deltaTime);
	}
	
	void RotateRight(){
		Vector3 pos = targetObject.transform.position;
		transform.LookAt(pos);	// カメラをtargetの方向へ向かせるように設定する
 
		// オブジェクトの周りをカメラが円運動する
		Vector3 wantRotate = new Vector3(pos.x + Mathf.Cos(angle) * distance, pos.y + yHeight, pos.z + Mathf.Sin(angle) * distance);
		//transform.position = wantRotate;
		transform.position = Vector3.Lerp(transform.position,wantRotate,damping*Time.deltaTime);
		angle -= 0.05f;
	}
	
	void RotateLeft(){
		Vector3 pos = targetObject.transform.position;
		transform.LookAt(pos);	// カメラをtargetの方向へ向かせるように設定する
 
		// オブジェクトの周りをカメラが円運動する
		Vector3 wantRotate = new Vector3(pos.x + Mathf.Cos(angle) * distance, pos.y + yHeight, pos.z + Mathf.Sin(angle) * distance);
		//transform.position = wantRotate;
		transform.position = Vector3.Lerp(transform.position,wantRotate,damping*Time.deltaTime);
		angle += 0.05f;
	}
}
