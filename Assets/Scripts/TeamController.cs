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
        foreach (var player in players)
        {
            player.transform.LookAt(ball.transform);
            player.transform.Translate(Vector3.forward * player.speedMax * Time.deltaTime);
        }
    }


    void SpawnPlayers()
    {
        for (int i = 0; i < 6; i++)
        {
            SoccerPlayer player = Instantiate(soccerPlayerPrefab, startingPositions[i], soccerPlayerPrefab.transform.rotation);
            players[i] = player;
        }
    }

}