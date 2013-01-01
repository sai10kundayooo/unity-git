using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    #region
    public float MinSpeed;
    public float MaxSpeed;
    
    private float currentSpeed = 1.0f;
    private float x, y, z;
    private Transform myTransform;
    #endregion

    // Use this for initialization
	void Start () {
        transform.position = new Vector3(0, 7.0f, 0);
        setPositionAndSpeed();
	}
	
	// Update is called once per frame
	void Update () {
        float antToMove = currentSpeed * Time.deltaTime;
        transform.Translate(Vector3.down * antToMove);
        
        if (transform.position.y <= -5.0f)
        {
            //Destroy(gameObject);
            setPositionAndSpeed();
        }
	}

    public void setPositionAndSpeed()
    {
        currentSpeed = Random.RandomRange(MinSpeed, MaxSpeed);

        x = Random.RandomRange(-6f, 6f);
        y = 7.0f;
        z = 0.0f;

        transform.position = new Vector3(x, y, z);

    }
}
