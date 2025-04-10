using UnityEngine;

public class SpellDisplay : MonoBehaviour {
    public Sprite[] spellCircles;

    private SpriteRenderer rend;
    
    private void Awake() {
        rend = GetComponent<SpriteRenderer>();
    }




    public void SpawnSpellCircle(SpellSO spellToCast)
    {
        if (spellToCast.spellType == "Buff")
        {
            rend.sprite = spellCircles[0];
        }
        else if (spellToCast.spellType == "Debuff")
        {
            rend.sprite = spellCircles[1];
        }
        else if (spellToCast.spellType == "Attack")
        {
            rend.sprite = spellCircles[2];
        }
        else
        {
            // Must be Summon spell type
            rend.sprite = spellCircles[3];
        }

        rend.color = spellToCast.color;
        
            
    }
}