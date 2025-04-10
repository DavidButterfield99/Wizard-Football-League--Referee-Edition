using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    GameObject[] team1;
    GameObject[] team2;
    public List<GameObject> allPlayers;
    List<SpellDisplay> activeSpells;
    public int activeSpellCount;
    public int maxActiveSpells = 4;
    static public bool gameOver = false;

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