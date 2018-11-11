using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingHoop : MonoBehaviour {

    public BallSpawnerController ballSpawnerController;
    public float speed = 1.0f;
    public Vector3 direction;
    public float maxZ = 4.84465f + 7.5f;
    public float minZ = 4.84465f - 8.7f;

	// Use this for initialization
	void Start () {
        direction = Vector3.forward;
      
	}
	
	// Update is called once per frame
	void FixedUpdate () {


        if (transform.position.z <= minZ)
        {
            ballSpawnerController.RequestDecision();
            direction = Vector3.forward;
         //   Debug.Log("CHANGE");
           // speed = Random.Range(0.01f, 0.2f);
        }

        if (transform.position.z >= maxZ)
        {
            ballSpawnerController.RequestDecision();
         //   Debug.Log("CHANGE");
            direction = Vector3.back;
        }

    //    Debug.Log(direction.z * speed);

        transform.position += direction * speed;
	}

    public float getVelocity(){
        return direction.z * speed;
    }
}
