using UnityEngine;

[CreateAssetMenu(fileName = "New Reusable Parts Counter", menuName = "Dynamic/UI Resources")]
public class ReusablePartsResource_SO : ScriptableObject
{
    public int currentGold;
    public int currentParts;

    public int allGold = 0;
    public int allParts = 0;

    private void OnEnable()
    {
        currentGold = allGold;
        currentParts = allParts;
    }
}
