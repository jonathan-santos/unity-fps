using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAction : MonoBehaviour
{
    public int damage = 1;
    public float attackKnockback = 5f;

    NavMeshAgent agent;
    GameObject player;
    Rigidbody rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        InvokeRepeating("GoToPlayer", 0, 1);
    }

    void GoToPlayer() {
        agent.destination = player.transform.position;
    }

    void LateUpdate()
    {
        LookAtPlayer();
    }

    void LookAtPlayer()
    {
        transform.LookAt(player.transform.position, Vector3.up);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "player")
        {
            var playerHealth = collision.gameObject.GetComponent<HealthAction>();
            playerHealth.ChangeHealth(-this.damage);
            LookAtPlayer();

            //rb.AddForce(Vector3.back * attackKnockback, ForceMode.Impulse);
        }
    }
}