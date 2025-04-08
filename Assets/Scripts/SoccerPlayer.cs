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

    private int forceDivider = 5;


    private void Start()
    {
        ball = GameObject.Find("Ball");
        ballRb = ball.GetComponent<Rigidbody>();
    }


    private void OnCollisionEnter(Collision other) {
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
        
        transform.LookAt(ball.transform);
        transform.Translate(Vector3.forward * speedMax * Time.deltaTime);
    }

    public void KickBall()
    {
        Debug.Log("Kicking ball!");
        Vector3 kickDirection =  ball.transform.position - transform.position;
        ballRb.AddForce(kickDirection * strength / forceDivider, ForceMode.Impulse);
    }

}