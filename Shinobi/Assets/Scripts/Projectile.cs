using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;

    public int damage;

    public GameObject explosion;

    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    private void Update()
    {
        //transform.Translate(Vector2.up * speed * Time.deltaTime);//So basically moves projectile 
    }

    void DestroyProjectile()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)//detects collissions
    {
        if(collision.tag == "Enemy")//If the tag of thing collided with was an enemy
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);//Damages the Enenemy
            DestroyProjectile();
        }
    }
}
