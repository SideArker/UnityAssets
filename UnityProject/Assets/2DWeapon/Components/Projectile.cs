using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] float damage;
    [SerializeField] float moveSpeed;
    [SerializeField] int projectileRange;
    [SerializeField] int knockbackStrength;
    [SerializeField] int pierceCount;
    Vector2 startPosition;
    public GameObject bulletOrigin { get; private set; }


    #region Projectile Movement

    void CheckDistance()
    {
        if (projectileRange <= 0) return;
        if (Vector2.Distance(transform.position, startPosition) > projectileRange) Destroy(gameObject);
    }

    void MoveProjectile()
    {
        transform.Translate(moveSpeed * Time.deltaTime * Vector2.up);
        CheckDistance();
    }
    #endregion

    #region Collision

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (bulletOrigin == collision.gameObject) return;

        if (collision.GetComponent<Projectile>())
        {
         return;
        }

        if (collision.GetComponent<EntityData>())
        {
            collision.GetComponent<Health>().TakeDamage(damage);

    if (knockbackStrength > 0)
            {
                Vector2 direction = (collision.transform.position - transform.position).normalized;
                collision.GetComponent<Rigidbody2D>().AddForce(direction * knockbackStrength, ForceMode2D.Impulse);
            }

            if (pierceCount > 0)
            {
                pierceCount--;
            }
        }



        if(!collision.GetComponent<EntityData>())
        {
            Destroy(gameObject);
        }
        else if(pierceCount < 0) Destroy(gameObject);


    }
    #endregion

    #region Value Updating

    /// <summary>
    /// Updates all the stats in the projectile to match the Entity
    /// </summary>
    /// <param name="damage">Damage that the projectile will inflict</param>
    /// <param name="moveSpeed">Speed that the projectile travels with</param>
    /// <param name="projectileRange">Range of the projectile</param>
    /// <param name="knockback">How much knockback the entity that got hit should take</param>
    public void UpdateStats(float damage, float moveSpeed, int projectileRange, int knockback)
    {
        this.damage = damage;
        this.moveSpeed = moveSpeed;
        this.projectileRange = projectileRange;
        knockbackStrength = knockback;
    }


    /// <summary>
    /// Changes bullet origin. 
    /// </summary>
    /// <param name="origin">Entity that spawned the bullet</param>
    public void SetOrigin(GameObject origin)
    {
        bulletOrigin = origin;
    }

    public void SetPierce(int count)
    {
        pierceCount = count;
    }

    #endregion

    #region Start & Update
    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        MoveProjectile();
    }

    #endregion
}
