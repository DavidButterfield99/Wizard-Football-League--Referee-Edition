using System.Collections.Generic;
using UnityEngine;

public class SpellDisplay : MonoBehaviour
{
    public Sprite[] spellCircles;
    public Sprite[] attackSprites;
    public SpellSO spellSO;
    public SoccerPlayer caster;
    public SoccerPlayer target;
    public bool spellActive = false;
    public Dictionary<string, int> spellTypeToIndex = new Dictionary<string, int>();

    private SpriteRenderer rend;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        spellTypeToIndex.Add("Buff", 0);
        spellTypeToIndex.Add("Debuff", 1);
        spellTypeToIndex.Add("Attack", 2);
        spellTypeToIndex.Add("Summon", 3);
    }

    public void SpawnSpellCircle(SpellSO spellToCast)
    {
        if (spellTypeToIndex.TryGetValue(spellToCast.spellType, out int index))
        {
            rend.sprite = spellCircles[index];
        }

        rend.color = spellToCast.color;
    }

    public void SpawnSpellEffect(SpellSO spellToCast)
    {
        rend.sprite = spellToCast.sprite;
        rend.color = Color.white;
        spellActive = true;
    }

    public void HomeOnPlayer(SoccerPlayer caster, SoccerPlayer target)
    {
        if (target == null)
        {
            return;
        }

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