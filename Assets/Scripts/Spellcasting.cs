using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spellcasting : MonoBehaviour
{
    public SpellDisplay spellPrefab;
    public SpellSO[] spells;

    public void CastSpell(SpellSO spellToCast, SoccerPlayer caster)
    {
        Debug.Log($"Casting: {spellToCast.spellName}");

        
        SpellDisplay spell = Instantiate(spellPrefab, caster.transform.position, spellPrefab.transform.rotation);
        spell.SpawnSpellCircle(spellToCast);
        spell.transform.SetParent(caster.transform);
    }

}