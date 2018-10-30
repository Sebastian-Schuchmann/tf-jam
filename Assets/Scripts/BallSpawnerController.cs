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
    public Transform TransformGoal;
    public Transform TransformAim;

    public GameObject PrefabBall;

    private Vector2 goalVector;
    private Vector2 positionVector;

    void Start()
    {
        //This is agent is set to on demand decision making
        //so we have to manually request a decision
        RequestDecision();
    }

    #region ML-Agents
    // OBSERVATION
    public override void CollectObservations()
    {
        UpdateGoalAndPositionVector();

        var distanceToCourt = (goalVector - positionVector).magnitude;
        //Normalize Distance
        distanceToCourt = 1 - distanceToCourt / 25.4f * 2;

        // Add Observation to Agent
        // This is everything the agent know about the enviroment
        AddVectorObs(distanceToCourt);
    }

    // ACTION
    public override void AgentAction(float[] vectorAction, string textAction)
    {
        //Calculate Direction and Distance
        var dir = (goalVector - positionVector).normalized;
        var dist = (goalVector - positionVector).magnitude;
        var arch = 0.5f;

        var closeness = Math.Min(10f, dist) / 10f;
        float force = (vectorAction[0] + 1) / 2;

        //Spawn Ball
        var ball = Instantiate(PrefabBall, transform.position, Quaternion.identity);
        var bc = ball.GetComponent<BallController>();
        bc.ballSpawnerController = this;

        //Direction Vector of Ball - props again to https://twitter.com/abeisgreat
        bc.Force = new Vector3(
            dir.x * arch * closeness,
            //This is the only parameter the agent controls
            force,//* (1f / closeness) Optional: Uncomment this to experiment with artificial shot arcs!
            dir.y * arch * closeness
        );

        bc.Distance = dist;

        // * UNCOMMENT HERE WHEN TRAINING *
        //  MoveToRandomDistance();
    }
    #endregion

    private void FixedUpdate()
    {
        TransformAim.LookAt(TransformGoal);
    }


    float GetForceRandomly(float distance) => Random.Range(0f, 1f);

    void MoveToRandomDistance()
    {
        var newPosition = new Vector3(TransformGoal.position.x + Random.Range(2.5f, 23f), transform.parent.position.y, TransformGoal.position.z);
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