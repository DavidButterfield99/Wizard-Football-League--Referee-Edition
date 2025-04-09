using UnityEngine;

[CreateAssetMenu(fileName = "SpellSO", menuName = "SpellSO", order = 0)]
public class SpellSO : ScriptableObject {
    public string spellName;
    public string spellType;
    public float spellDuration;
    public Color color;
    public int damage;
}