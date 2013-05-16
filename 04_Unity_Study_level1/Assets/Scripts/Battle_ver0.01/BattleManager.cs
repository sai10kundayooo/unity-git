using UnityEngine;
using System.Collections;

public class BattleManager : MonoBehaviour {
	
	
	private GameObject Player1,Player2,Player3;
	
	private int Turn = 0;
	
	private UISprite RangeStateSprite;
	private UIImageButton DecideSprite;
	
	/*
	 *  0 = far
	 *  1 = middle
	 *  2 = near
	 *  3 = too near
	 */
	private int RangeState = 1;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	void Awake(){
		Player1 = GameObject.Find("Player1");
		
		//Turn Reset
		this.Turn = 0;
		
		//set sprite
		RangeStateSprite =   (UISprite)GameObject.Find("BattleStateSprite").GetComponent("UISprite");
		RangeStateSprite.spriteName = "Middle_Battle";
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void InitStatus(){
		//Set Labels Player1
		Player1.SendMessage("InitPlayerStatus");
		
	}
	
	void MoveFront (){
		if(RangeState <  2){
			RangeState++;
			SetRangeSprite(RangeState);
		}else{ 
			Debug.Log("Error_RangeState");
		}
	}
	
	void MoveBack (){
		if(RangeState > 0){
			RangeState--;
			SetRangeSprite(RangeState);
		}else{
			Debug.Log("Error_RangeState");
		}
	}
	
	void SetRangeSprite(int sp){
		switch(sp){
		case 0:
			RangeStateSprite.spriteName = "Far_Battle";
			break;
		case 1:
			RangeStateSprite.spriteName = "Middle_Battle";
			break;
		case 2:
			RangeStateSprite.spriteName = "Near_Battle";
			break;
		}
	}

}
