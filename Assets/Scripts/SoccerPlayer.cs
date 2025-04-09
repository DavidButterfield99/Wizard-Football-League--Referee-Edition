using UnityEngine;

public class SoccerPlayer : MonoBehaviour
{
    GameObject ball;
    Rigidbody ballRb;
    public float spellCasting;
    public float healthPoints = 50f;
    public float strength = 50f;
    public float speedMax = 50f;
    public float acceleration = 50f;
    public string team;
    public string role;

    private float avoidRadius = 15f;
    private bool isBallBlocking;
    private int forceDivider = 5;

    public GameObject goalObject;


    private void Start()
    {
        ball = GameObject.Find("Ball");
        ballRb = ball.GetComponent<Rigidbody>();
    }


    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject == ball)
        {
            KickBall();
        }
    }

    public float GetDistanceFromBall()
    {
        float distance = Vector3.Distance(transform.position, ball.transform.position);
        return distance;
    }

    public void RunTowardsBall()
    {
        float ballToGoal = Vector3.Distance(ball.transform.position, goalObject.transform.position);
        float playerToGoal = Vector3.Distance(transform.position, goalObject.transform.position);
        

        isBallBlocking = ballToGoal + avoidRadius > playerToGoal;

        Vector3 behindBall = ball.transform.position - goalObject.transform.position * avoidRadius;
        Vector3 moveDirection = ball.transform.position;

        Debug.Log(isBallBlocking);
        if (isBallBlocking)
        {
            moveDirection = behindBall + (Vector3.right * speedMax * Time.deltaTime);
        }



        transform.LookAt(moveDirection);
        transform.Translate(Vector3.forward * speedMax * Time.deltaTime);
    }

    public void KickBall()
    {
        Debug.Log("Kicking ball!");
        Vector3 kickDirection = ball.transform.position - transform.position;
        if (!isBallBlocking)
        {
            ballRb.AddForce(kickDirection * strength / forceDivider, ForceMode.Impulse);
        }
    }

}