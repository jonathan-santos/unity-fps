using UnityEngine;

public class BulletAction : MonoBehaviour
{
    public int duration = 3;
    public int damage = 1;

    void Start()
    {
        Destroy(gameObject, 3);
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            var enemyLife = collision.gameObject.GetComponent<HealthAction>();
            enemyLife.ChangeHealth(- this.damage);
        }

        Destroy(gameObject);
    }
}
