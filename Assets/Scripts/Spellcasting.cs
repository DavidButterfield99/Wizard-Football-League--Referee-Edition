using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spellcasting : MonoBehaviour
{
    public SpellDisplay spellPrefab;
    public SpellSO[] spells;
    GameManager gameManager;

    void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void CastSpell(SpellSO spellToCast, SoccerPlayer caster)
    {
        SpellDisplay spell = InstantiateSpell(spellToCast, caster);
        spell.SpawnSpellCircle(spellToCast);
        spell.transform.SetParent(caster.transform);

        Action spellAction = null;

        if (spell.spellSO.spellType == "Attack") spellAction = () => AttackSpell(spell);
        if (spell.spellSO.spellType == "Buff") spellAction = () => BuffSpell(spell);
        if (spell.spellSO.spellType == "Summon") spellAction = () => SummonSpell(spell);

        StartCoroutine(pendingCast(spell, spellAction));
    }

    public IEnumerator pendingCast(SpellDisplay spell, Action spellAction)
    {
        float spellcastingDelay = 100 / spell.caster.stats["spellCasting"];

        yield return new WaitForSeconds(spellcastingDelay);
        spell.caster.isCasting = false;

        List<SoccerPlayer> targets = spell.caster.GetValidSpellTargets();
        int randomIndex = UnityEngine.Random.Range(0, targets.Count);
        SoccerPlayer target = targets[randomIndex];
        spell.target = target;

        spellAction?.Invoke();
    }

    private void AttackSpell(SpellDisplay spell)
    {
        spell.transform.SetParent(null);
        spell.SpawnSpellEffect(spell.spellSO);
    }

    private void BuffSpell(SpellDisplay spell)
    {
        Dictionary<SoccerPlayer, float> distances = spell.caster.GetDistanceToPlayers();
        GameManager.activeBuffs++;

        // Filter by spell range
        distances = distances
        .Where(pair => pair.Value < spell.spellSO.range)
        .ToDictionary(pair => pair.Key, pair => pair.Value);

        List<SoccerPlayer> enemiesInRange = GetEnemiesInRange(spell.caster, distances);
        List<SoccerPlayer> teammatesInRange = GetTeammatesInRange(spell.caster, distances);
        if (enemiesInRange.Count != 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, enemiesInRange.Count);
            spell.target = enemiesInRange[randomIndex];
        }
        else if (teammatesInRange.Count != 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, teammatesInRange.Count);
            spell.target = teammatesInRange[randomIndex];
        }
        else 
        {
            // No one in range, target self.
            spell.target = spell.caster;
        }

        Debug.Log($"Buff spell {spell.spellSO.spellName}");
    }

    private void SummonSpell(SpellDisplay spell)
    {
        GameManager.activeSummons++;
        Debug.Log($"Summon spell {spell.spellSO.spellName}");
    }


    private IEnumerator SpellTimeLimitCoroutine(SpellDisplay spell)
    {
        yield return new WaitForSeconds(spell.spellSO.spellDuration);
        Destroy(spell.gameObject);
    }

    private List<SoccerPlayer> GetEnemiesInRange(SoccerPlayer caster, Dictionary<SoccerPlayer, float> distances)
    {
        List<SoccerPlayer> enemiesInRange = distances
        .Keys
        .Where(player => player.team != caster.team)
        .ToList();

        return enemiesInRange;
    }

    private List<SoccerPlayer> GetTeammatesInRange(SoccerPlayer caster, Dictionary<SoccerPlayer, float> distances)
    {
        List<SoccerPlayer> teammatesInRange = distances
        .Keys
        .Where(player => player.team == caster.team)
        .ToList();

        return teammatesInRange;
    }

    SpellDisplay InstantiateSpell(SpellSO spellToCast, SoccerPlayer caster, SoccerPlayer target = null)
    {
        SpellDisplay spell = Instantiate(spellPrefab, caster.transform.position, spellPrefab.transform.rotation);
        spell.spellSO = spellToCast;
        spell.caster = caster;
        spell.target = target;

        return spell;
    }
}