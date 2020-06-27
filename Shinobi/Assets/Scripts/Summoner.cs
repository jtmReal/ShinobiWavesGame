using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : Enemy
{

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private Vector2 targetPosition;
    private Animator anim;

    public float timeBetweenSummons;
    private float summonTime;

    public Enemy enemyToSummon;

    public float attackSpeed;
    public float stopDistance;
    private float attackTime;

    public override void Start()//Basically lets me use both start functions from this class and enemy class too
    {
        base.Start();//Calls start function form enemy script
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        targetPosition = new Vector2(randomX, randomY);
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        if(player != null)//if still alive
        {
            if(Vector2.Distance(transform.position, targetPosition) > .5f)//if distance between current position and target position is greater than .5 this means STILL too far away
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);//Moves towards random position on map
                anim.SetBool("isRunning", true);//Starts running
            }
            else//reached target position
            {
                anim.SetBool("isRunning", false);

                if(Time.time >= summonTime)//decides when summons happen
                {
                    summonTime = Time.time + timeBetweenSummons;
                    anim.SetTrigger("summon");
                }
            }
            if (Vector2.Distance(transform.position, player.position) < stopDistance)//basically if player is in range of this Enemy
            {
                if (Time.time >= attackTime)//Basically lets me decide the cooldown between each attacks
                {
                    StartCoroutine(Attack());//attack
                    attackTime = Time.time + timeBetweenAttacks;
                }
            }
        }
    }

    public void Summon()//made it where this is called on frame 30 of the Summon animation
    {
        if (player != null)//if alive
        {
            Instantiate(enemyToSummon, transform.position, transform.rotation);
        }
    }

    IEnumerator Attack()
    {
        player.GetComponent<Player>().TakeDamage(damage);

        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.position;

        float percent = 0;//0 meaning started animation, 1 meaning completed
        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;//Every frame adds a little bit to percent variable
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;//enable us to perform animation of going towards target position then goes back to original position based on percent variable
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            yield return null;
        }
    }
}
