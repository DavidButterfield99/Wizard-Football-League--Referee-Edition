using UnityEditor.AnimatedValues;
using UnityEngine;

public class TeamController : MonoBehaviour
{
    GameObject ball;
    public SoccerPlayer soccerPlayerPrefab;
    SoccerPlayer[] players = new SoccerPlayer[6];
    public Vector3[] startingPositions = new Vector3[6];
    /*
    Player order goes:
    0: Goalkeeper
    1-2: Defenders
    3-5: Attackers
    */


    private void Start()
    {
        ball = GameObject.Find("Ball");
        SpawnPlayers();
    }

    private void Update()
    {
        SoccerPlayer player = GetPlayerClosestToBall();
        player.RunTowardsBall();

    }

    SoccerPlayer GetPlayerClosestToBall()
    {
        float minDistance = float.PositiveInfinity;
        float distance;
        SoccerPlayer closestPlayer = players[0];
        foreach (var player in players)
        {
            if (player.role == "Attacker")
            {
                distance = player.GetDistanceFromBall();
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestPlayer = player;
                }
            }
        }

        return closestPlayer;
    }

    void SpawnPlayers()
    {
        for (int i = 0; i < 6; i++)
        {
            SoccerPlayer player = Instantiate(soccerPlayerPrefab, startingPositions[i], soccerPlayerPrefab.transform.rotation);

            if (i == 0)
            {
                player.role = "Goalkeeper";
            }
            else if (i == 1 || i == 2)
            {
                player.role = "Defender";
            }
            else
            {
                player.role = "Attacker";
            }

            players[i] = player;
        }
    }

}