using UnityEngine;

public class DamageAction : MonoBehaviour
{
    public float damage = 1;
    public string collisionObjectiveTag = "";
    public bool destroyOnCollision = false;

    HealthAction collidedObjectHealth;

    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == collisionObjectiveTag)
        {
            collidedObjectHealth = collision.gameObject.GetComponent<HealthAction>();
            collidedObjectHealth.ChangeHealth(-this.damage);
        }

        if(this.destroyOnCollision)
            Destroy(gameObject);
    }
}
