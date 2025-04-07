using UnityEngine;

public class SoccerPlayer : MonoBehaviour 
{
    public GameObject ball;
    public float spellCasting;
    public float healthPoints = 50f;
    public float strength = 50f;
    public float speedMax = 50f;
    public float acceleration = 50f;
    public string team;
    public string role;

    public float GetDistanceFromBall() {
        float distance = Vector3.Distance(transform.position, ball.transform.position);
        return distance;
    }

}