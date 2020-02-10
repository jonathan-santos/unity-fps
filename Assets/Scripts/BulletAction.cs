using UnityEngine;

public class BulletAction : MonoBehaviour
{
    public int duration = 3;
    public int damage = 1;

    void Start()
    {
        Destroy(gameObject, 3);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            var enemy = collision.gameObject.GetComponent<EnemyAction>();
            enemy.TakeDamage(this.damage);
        }

        Destroy(gameObject);
    }
}
