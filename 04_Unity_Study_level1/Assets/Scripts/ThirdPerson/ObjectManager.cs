using UnityEngine;
using System.Collections;

public class ObjectManager : MonoBehaviour {
	
	private GameObject transObj;
	public ObjectManager instance;
	
	bool istransparentize = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	void Awake(){
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		//renderer.material.
		this.renderer.enabled = true;
	}
	
	void transparentize(){
		this.renderer.enabled = false;
		
	}
}
