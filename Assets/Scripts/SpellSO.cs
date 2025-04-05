using UnityEngine;

[CreateAssetMenu(fileName = "SpellSO", menuName = "SpellSO", order = 0)]
public class SpellSO : ScriptableObject {
    string spellName;
    string spellType;
    Color color;
    float spellCastingTime;
    float spellDuration;
    Sprite rune;
}