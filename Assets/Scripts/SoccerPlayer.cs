using UnityEngine;

public class SoccerPlayer : MonoBehaviour
{
    GameObject ball;
    public float spellCasting;
    public float healthPoints = 50f;
    public float strength = 50f;
    public float speedMax = 50f;
    public float acceleration = 50f;
    public string team;
    public string role;

    private void Start()
    {
        ball = GameObject.Find("Ball");
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

}