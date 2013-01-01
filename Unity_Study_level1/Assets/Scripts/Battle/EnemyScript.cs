using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	
	private Vector3 position;
	private int TargetedState=0;
	private int NoTarget=0;
	private int Hate=1;
	private int Friendly=2;
	private GameObject red_mark_enemy;
	
	// Use this for initialization
	void Start () {
		TargetedState = NoTarget;
		red_mark_enemy = GameObject.Find("RedMark");
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if(this.TargetedState == NoTarget){
			position = new Vector3(0.0f,-20.0f,0.0f);
		}else if(this.TargetedState == Hate){
			position = this.transform.position;
		}
		red_mark_enemy.transform.position = new Vector3(position.x,position.y + 1.5f,position.z);
		//red_mark_enemy.SendMessage("changePosition",position);	
	}
	
	void changeTargetState(){
		if(this.TargetedState == NoTarget) this.TargetedState = Hate;
		else if(this.TargetedState == Hate) this.TargetedState = NoTarget;
	}
}
