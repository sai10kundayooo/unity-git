using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    private float PlayerSpeed = 4.2f;
    public GameObject Projectile_prehab;
	public int HitPoint = 50;
	
	
	//states
	public int state =0;
	public int MOVE = 1;
	public int STOP = 2;
	
	//player_actionstates
	public int action_state =0;
	private int NORMAL = 1;
	private int GUARD = 2;
	
	
	//Move Goal 
	private Vector3 start_v3 = Vector3.zero;
	private Vector3 goal_v3 = Vector3.zero;
	private Vector3 v = Vector3.zero;
	private float speedlate = 0.05f;
	
	// Use this for initialization
	void Start () {
        transform.position = new Vector3(0,-4,transform.position.z);
	}
	
	void Awake(){
		action_state = NORMAL;
	}
	
	// Update is called once per frame
	void Update () {
        
		
		//ForDebug
        //Move
        //float antToMove = Input.GetAxis("Horizontal") * PlayerSpeed * Time.deltaTime;     
        
        //change position
        //transform.Translate(Vector3.right * antToMove);
        
		
		//Fire
		if (Input.GetKeyDown("space")) {

            //Fire projectile
            Vector3 position = new Vector3(transform.position.x, transform.position.y + (transform.localScale.y / 2), transform.position.z);
            Instantiate(Projectile_prehab, position, Quaternion.identity);

        }
		
		//
		if(state == MOVE/* && transform.position != goal_v3*/){
			if((transform.position.x >= goal_v3.x - 0.3f && transform.position.x <= goal_v3.x + 0.3f) 
				&& (transform.position.y >= goal_v3.y - 0.3f && transform.position.y <= goal_v3.y + 0.3f)
				&& (transform.position.z >= goal_v3.z - 0.3f && transform.position.z <= goal_v3.z + 0.3f)
			){
				Debug.Log("STOP!!!!");
				state = STOP;
			}
			
			transform.Translate(v.x * speedlate *PlayerSpeed,v.y * speedlate *PlayerSpeed ,v.z * speedlate *PlayerSpeed);
		}
		
		if(state == STOP){
			//Vector3 nowVector_v3 = new Vector3(transform.position.x - goal_v3);
			
			transform.Translate(Vector3.zero);
		}

	}
	
	void MovePlayerTo(Vector3 pos){
		//change position
        //Vector3 v = pos - transform.position;
		start_v3 = transform.position;
		v = pos - transform.position;
		v.Normalize();
		//transform.Translate(v.x * PlayerSpeed * Time.deltaTime,v.y * PlayerSpeed * Time.deltaTime,v.z * PlayerSpeed * Time.deltaTime);
		goal_v3 = pos;
		state = MOVE;
	}
	
	void Guard(){
		//Debug.Log("called Guard()");
		this.action_state = GUARD;
	}
	
	void Act_NORMAL(){
		Debug.Log("NOMARIZE!!");
		this.action_state = NORMAL;		
	}
	void OnTriggerEnter(Collider c){
		//Debug.Log("UWAAAAAAAAAAA!!");
		if(c.tag == "enemy"){
			if(this.action_state == NORMAL){
				Debug.Log("UWAAAAAAAAAAA!!");
            	this.HitPoint--;	
			}else if(this.action_state == GUARD){
				Debug.Log("HYHHHHHHHHHH");
			}
			
        }
	}
}
