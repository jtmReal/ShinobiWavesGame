using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public float stopDistance;

    private float attackTime;

    public Transform shotPoint;

    public GameObject enemyBullet;


    public override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (player != null)
        {
            if (Vector2.Distance(transform.position, player.position) > stopDistance)//If still too far away from player
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            if ( Time.time >= attackTime)//if close enough
            {
                attackTime = Time.time + timeBetweenAttacks;
                RangedAttack();
            }



        }
    }
    public void RangedAttack()
    {
        Vector2 direction = player.position - shotPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//Transforms direction into angle, then we have to convert it to degrees 
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);//Convert angle into rotation
        shotPoint.rotation = rotation;//Set rotation of actual weapon to rotation we calculated

        Instantiate(enemyBullet, shotPoint.position, shotPoint.rotation);
    }
}
