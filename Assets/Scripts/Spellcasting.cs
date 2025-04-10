using System.Collections;
using System.Collections.Generic;
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

        StartCoroutine(pendingCast(spell));
    }

    public IEnumerator pendingCast(SpellDisplay spell)
    {
        float spellcastingDelay = 100 / spell.caster.stats["spellCasting"];

        yield return new WaitForSeconds(spellcastingDelay);
        spell.caster.isCasting = false;

        List<SoccerPlayer> targets = spell.caster.GetValidSpellTargets();
        int randomIndex = Random.Range(0, targets.Count);
        SoccerPlayer target = targets[randomIndex];
        spell.target = target;

        AttackSpell(spell);
    }

    private void AttackSpell(SpellDisplay spell)
    {
        spell.transform.SetParent(null);
        spell.SpawnSpellEffect(spell.spellSO);
    }

    private void BuffSpell()
    {

    }

    private void SummonSpell()
    {

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