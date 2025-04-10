using UnityEngine;

[CreateAssetMenu(fileName = "SpellSO", menuName = "SpellSO", order = 0)]
public class SpellSO : ScriptableObject {
    public string spellName;
    public string spellType;
    public float spellDuration;
    public int damage;
    public Color color;
    public Sprite sprite;
}