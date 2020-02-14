using UnityEngine;
using UnityEngine.AI;

public class EnemyAction : MonoBehaviour
{
    public int life = 3;

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

    public void TakeDamage(int damage)
    {
        this.life -= damage;
        if (this.life <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}