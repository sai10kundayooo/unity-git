using UnityEngine;
using System.Collections;

public class ThirdPersonPlayer : MonoBehaviour {
	
	private float PlayerSpeed = 3.0f;
	
	//states
	private int state =0;
	private int MOVE = 1;
	private int STOP = 2;
	
	//Move Goal 
	private Vector3 start_v3 = Vector3.zero;
	private Vector3 goal_v3 = Vector3.zero;
	private Vector3 v = Vector3.zero;
	private float speedlate = 0.05f;
	
	
	void Awake(){
		state = state;
		
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		//For Debug
		/*
		//Move
        float antToMove_H = Input.GetAxis("Horizontal") * PlayerSpeed * Time.deltaTime;
		float antToMove_V = Input.GetAxis("Vertical") * PlayerSpeed * Time.deltaTime;
        
        //change position
        transform.Translate(Vector3.right * antToMove_H);
		transform.Translate(Vector3.forward * antToMove_V);
		*/
		if(state == MOVE/* && transform.position != goal_v3*/){
			if((transform.position.x >= goal_v3.x - 0.3f && transform.position.x <= goal_v3.x + 0.3f) 
				&& (transform.position.y >= goal_v3.y - 0.3f && transform.position.y <= goal_v3.y + 0.3f)
				&& (transform.position.z >= goal_v3.z - 0.3f && transform.position.z <= goal_v3.z + 0.3f)
			){
				Debug.Log("STOP!!!!");
				state = STOP;
			}
			
			transform.Translate(v.x * speedlate *PlayerSpeed,0.0f ,v.z * speedlate *PlayerSpeed);
		}
		
		if(state == STOP){
			//Vector3 nowVector_v3 = new Vector3(transform.position.x - goal_v3);
			
			transform.Translate(Vector3.zero);
		}
		
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
}
