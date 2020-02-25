using UnityEngine;
using UnityEngine.AI;

public class EnemyAction : MonoBehaviour
{
    public int damage = 1;

    NavMeshAgent agent;
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        agent = GetComponent<NavMeshAgent>();

        InvokeRepeating("GoToPlayer", 0, 1);
    }

    void GoToPlayer() {
        agent.destination = player.transform.position;
        transform.LookAt(player.transform.position, Vector3.up);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "player")
        {
            var playerHealth = collision.gameObject.GetComponent<HealthAction>();
            playerHealth.ChangeHealth(-this.damage);
        }
    }
}