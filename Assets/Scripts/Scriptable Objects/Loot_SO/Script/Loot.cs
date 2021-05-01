using UnityEngine;

[CreateAssetMenu(fileName = "New Loot Set", menuName = "Sets/Loot")]
public class Loot : RuntimeSet<GameObject>
{
    private void OnEnable()
    {
        Items.Clear();
        Items.TrimExcess();
    }
}
