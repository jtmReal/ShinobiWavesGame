using System.Collections;
using UnityEngine;

public class Level1Enemy : Enemy //Level1Enemy is Inheriting from Enemy
{
    public float stopDistance;

    private float attackTime;

    public float attackSpeed;

    private void Update()
    {
        if(player != null)
        {
            if(Vector2.Distance(transform.position, player.position) > stopDistance)//If still too far away from player
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else//if near enough
            {
                if(Time.time >= attackTime)//Basically lets me decide the cooldown between each attacks
                {
                    StartCoroutine(Attack());//attack
                    attackTime = Time.time + timeBetweenAttacks;
                }
            }
        }
    }

    IEnumerator Attack()
    {
        player.GetComponent<Player>().TakeDamage(damage);

        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.position;

        float percent = 0;//0 meaning started animation, 1 meaning completed
        while(percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;//Every frame adds a little bit to percent variable
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;//enable us to perform animation of going towards target position then goes back to original position based on percent variable
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            yield return null;
        }
    }
}
