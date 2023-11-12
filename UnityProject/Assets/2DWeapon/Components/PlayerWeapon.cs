using System.Collections;
using UnityEngine;
using NaughtyAttributes.Test;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayerWeapon : Weapon 
{

    public static PlayerWeapon Instance { get; private set; }
    public bool canAttack { get; private set; } = true;


    #region Attacking
    public void PlayerAttack()
    {
        if (!onCooldown) Attack();


        // IMPLEMENT END LAG HERE
        //float endLagTime = GetEndLagTime();
        //if (endLagTime > 0) StartCoroutine(Player.Instance.EndLag(endLagTime));
    }

    #endregion

    #region Update & Awake
    private void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        if (Input.GetMouseButton(0) && !onCooldown && canAttack) { PlayerAttack(); }
    }
    #endregion

    #region Script control
    public void CanAttack(bool state)
    {
        canAttack = state;
    }
    #endregion
}


#region Editor

#if UNITY_EDITOR
[CustomEditor(typeof(PlayerWeapon))]
public class PlayerWeaponEditor : WeaponEditor
{
}
#endif

#endregion
