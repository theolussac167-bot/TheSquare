using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrades/Upgrade Data")]
public class UpgradeData : ScriptableObject
{
    [Header("Identidade")]
    public string upgradeName;
    [TextArea] public string description;

    [Header("Custo")]
    public int costCoins;
    public int costOrbs;
    public int maxUpgrades = 1;

    [Header("Modificadores de Stat")]
    public List<StatModifier> statModifiers;
}
