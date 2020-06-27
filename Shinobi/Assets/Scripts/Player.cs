using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;
    private Animator anim;

    private Vector2 moveAmount;

    public float  health;

    public Image[] hearts;
    public Sprite fullHeart;//Contains Red Hearts
    public Sprite emptyHeart;//Contains Black Hearts

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();//rb is set equal to RigidBody2D that is attatched to player
        anim = GetComponent<Animator>();//anim is set equal to animator that is attatched to player
    }

    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));//So basically this Vector2 is equal to specific cordinates based on wasd or arrow keys
        moveAmount = moveInput.normalized * speed; //Since the default values for moveInput are like 0 and 1 it multiples this by the speed, .normalized makes sure player will not move faster when moving diagonally
   
        if (moveInput != Vector2.zero)//If player is moving running animation is triggered
        {
            anim.SetBool("isRunning", true);
        }
        else//If player is not moving then running animation is set to false and the default animation which is idle is triggered
        {
            anim.SetBool("isRunning", false);
        }
    
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);//So it takes the current position and adds on the moveAmount basically making player move, Time.fixedDeltaTime makes it framerate independent meaning it moves at same speed regardless of the pc
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;//damages enemy
        UpdateHealthUI(health);

        if (health <= 0)//dead
        {
            Destroy(gameObject);
        }
    }


    public void ChangeWeapon(Weapon weaponToEquip)
    {
        Destroy(GameObject.FindGameObjectWithTag("Weapon"));
        Instantiate(weaponToEquip, transform.position, transform.rotation, transform);
    }

    void UpdateHealthUI(float currentHealth)//updates the UI/Hearts based on health
    {
        for(int i = 0; i < hearts.Length; i++)//This runs as long as you have health/hearts left
        {
            if ( i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    public void Heal(float healAmount)
    {
        if(health + healAmount > 5)
        {
            health = 5;
        }
        else
        {
            health += healAmount;
        }
        UpdateHealthUI(health);
    }
}
