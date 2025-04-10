using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoccerPlayer : MonoBehaviour
{
    GameObject ball;
    Rigidbody ballRb;
    Rigidbody playerRb;
    public Dictionary<string, float> stats = new Dictionary<string, float>(); 
    public string team;
    public string role;
    public int defenceZone; // 1 or 2


    private float avoidRadius = 15f;
    private bool isBallBlocking;
    private int forceDivider = 5;
    private float boundryX = 0;

    private Vector3 moveDirection;

    public GameObject goalObject;
    public Spellcasting spellcasting;

    private GameManager gameManager;



    private void Awake()
    {
        ball = GameObject.Find("Ball");
        ballRb = ball.GetComponent<Rigidbody>();
        playerRb = GetComponent<Rigidbody>();
        spellcasting = GetComponent<Spellcasting>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        GenerateStats(90);
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
            moveDirection = behindBall + (Vector3.right * stats["speedMax"] * Time.deltaTime);
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
        transform.Translate(Vector3.forward * stats["speedMax"] * Time.deltaTime);
    }

    public void KickBall()
    {
        Vector3 kickDirection = ball.transform.position - transform.position;
        if (!isBallBlocking)
        {
            ballRb.AddForce(kickDirection * stats["strength"] / forceDivider, ForceMode.Impulse);
        }
    }

    public void GenerateStats(int maxVal)
    {
        stats.Add("spellCasting", 0);
        stats.Add("healthPoints", 0);
        stats.Add("strength", 0);
        stats.Add("speedMax", 0);
        stats.Add("acceleration", 0);
        stats.Add("Defense", 0);
        
        for (int i = 0; i < maxVal * stats.Count; i++)
        {
            
            var element = stats.ElementAt(Random.Range(0,stats.Count));
            if (stats[element.Key] == 100)
            {
                continue;
            }
            stats[element.Key]++;
        }
    }

    public List<SoccerPlayer> GetValidSpellTargets()
    {
        List<SoccerPlayer> targets = new List<SoccerPlayer>();

        for (int i = 0; i < gameManager.allPlayers.Count; i++)
        {
            SoccerPlayer player = gameManager.allPlayers[i].GetComponent<SoccerPlayer>();
            if (player.team != team)
            {
                targets.Add(player);
            }
        
        }
        
        return targets;
    }

}