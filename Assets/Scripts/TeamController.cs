using UnityEngine;

public class TeamController : MonoBehaviour
{
    GameObject ball;
    public GameObject goalObject;
    public SoccerPlayer soccerPlayerPrefab;
    SoccerPlayer[] players = new SoccerPlayer[6];
    public Vector3[] startingPositions = new Vector3[6];
    /*
    Player order goes:
    0: Goalkeeper
    1-2: Defenders
    3-5: Attackers
    */

    float minDistanceFromClosestPlayer = 100f;

    private void Awake()
    {
        ball = GameObject.Find("Ball");
        SpawnPlayers();
    }

    private void Update()
    {
        SoccerPlayer closestPlayer = GetPlayerClosestToBall();
        closestPlayer.RunTowardsBall();

        foreach (var player in players)
        {
            if (player.role == "Attacker" && player != closestPlayer)
            {
                // Attacker players who are further from the ball, should keep some distance 
                // from the player closest to the ball
                float distanceFromPlayer = Vector3.Distance(player.transform.position, closestPlayer.transform.position);
                if (distanceFromPlayer > minDistanceFromClosestPlayer)
                {
                    player.RunTowardsBall();
                }
            }

            if (player.role == "Defender")
            {
                player.RunTowardsBall();
            }

            if (Random.Range(0, 1000) == 0)
            {
                player.spellcasting.CastSpell(player.spellcasting.spells[0], player);
            }   
        }
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
            player.goalObject = goalObject;

            if (i == 0)
            {
                player.role = "Goalkeeper";
            }
            else if (i == 1 || i == 2)
            {
                player.role = "Defender";
                player.defenceZone = i;
            }
            else
            {
                player.role = "Attacker";
            }

            players[i] = player;
        }
    }

}