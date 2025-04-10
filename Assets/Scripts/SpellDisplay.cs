using System.Collections;
using UnityEngine;

public class SpellDisplay : MonoBehaviour
{
    public Sprite[] spellCircles;
    public Sprite[] attackSprites;
    public SpellSO spellSO;
    public SoccerPlayer caster;
    public SoccerPlayer target;
    public bool spellActive = false;


    private SpriteRenderer rend;

    private void Awake()
    {
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
        spellActive = true;
    }

    public void HomeOnPlayer(SoccerPlayer caster, SoccerPlayer target)
    {
        if (target == null)
        {
            return;
        }
        // Vector3 moveDirection = (target.transform.position - transform.position).normalized;
        // transform.Translate(moveDirection * Time.deltaTime * caster.stats["spellCasting"]);
        // Vector3 moveDirection = target.transform.position;
        transform.LookAt(target.transform);
        transform.Translate(Vector3.forward * caster.stats["spellCasting"] * Time.deltaTime);

    }

    private void OnCollisionEnter(Collision other)
    {
        SoccerPlayer player;
        try
        {
            player = other.gameObject.GetComponent<SoccerPlayer>();
            if (player == target)
            {
                Debug.Log($"Hit target, hp reduced from {target.stats["healthPoints"]} to {target.stats["healthPoints"] - spellSO.damage}");
                target.stats["healthPoints"] -= spellSO.damage;
                Destroy(this.gameObject);
            }
        }
        catch 
        { }
        if (other.gameObject.CompareTag("Player") && spellActive)
        {
            GameManager.gameOver = true;
        }
    }
}