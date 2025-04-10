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
    private List<SpellSO> SpellsPoolOriginal = new List<SpellSO>();
    private List<SpellSO> SpellsPoolCurrent = new List<SpellSO>();

    void Start()
    {
        team1 = GameObject.FindGameObjectsWithTag("Team 1");
        team2 = GameObject.FindGameObjectsWithTag("Team 2");
        foreach (var player in team1)
        {
            allPlayers.Add(player);
        }

        foreach (var player in team2)
        {
            allPlayers.Add(player);
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
        int waitTime = Random.Range(10, 20);
        yield return new WaitForSeconds(waitTime);

        Debug.Log(playerPoolCurrent.Count);
        int selection = Random.Range(0, playerPoolCurrent.Count);
        SoccerPlayer player = playerPoolCurrent[selection];
        playerPoolCurrent.RemoveAt(selection);


        player.spellcasting.CastSpell(player.spellcasting.spells[0], player);
        player.isCasting = true;
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