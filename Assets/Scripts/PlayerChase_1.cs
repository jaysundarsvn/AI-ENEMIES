using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerChase_1 : MonoBehaviour
{
    Transform player;
    public LayerMask playerLayer;
    NavMeshAgent agent;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Chase();
    }

    void Chase()
    {
        agent.SetDestination(player.position);
    }
}
