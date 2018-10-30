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
public class PlayerController : MonoBehaviour
{
    public float Speed;
    public GameObject Body;
    Rigidbody Rigidbody;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(
            Input.GetAxis("Horizontal") * Speed * Time.deltaTime,
            0f,
            Input.GetAxis("Vertical") * Speed * Time.deltaTime
        );

        Rigidbody.MovePosition(Vector3.MoveTowards(
            transform.position,
            transform.position + movement,
            Speed
        ));
    }
}
