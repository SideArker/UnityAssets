using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    EntityData Data;
    public float shieldAmount { get; private set; }

    [Header("Passive Regen")]
    [SerializeField] bool canHeal;
    [SerializeField] float healIncrement = 0.01f;
    [SerializeField] float waitPerTick = 1f;
    bool isHealing;

    [Header("Other")]
    [SerializeField] bool showBossBar;

    public UnityEvent onDeath;

    #region Health Functions

    public void HealHealth(float health)
    {
        Data.Health.AddVal(health);

        float MaxHp = Data.MaxHealth.GetVal();

        if (Data.Health.GetVal() > MaxHp) Data.Health.SetVal(MaxHp);
    }

    public void IncreaseMaxHealth(float amount)
    {
        Data.MaxHealth.AddVal(amount);
    }
    #endregion

    #region Passive Healing
    IEnumerator HealTick()
    {
        isHealing = true;
        yield return new WaitForSeconds(0.5f); // Wait for a bit before starting ensuring entity isn't dead

        while (Data.Health.GetVal() < Data.MaxHealth.GetVal() && canHeal)
        {
            float HealValue = Data.MaxHealth.GetVal() * healIncrement;
            Debug.Log("Healed " + HealValue + " during current heal tick.");

            HealHealth(HealValue);
            yield return new WaitForSeconds(waitPerTick);
        }
        isHealing = false;
    }

    public void SwitchHealing()
    {
        canHeal = !canHeal;

        if (canHeal) StartCoroutine(HealTick());
    }

    IEnumerator DisableHealingForSeconds(float waitTime)
    {
        if (!canHeal) yield break;

        SwitchHealing();
        yield return new WaitForSeconds(waitTime);
        SwitchHealing();

    }

    /// <summary>
    /// NOTE: INCREMENT IS PERCENTAGE OF PLAYER MAX HEALTH
    /// </summary>
    /// <param name="value">Value to add</param>
    public void ChangeIncrement(float value)
    {
        healIncrement += value;
    }

    /// <summary>
    /// NOTE: WAIT TIME IS IN SECONDS
    /// </summary>
    /// <param name="value">Value to add</param>
    public void ChangeWaitTime(float value)
    {
        waitPerTick += value;
    }

    #endregion

    #region Damage Functions

    public void TakeTrueDamage(float damage)
    {
        if (damage < 0) damage = -damage;

        Data.Health.SubVal(damage);

        if (Data.Health.GetVal() <= 0) KillEntity();


    }
    public void TakeDamage(float amount)
    {
        //print("Roblox uffff");
        StartCoroutine(DisableHealingForSeconds(2f));
        //Disable passive heal for 2 seconds

        if (amount > shieldAmount)
        {
            float healthDeplete = shieldAmount - amount;

            TakeTrueDamage(healthDeplete);

            shieldAmount = 0;
        }
        else shieldAmount -= amount;
    }

    void KillEntity()
    {
        // Send a message that character is dead 
        Debug.Log(transform.name + " is dead");
        onDeath.Invoke();
        Destroy(transform.gameObject);
    }

    #endregion

    #region Other
    public void GiveShield(float amount)
    {
        shieldAmount += amount;
    }

    #endregion

    #region START & UPDATE
    private void Start()
    {
        Data = transform.GetComponent<EntityData>();
    }

    private void Update()
    {
        if (
            Data.Health.GetVal() < Data.MaxHealth.GetVal() &&
            canHeal && !isHealing) { StartCoroutine(HealTick()); }

        if (Data.Health.GetVal() <= 0) KillEntity();

    }
    #endregion
}