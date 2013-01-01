using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public float ProjectileSpeed;
    //public GameObject Projectile_Prehab;
    public GameObject Explosion_Prehab;
    private Transform myTransform;

    public float elimination_time = 2.0f;

	// Use this for initialization
	void Start () {
        myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
        float antToMove = ProjectileSpeed + Time.deltaTime;
        transform.Translate(Vector3.up * antToMove / 10);

        if (myTransform.transform.position.y > 6.4f) {
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter(Collider otherObject) {

        //Debug.Log("We hit:" + otherObject.name);

        if(otherObject.tag == "enemy"){
            //Destroy(otherObject);
            /*
            float x = Random.RandomRange(-6f, 6f);
            float y = 7.0f;
            float z = 0.0f;
            otherObject.transform.position = new Vector3(x,
                                                         7.0f,
                                                         otherObject.transform.position.z
                                                         );
             */
            Enemy enemy = (Enemy)otherObject.gameObject.GetComponent("Enemy");
            //Instantiate(Explosion_Prehab,enemy.transform.position,enemy.transform.rotation);
            enemy.setPositionAndSpeed();

            //Destroy(enemy, elimination_time);
            Destroy(gameObject);
        }
    }
}
