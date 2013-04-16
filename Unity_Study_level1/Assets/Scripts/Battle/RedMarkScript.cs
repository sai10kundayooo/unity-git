using UnityEngine;
using System.Collections;

public class RedMarkScript : MonoBehaviour {
	
	private Vector3 position;
	// 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = this.position;
	}
	
	void changePositon(Vector3 p){
		this.position = p;
	}
}
