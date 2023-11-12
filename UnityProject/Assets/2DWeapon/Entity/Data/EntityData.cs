using NaughtyAttributes;
using UnityEngine;

public class EntityData : MonoBehaviour
{
    public int Coins { get; private set; } = 0;

    [Header("Health")]
    public Stat Health;
    public Stat MaxHealth;

    [Header("Movement")]
    public Stat Speed;
    public Stat SpeedLerp;

    [Header("Weapon")]
    public Stat FireRateMultiplier;
    public Stat DamageMultiplier;
    public Stat BulletSpeedMultiplier;

    private void Awake()
    {
        Health.SetVal(MaxHealth.GetVal());
    }
    public void AddCoins(int count)
    {
        Coins += count;
        if(Coins < 0) Coins = 0;
    }
}