using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    float addition = 0;
    float multiplication = 0;

    [SerializeField] private float Value;

    /// <summary>
    /// Returns the current stat value
    /// </summary>
    /// <returns>float value</returns>
    public float GetVal()
    {
        return Value * ((100 + multiplication) / 100) + addition;
    }

    /// <summary>
    /// Adds a number to the current value
    /// </summary>
    /// <param name="addition">How much should be added</param>
    public void AddVal(float addition)
    {
        Value += addition;
    }


    /// <summary>
    /// Subtracts a number from the current value
    /// </summary>
    /// <param name="subtraction">How much should be subtracted</param>
    public void SubVal(float subtraction)
    {
        Value -= subtraction;
    }


    /// <summary>
    /// Replaces stat value with the parameter
    /// </summary>
    /// <param name="Value">New stat value</param>
    public void SetVal(float Value)
    {
        this.Value = Value;
    }

    /// <summary>
    /// Adds a modifier to the stat. Modifiers are multiplicative ex. 1.3 will increase the stat value by 30%
    /// </summary>
    /// <param name="modifier">Modifier value</param>
    /// <param name="type">0 - addition, 1 - multiplication</param>
    public void AddMod(StatModifier modifier)
    {
        if (modifier.Value == 0) return;

        if (modifier.type == ModifierType.Additional) addition += modifier.Value;
        else multiplication += modifier.Value;
    }


    /// <summary>
    /// Removes a modifier from the stat
    /// </summary>
    /// <param name="modifier">Value of the modifier to be removed</param>
    ///<param name="type">0 - addition, 1 - multiplication</param>
    public void DelMod(StatModifier modifier)
    {
        if (modifier.Value == 0) return;

        if (modifier.type == ModifierType.Additional) addition -= modifier.Value;
        else multiplication -= modifier.Value;
    }

}
