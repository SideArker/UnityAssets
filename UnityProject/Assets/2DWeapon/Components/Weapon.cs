using System.Collections;
using UnityEngine;
using NaughtyAttributes;

#if UNITY_EDITOR
using UnityEditor;
#endif

public enum ShootingStyle
{
    Normal,
    Burst
}

public class Weapon : MonoBehaviour
{

       //   THIS CLASS IS MEANT TO BE INHERITED    \\
      //    YOU HAVE TO IMPLEMENT ENDLAG YOURSELF   \\
     //  YOU HAVE TO IMPLEMENT CAMERA SHAKE YOURSELF \\

    #region Variables
    [Header("Main")]
    [SerializeField] GameObject bullet;
    [SerializeField] ShootingStyle ShootingStyle;
    [SerializeField] float damage = 20;
    [SerializeField] protected float fireRate = 30;
    [SerializeField] float bulletSpeed = 10;
    [SerializeField] int bulletRange = 50;

    [Header("Burst")]
    [SerializeField] int burstCount = 0;
    [SerializeField][Range(0, 360)] int spreadRange = 1;
    [SerializeField] float timeBetweenBurstShots;
    [SerializeField] float burstRestTime;

    [Header("Camera Shake")]
    [SerializeField] bool cameraShake;
    [SerializeField] float shakeIntensity;
    [SerializeField] float shakeTime;

    [Header("Other")]
    [SerializeField] bool enableKnockback;
    [SerializeField] float endLag;
    [SerializeField] int knockback;
    [SerializeField] int selfKnockback;
    [SerializeField] bool enablePierce;
    [SerializeField] int pierceCount;

    [HideInInspector] public bool onCooldown { get; private set; }
    public bool canShoot { get; private set; } = true;
    bool appliedKnockback;
    #endregion

    #region Attacks

    IEnumerator BurstAttack()
    {
        onCooldown = true;

        for (int i = 0; i < burstCount; i++)
        {
            Quaternion rotation;
            if (spreadRange > 1)
            {
                float aimingAngle = transform.eulerAngles.z;
                float angleStep = spreadRange / burstCount;
                float centeringOffset = (spreadRange / 2) - (angleStep / 2);
                float currentBulletAngle = angleStep * i;
                 rotation = Quaternion.Euler(new Vector3(0, 0, aimingAngle + currentBulletAngle - centeringOffset));
            }
            else  rotation = transform.rotation;


            CreateBullet(transform.position, rotation);
            
            if(timeBetweenBurstShots != 0 ) yield return new WaitForSeconds(timeBetweenBurstShots);

        }

        yield return new WaitForSeconds(burstRestTime);
        onCooldown = false;

        if(endLag > 0)
        {
            // IMPLEMENT END LAG HERE
        }
    }
    [Button]
    public void Attack()
    {
        if (!canShoot) return;
        if (onCooldown) return;

        appliedKnockback = false;

        if (ShootingStyle == ShootingStyle.Burst)
        {
            StartCoroutine(BurstAttack());
            return;
        }

        CreateBullet(transform.position, transform.rotation);
        StartCoroutine(FireRateCooldown());
    }
    #endregion

    #region Other

    public float GetEndLagTime()
    {
        return endLag;
    }
    public void ChangeCanShootState(bool state)
    {
        canShoot = state;
    }
    IEnumerator FireRateCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(60 / fireRate);
        onCooldown = false;
    }

    void CreateBullet(Vector2 position, Quaternion rotation)
    {
        GameObject newBullet = Instantiate(bullet, position, rotation);
        newBullet.transform.position += transform.up * 0.7f;
        Projectile bulletStats = newBullet.GetComponent<Projectile>();

        bulletStats.UpdateStats(damage, bulletSpeed, bulletRange, knockback);
        if (pierceCount > 0) bulletStats.SetPierce(pierceCount);
        bulletStats.SetOrigin(transform.gameObject);

        if(selfKnockback > 0 && !appliedKnockback)
        {
            appliedKnockback = true;
            Vector2 direction = -(newBullet.transform.position - transform.position).normalized;
            transform.GetComponent<Rigidbody2D>().AddForce(direction * selfKnockback, ForceMode2D.Impulse);
        }
    }
    #endregion


}

