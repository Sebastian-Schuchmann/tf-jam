using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

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

public class PercentageController : MonoBehaviour
{
    public TextMeshPro Percentage;
    public TextMeshPro Count;

    // Update is called once per frame
    void Update()
    {
        Percentage.text = String.Format("{0:0.00}%",
            BallController.SuccessCount / (float)BallController.ShotCount * 100f);

        Count.text = "times scored: " + BallController.SuccessCount;
    }
}
