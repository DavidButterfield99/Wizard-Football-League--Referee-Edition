using UnityEngine;

public class TeamController : MonoBehaviour 
{
    public SoccerPlayer soccerPlayerPrefab;
    SoccerPlayer[] players;
    public Vector3[] startingPositions = new Vector3[6]; 
    /*
    Player order goes:
    0: Goalkeeper
    1-2: Defenders
    3-5: Attackers
    */


    private void Start() {
        SpawnPlayers();        
    }
    
    void SpawnPlayers()
    {
        for (int i = 0; i < 6; i++)
        {
            Instantiate(soccerPlayerPrefab, startingPositions[i], soccerPlayerPrefab.transform.rotation);
        }


    }
    
}