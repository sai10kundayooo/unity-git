using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {
	
	//
	Vector3 vectorToCamera;
	
	private GameObject targetObject;	//
	private GameObject[] distractObj;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
	private Vector3 viewPoint;
	
	//CameraPositon
    private Vector3 initPosition;
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
		
		initPosition = new Vector3(20,50,20);
		
		targetObject = GameObject.Find("Player");
    	targetPosition = new Vector3();
    	targetRotation = new Quaternion();
		
		distractObj = GameObject.FindGameObjectsWithTag("Object");
		
    	viewPoint = new Vector3();
		
		this.transform.position = initPosition;
		wantedPosition = new Vector3();
		xzVectorToCamera = new Vector3();
		
		distance = 10.0f;
		yHeight = 5.0f;
		damping = 100.0f;
		angle = 0.00f;
		
		//Vector3 pos = targetObject.transform.position;
		//transform.position = new Vector3(pos.x + Mathf.Cos(angle) * distance , pos.y + yHeight, pos.z + Mathf.Sin(angle) * distance);
    }
	
	// Update is called once per frame
	void Update () {
		//Calclate Camera Positon
		CalcCameraPosition();
		
		
		CalcCameraRotate();
		
		//transparentizeObj();
	}
	
	private void CalcCameraPosition(){
		
		//Back 
		
		//vectorToCamera = this.transform.position - targetObject.transform.position;
		vectorToCamera = this.transform.position - new Vector3(0,0,0);
		
		/*
		xzVectorToCamera = new Vector3(vectorToCamera.x,0.0f,vectorToCamera.z);
		xzVectorToCamera.Normalize();
			
		wantedPosition = targetObject.transform.position + xzVectorToCamera*distance;
		wantedPosition.y += yHeight;
		transform.position = Vector3.Lerp(transform.position,wantedPosition,damping*Time.deltaTime);
		*/
		//
		
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