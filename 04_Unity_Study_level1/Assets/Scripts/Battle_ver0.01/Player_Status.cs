using UnityEngine;
using System.Collections;

public class Player_Status : MonoBehaviour {
	
	//charactor staus
	public float HP = 100;
	public float AP = 50;
	
	public GameObject PlayerAnchor;
	
	// Use this for initialization
	void Start () {
		//PlayerAnchor1 = GameObject.Find("PlayerStatus_1");
	}
	
	
	void Awake(){
		PlayerAnchor = GameObject.Find("PlayerStatus_1");
		
		InitPlayerStatus();
	}
	
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	//getter
	public float getHP(){
		return this.HP;
	}
	
	public float getAP(){
		return this.AP;
	}
	
	//setter
	public void setHP(float h){
		this.HP = h;
	}
	
	public void setAP(float a){
		this.AP = a;
	}
	
	//Init
	void InitPlayerStatus(){
		PlayerAnchor.transform.FindChild("PlayerNameLabel").GetComponent<UILabel>().text = "Player1";
		PlayerAnchor.transform.FindChild("HPLabel").GetComponent<UILabel>().text = "HP:" + getHP();
		PlayerAnchor.transform.FindChild("APLabel").GetComponent<UILabel>().text = "AP:" + getAP();
	}
}
