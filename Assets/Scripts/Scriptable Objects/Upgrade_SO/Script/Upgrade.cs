using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Turret Upgrade", menuName = "Turret Upgrade")]
public class Upgrade : ScriptableObject
{
    // Rate of fire
    public float rateOfFire;

    // Splash
    public float splash;

    // Barrels
    public int barrels;

    // Range
    public float range;

    // Damage
    public float damage;

    // Cost
    public int cost;
}
