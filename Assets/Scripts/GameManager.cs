using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public List<GameObject> allPlayers;
    List<SpellDisplay> activeSpells;
    public int activeSpellCount;
    public int maxActiveBuffs = 4;
    public int maxActiveSummons = 2;
    public int activeBuffs = 0;
    public int activeSUmmons = 0;
    static public bool gameOver = false;

    private List<SoccerPlayer> playerPoolOriginal = new List<SoccerPlayer>();
    private List<SoccerPlayer> playerPoolCurrent = new List<SoccerPlayer>();
    public List<SpellSO> SpellsPoolOriginal = new List<SpellSO>();
    private List<SpellSO> SpellsPoolCurrent = new List<SpellSO>();
    private int WAIT_TIME_MIN = 2;
    private int WAIT_TIME_MAX = 5;

    void Start()
    {
        allPlayers = GameObject.FindGameObjectsWithTag("SoccerPlayerTag").ToList();
        foreach (var player in allPlayers)
        {
            playerPoolCurrent.Add(player.GetComponent<SoccerPlayer>());
        }

        SpellsPoolCurrent.AddRange(SpellsPoolOriginal);

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

        SpellSO spell = PoolHandler(ref SpellsPoolCurrent, SpellsPoolOriginal);
        SoccerPlayer player = PoolHandler(ref playerPoolCurrent, playerPoolCurrent);

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

    private T PoolHandler<T>(ref List<T> pool, List<T> poolRefill)
    {
        if (pool.Count == 0)
        {
            pool.AddRange(poolRefill);
        }

        int itemIndex = Random.Range(0, pool.Count);
        T item = pool[itemIndex];
        pool.RemoveAt(itemIndex);

        return item;
    }
}