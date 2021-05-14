using UnityEngine;

[CreateAssetMenu(fileName = "New Selected Object holder",menuName ="Game Object Holder")]
public class SelectedTurret : ScriptableObject
{
    public GameObject selected;

    public GameObject Selected
    {
        set { selected = value; }
        get { return selected; }
    }

    private void OnDisable()
    {
        this.selected = null;
    }
}
