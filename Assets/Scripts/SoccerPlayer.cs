using UnityEngine;

public class SoccerPlayer : MonoBehaviour
{
    GameObject ball;
    Rigidbody ballRb;
    Rigidbody playerRb;
    float spellCasting;
    float healthPoints = 50f;
    float strength = 50f;
    float speedMax = 350f;
    float acceleration = 50f;
    public string team;
    public string role;
    public int defenceZone; // 1 or 2


    private float avoidRadius = 15f;
    private bool isBallBlocking;
    private int forceDivider = 5;
    private float boundryX = 0;

    private Vector3 moveDirection;

    public GameObject goalObject;


    private void Start()
    {
        ball = GameObject.Find("Ball");
        ballRb = ball.GetComponent<Rigidbody>();
        playerRb = GetComponent<Rigidbody>();

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
        if (transform.position.y < 20)
        {
            Vector3 newPos = new Vector3(transform.position.x, 25, transform.position.y);
            transform.position = newPos;
        }

        float ballToGoal = Vector3.Distance(ball.transform.position, goalObject.transform.position);
        float playerToGoal = Vector3.Distance(transform.position, goalObject.transform.position);


        isBallBlocking = ballToGoal + avoidRadius > playerToGoal;

        Vector3 behindBall = ball.transform.position - goalObject.transform.position * avoidRadius;
        moveDirection = ball.transform.position;

        if (isBallBlocking)
        {
            moveDirection = behindBall + (Vector3.right * speedMax * Time.deltaTime);
        }

        if (role == "Defender")
        {
            if (ball.transform.position.x > boundryX && team == "1" || ball.transform.position.x < boundryX && team == "2")
            {
                moveDirection.x = 0;
            }

            if (ball.transform.position.z < 0 && defenceZone == 1)
            {
                moveDirection.z = 20;
            }
            if (ball.transform.position.z > 0 && defenceZone == 2)
            {
                moveDirection.z = -20;
            }
        }

        moveDirection.y = 20;
        transform.LookAt(moveDirection);
        transform.Translate(Vector3.forward * speedMax * Time.deltaTime);
    }

    public void KickBall()
    {
        Vector3 kickDirection = ball.transform.position - transform.position;
        if (!isBallBlocking)
        {
            Debug.Log("Kicking ball!");
            ballRb.AddForce(kickDirection * strength / forceDivider, ForceMode.Impulse);
        }
    }

}