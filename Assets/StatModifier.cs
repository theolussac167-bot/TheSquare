using System;

[Serializable]
public class StatModifier
{
    public enum StatType { Damage, MaxHealth, MaxMana, Speed, ManaRegen, VelocidadeTiro, Coins, Orbs }
    public enum ModifierType { Flat, Multiplier }

    public StatType stat;
    public ModifierType modifier;
    public float value;
}
