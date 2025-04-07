using UnityEngine;

[CreateAssetMenu(fileName = "SpellSO", menuName = "SpellSO", order = 0)]
public class SpellSO : ScriptableObject {
    public string spellName;
    public string spellType;
    public Color color;
    public float spellCastingTime;
    public float spellDuration;
    public Sprite rune;
}