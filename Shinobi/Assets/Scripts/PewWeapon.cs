using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PewWeapon : Weapon
{

    private float shotTime;//exact time we can shoot at

    private void Update()
    {
        //I dont really understand why I have to get direction then angle then rotation?!?!?!??!!?!?!?!???!?!??!?!?!?!?!?!?!!?!??!?!??!
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; //This gets distance between mouse position and weapon position, input.mousePosition returns a screenpoint so we had to convert it to world points using ScreenToWorldPoint()
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//Transforms direction into angle, then we have to convert it to degrees 
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);//Convert angle into rotation
        transform.rotation = rotation;//Set rotation of actual weapon to rotation we calculated

        if (Input.GetMouseButton(0))//if player clicks left mouse button
        {
            if (Time.time >= shotTime)//if time in game is greater or equal to shotTIme(if allowed to shoot)
            {
                Instantiate(projectile, shotPoint.position, transform.rotation);//projectile is spawned at shotPoint position with the rotation the weapon currently already has
                shotTime = Time.time + timeBetweenShots;//recalculate shot time setting it equal to current time + time between shots
            }
        }
    }
}
