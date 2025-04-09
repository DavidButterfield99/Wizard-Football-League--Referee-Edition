using UnityEngine;
using UnityEngine.InputSystem;

public class Spellcasting : MonoBehaviour
{
    public SpellDisplay spellPrefab;
    public SpellSO[] spells;

    private void OnMouseDown() {
        CastSpell(spells[0], Input.mousePosition);
    }


    public void CastSpell(SpellSO spellToCast, Vector3 castLocation)
    {
        Debug.Log($"Casting: {spellToCast.spellName}");

        
        SpellDisplay spell = Instantiate(spellPrefab, castLocation, spellPrefab.transform.rotation);
        spell.SpawnSpellCircle(spellToCast);
    }

}