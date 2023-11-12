using UnityEngine;

public enum ModifierType
{
    Additional = 0,
    Multiplication = 1
}

[System.Serializable]
public struct StatModifier
{
    public float Value;
    public ModifierType type;
}
