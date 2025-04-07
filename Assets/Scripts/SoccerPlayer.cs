using UnityEngine;

public class SoccerPlayer : MonoBehaviour 
{
    public GameObject ball;
    float spellCasting;
    float healthPoints;
    float strength;
    float speedMax;
    float acceleration;
    string team;
    string role;

    public float GetDistanceFromBall() {
        float distance = Vector3.Distance(transform.position, ball.transform.position);
        return distance;
    }

}