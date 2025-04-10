using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellcasting : MonoBehaviour
{
    public SpellDisplay spellPrefab;
    public SpellSO[] spells;

    public void CastSpell(SpellSO spellToCast, SoccerPlayer caster)
    {

        
        SpellDisplay spell = InstantiateSpell(spellToCast, caster);
        spell.SpawnSpellCircle(spellToCast);
        spell.transform.SetParent(caster.transform);

        StartCoroutine(pendingCast(spellToCast, caster));
        
    }

    public IEnumerator pendingCast(SpellSO spellToCast, SoccerPlayer caster)
    {
        float spellcastingDelay = 100 / caster.stats["spellCasting"];

        List<SoccerPlayer> targets = caster.GetValidSpellTargets();
        int randomIndex = Random.Range(0,targets.Count);
        SoccerPlayer target = targets[randomIndex];

        Debug.Log($"target : {target}");

        AttackSpell(spellToCast, caster, target);
        yield return new WaitForSeconds(spellcastingDelay);
    }

    private void AttackSpell(SpellSO spellToCast, SoccerPlayer caster, SoccerPlayer target)
    {
        SpellDisplay spell = InstantiateSpell(spellToCast, caster, target);
        spell.SpawnSpellEffect(spellToCast);
    }

    private void BuffSpell()
    {

    }

    private void SummonSpell()
    {

    }

    SpellDisplay InstantiateSpell(SpellSO spellToCast, SoccerPlayer caster, SoccerPlayer target=null)
    {
        SpellDisplay spell = Instantiate(spellPrefab, caster.transform.position, spellPrefab.transform.rotation);
        spell.spellSO = spellToCast;
        spell.caster = caster;
        spell.target = target;

        return spell;       
    }

}