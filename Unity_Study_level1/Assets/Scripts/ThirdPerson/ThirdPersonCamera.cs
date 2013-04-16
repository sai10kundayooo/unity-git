using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {
	
	private GameObject targetObject;
	
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private Vector3 viewPoint;

	//CameraState
	private int CameraState =0;
	private int Back = 0;
	private int UpperBack=1;
	private int Upper=2;
	private int Upper_far=3;
	

	private Vector3 distanceVector;
    private Vector3 distanceVector_Back = new Vector3(0.0f,0.0f,-8.0f);
	private Vector3 distanceVector_UpperBack = new Vector3(0.0f,8.0f,-8.0f);
	private Vector3 distanceVector_Upper = new Vector3(0.0f,8.0f,-0.2f);
	private Vector3 distanceVector_Upper_far = new Vector3(0.0f,18.0f,-0.2f);
	
    private Vector3 relativeViewPoint;
	private float distance = 0.0f;
	
	private float angle = 0.0f;
	
	//   change camera
	private Vector3 changeViewCamera = Vector3.zero;
	
    void Start () {
    	Initialize();
		
    }

    // Update is called once per frame
    void LateUpdate () {
		
    	CaluculateViewPoint();
    	CalculateCameraMovement();
    	ApplyCameraMovement();
		
    }

    void Initialize(){
		targetObject = GameObject.Find("Player");
		CameraState = 0;
		
		distanceVector = new Vector3();
    	targetPosition = new Vector3();
    	targetRotation = new Quaternion();
    	viewPoint = new Vector3();
		
		distance = Vector3.Distance(targetObject.transform.position,this.transform.position);
    }

    void CalculateCameraMovement(){
		
		//change camerastate
		if(CameraState == Back)distanceVector = distanceVector_Back;
		else if(CameraState == UpperBack)distanceVector = distanceVector_UpperBack;
		else if(CameraState == Upper)distanceVector = distanceVector_Upper;
		else if(CameraState == Upper_far)distanceVector = distanceVector_Upper_far;
		
	    targetPosition = targetObject.transform.position + distanceVector + this.changeViewCamera;
    	targetRotation = Quaternion.LookRotation(viewPoint - this.transform.position);
		if(CameraState == Upper || CameraState == Upper_far) targetRotation = this.targetRotation;
    }

    void CaluculateViewPoint(){
    	viewPoint = targetObject.transform.position + relativeViewPoint;
    }

    void ApplyCameraMovement(){

    	this.transform.position = targetPosition;
    	this.transform.rotation = targetRotation;
		this.transform.LookAt(targetObject.transform);
    }
	
	public void setdistanceVector(){
		if(CameraState == Back)CameraState = UpperBack;
		else if(CameraState == UpperBack)CameraState = Upper;
		else if(CameraState == Upper)CameraState = Upper_far;
		else if(CameraState == Upper_far)CameraState = Back;
		distance = Vector3.Distance(targetObject.transform.position,this.transform.position);		
	}
	
	 public void MoveRight(){
		//Camera Rotate Right
		this.changeViewCamera -= Vector3.left;
	}
	
	public void MoveLeft(){
		//Camera Rotate Right
		this.changeViewCamera -= Vector3.right;
	}
	
	void DoubleRotateRight(){
		Debug.Log("DRR"+distance);
		this.changeViewCamera = new Vector3(Mathf.Cos(angle) * distance, 0.0f, Mathf.Sin(angle) * distance);
		angle += 0.03f;
	}
	
	void DoubleRotateLeft(){
		Debug.Log("DRF");
		transform.RotateAround(targetPosition, Vector3.forward, -30);
	}
}