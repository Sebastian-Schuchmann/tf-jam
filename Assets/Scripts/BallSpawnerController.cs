using System;
using UnityEngine;
using Random = UnityEngine.Random;
using MLAgents;

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

public class BallSpawnerController : Agent
{
    private float lastTime;
    public Transform TransformGoal;
    public Transform TransformAim;

    public GameObject PrefabBall;
    public MovingHoop Hoop;

    private Vector2 goalVector;
    private Vector2 positionVector;

    private float perc = 0.85f;
    void Start()
    {
        //This is agent is set to on demand decision making
        //so we have to manually request a decision
        RequestDecision();
        lastTime = Time.time;
    }

    #region ML-Agents
    // OBSERVATION
    public override void CollectObservations()
    {
        UpdateGoalAndPositionVector();

        var dir = (goalVector - positionVector).normalized;
        var distanceToCourt = (goalVector - positionVector).magnitude;
        //Normalize Distance
        distanceToCourt = 1 - distanceToCourt / 25.4f * 2;

        // Add Observation to Agent
        // This is everything the agent know about the enviroment
        AddVectorObs(distanceToCourt);
        //Angle to Court
        AddVectorObs(dir.x);
        AddVectorObs(dir.y);
//        Debug.Log(Hoop.getVelocity());
        AddVectorObs(Hoop.getVelocity());
    }

    // ACTION
    public override void AgentAction(float[] vectorAction, string textAction)
    {
       // perc += 1f;
        Debug.Log(perc);
        //Calculate Direction and Distance
        var dir = (goalVector - positionVector).normalized;
        var dist = (goalVector - positionVector).magnitude;
        //var arch = 0.5f;
       // Debug.Log(dir);
     //   var closeness = Math.Min(10f, dist) / 10f;
        float force = (vectorAction[0] + 1) / 2;
        float directionX = (vectorAction[1] - 1) / 4;
        float directionY = (vectorAction[2] ) / 2;

    //    force = Mathf.Lerp(GetForceRandomly(), force, perc); 
    //    directionX = Mathf.Lerp(GetForceRandomly(), directionX, perc); 
    //    directionY = Mathf.Lerp(GetForceRandomly(), directionY, perc); 

        //Spawn Ball
        var ball = Instantiate(PrefabBall, transform.position, Quaternion.identity);
        var bc = ball.GetComponent<BallController>();
        bc.ballSpawnerController = this;
//        Debug.Log("Direction X: " + directionX);
  //      Debug.Log("Direction Y: " + directionY);
        //Direction Vector of Ball - props again to https://twitter.com/abeisgreat
        bc.Force = new Vector3(
            directionX,
            //This is the only parameter the agent controls
            force,//* (1f / closeness) Optional: Uncomment this to experiment with artificial shot arcs!
            directionY
        );

        bc.Distance = dist;

        // * UNCOMMENT HERE WHEN TRAINING *
      MoveToRandomDistance();
    }
    #endregion

    private void FixedUpdate()
    {
        //RequestDecision();
        TransformAim.LookAt(TransformGoal);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space)){
            if(lastTime < Time.time){
                RequestAction();
                lastTime = Time.time + 0.1f;
            }
        }

    }


    float GetForceRandomly(float distance = 0f) => Random.Range(-1f, 1f);

    void MoveToRandomDistance()
    {
        var newPosition = new Vector3(TransformGoal.position.x + Random.Range(2.5f, 16f), transform.parent.position.y, TransformGoal.position.z);
        transform.parent.position = newPosition;
    }

    void UpdateGoalAndPositionVector()
    {
        goalVector = new Vector2(
            TransformGoal.position.x,
            TransformGoal.position.z);

        positionVector = new Vector2(
            transform.position.x,
            transform.position.z);
    }
}