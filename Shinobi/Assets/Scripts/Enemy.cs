using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;

    [HideInInspector]
    public Transform player;

    public float speed;

    public float timeBetweenAttacks;

    public float damage;

    public float pickupChance;
    public GameObject[] pickups;

    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;//damages enemy

        if (health <= 0)//dead
        {
            float randomNumber = Random.Range(0, 101);
            if (randomNumber < pickupChance)
            {
                GameObject randomPickup = pickups[Random.Range(0, pickups.Length)];//Picks random pickup inside array of pickups if number is less than pickUpChance that we made
                Instantiate(randomPickup, transform.position, transform.rotation);//Spawns the pickup at where enemy died
            }

            Destroy(gameObject);
        }
    }
}
