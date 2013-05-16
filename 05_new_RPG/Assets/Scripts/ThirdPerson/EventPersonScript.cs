using UnityEngine;
using System.Collections;

public class EventPersonScript : MonoBehaviour {
	
	private GameObject player;
	
	// Use this for initialization
	void Start () {
	
	}
	
	void Awake(){
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), 0.1f);
	}
}
