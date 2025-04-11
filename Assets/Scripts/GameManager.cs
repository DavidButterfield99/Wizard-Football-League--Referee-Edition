using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    GameObject[] team1;
    GameObject[] team2;
    public List<GameObject> allPlayers;
    List<SpellDisplay> activeSpells;
    public int activeSpellCount;
    public int maxActiveBuffs = 4;
    public int maxActiveSummons = 2;
    public int activeBuffs = 0;
    public int activeSUmmons = 0;
    static public bool gameOver = false;

    private List<SoccerPlayer> playerPoolCurrent = new List<SoccerPlayer>();
    public List<SpellSO> SpellsPoolOriginal = new List<SpellSO>();
    private List<SpellSO> SpellsPoolCurrent = new List<SpellSO>();
    private int WAIT_TIME_MIN = 2;
    private int WAIT_TIME_MAX = 5;

    void Start()
    {
        team1 = GameObject.FindGameObjectsWithTag("Team 1");
        team2 = GameObject.FindGameObjectsWithTag("Team 2");
        foreach (var player in team1)
        {
            allPlayers.Add(player);
            playerPoolCurrent.Add(player.GetComponent<SoccerPlayer>());
        }

        foreach (var player in team2)
        {
            allPlayers.Add(player);
            playerPoolCurrent.Add(player.GetComponent<SoccerPlayer>());
        }

        foreach (var spell in SpellsPoolOriginal)
        {
            SpellsPoolCurrent.Add(spell);
        }

        StartCoroutine("spellcastCoroutine");
    }

    void Update()
    {
#if UNITY_EDITOR
        gameOver = false;
#endif
        if (gameOver)
        {
            return;
        }
        activeSpells = getActiveSpells();
        activeSpellCount = activeSpells.Count;

        foreach (SpellDisplay spell in activeSpells)
        {
            try
            {
                if (spell.spellSO.spellType == "Attack")
                {
                    spell.HomeOnPlayer(spell.caster, spell.target);
                }

            }
            catch { }
        }
    }

    public IEnumerator spellcastCoroutine()
    {
        int waitTime = Random.Range(WAIT_TIME_MIN, WAIT_TIME_MAX);
        yield return new WaitForSeconds(waitTime);

        Debug.Log(playerPoolCurrent.Count);
        int playerIndex = Random.Range(0, playerPoolCurrent.Count);
        SoccerPlayer player = playerPoolCurrent[playerIndex];
        playerPoolCurrent.RemoveAt(playerIndex);

        int spellIndex = Random.Range(0, SpellsPoolCurrent.Count);
        SpellSO spell = SpellsPoolCurrent[spellIndex];
        SpellsPoolCurrent.RemoveAt(spellIndex);


        player.spellcasting.CastSpell(spell, player);
        player.isCasting = true;

        StartCoroutine(spellcastCoroutine());
    }

    List<SpellDisplay> getActiveSpells()
    {
        List<SpellDisplay> activeSpells = new List<SpellDisplay>();
        GameObject[] spells = GameObject.FindGameObjectsWithTag("Spell");
        foreach (var spell in spells)
        {
            activeSpells.Add(spell.GetComponent<SpellDisplay>());
        }

        return activeSpells;
    }
}