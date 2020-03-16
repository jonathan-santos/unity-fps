using UnityEngine;
using UnityEngine.AI;

public class DamageAction : MonoBehaviour
{
    public float damage = 1f;
    public float knockback = 0f;
    public string collisionObjectiveTag = "";
    public bool selfDestroyOnCollision = false;

    HealthAction collidedObjectHealth;
    Rigidbody collidedObjectRB;
    NavMeshAgent collidedObjectNavMesh;

    void OnTriggerEnter(Collider collision)
    {
        ApplyDamage(collision.gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        ApplyDamage(collision.gameObject);
    }

    void ApplyDamage(GameObject collidedObject)
    {
        if (collidedObject.tag == collisionObjectiveTag)
        {
            collidedObjectHealth = collidedObject.GetComponent<HealthAction>();
            collidedObjectRB = collidedObject.GetComponent<Rigidbody>();
            collidedObjectNavMesh = collidedObject.GetComponent<NavMeshAgent>();

            collidedObjectHealth?.ChangeHealth(-this.damage);

            if(collidedObjectNavMesh != null && collidedObjectNavMesh.enabled)
            {
                collidedObjectNavMesh.enabled = false;
                collidedObjectRB.isKinematic = false;
                Invoke("ReEnableCollidedObjectNavMesh", 0.25f);
            }


            collidedObjectRB?.AddForce(this.gameObject.transform.forward * knockback, ForceMode.Impulse);
        }

        if (this.selfDestroyOnCollision)
            Destroy(this.gameObject);
    }

    void ReEnableCollidedObjectNavMesh()
    {
        if(this.collidedObjectNavMesh!= null)
        {
            collidedObjectNavMesh.enabled = true;
            collidedObjectRB.isKinematic = true;
        }
    }
}
