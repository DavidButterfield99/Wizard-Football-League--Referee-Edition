using System.Collections;
using UnityEngine;

public class SpellDisplay : MonoBehaviour {
    public Sprite[] spellCircles;
    public Sprite[] attackSprites;
    public SpellSO spellSO;
    public SoccerPlayer caster;
    public SoccerPlayer target;


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

    public void SpawnSpellEffect(SpellSO spellToCast)
    {
        rend.sprite = spellToCast.sprite;
    }

    public void HomeOnPlayer(SoccerPlayer caster, SoccerPlayer target)
    {
        if (target == null)
        {
            Debug.Log("No target found");
            return;
        }
        Vector3 moveDirection = (target.transform.position - transform.position).normalized;
        transform.Translate(moveDirection * Time.deltaTime * caster.stats["spellCasting"]);
    }

    

}