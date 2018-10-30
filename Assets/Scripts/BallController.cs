using System;
using System.Collections;
using UnityEngine;

/* Most of the source code here was made by
 * https://github.com/abehaskins/tf-jam 
 * https://twitter.com/abeisgreat
 * 
 * Check out his original Medium Post:
 * https://medium.com/tensorflow/tf-jam-shooting-hoops-with-machine-learning-7a96e1236c32
 * Thank you!
 * 
 * I took his example and made it work with ML-Agents and PPO. 
 */

public class BallController : MonoBehaviour
{
    public Vector3 Force;
    public float Distance;
    public static int SuccessCount = 0;
    public static int ShotCount = 1;
    public BallSpawnerController ballSpawnerController;

    public Material MaterialBallScored;
    private Vector3 Scaler = new Vector3(1000, 1000, 1000);

    private bool hasBeenScored = false;
    private bool hasTriggeredTop = false;

    // Use this for initialization
    void Start()
    {
        var scaledForce = Vector3.Scale(Scaler, Force);
        GetComponent<Rigidbody>().AddForce(scaledForce);
        StartCoroutine(DoDespawn(10));
        ShotCount++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "TriggerTop")
        {
            hasTriggeredTop = true;
        }
        else if (other.name == "TriggerBottom")
        {
            if (hasTriggeredTop && !hasBeenScored)
            {
                //Change Color
                GetComponent<Renderer>().material = MaterialBallScored;
                SuccessCount++;

                //Reward for hitting
                ballSpawnerController.AddReward(1.0f);
                ballSpawnerController.AgentReset();

                ballSpawnerController.RequestDecision();

                //For performance reasons we destroy the Ball
                Destroy(gameObject);
            }
            hasBeenScored = true;
        }
    }

    //Gets called when the ball gets destroyed - see KillBalls.cs
    private void OnDestroy()
    {
        //ballSpawnerController.AddReward(-0.1f);
        ballSpawnerController.RequestDecision();
    }

    IEnumerator DoDespawn(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Court")
        {
            StartCoroutine(DoDespawn(.5f));
        }
    }
}
