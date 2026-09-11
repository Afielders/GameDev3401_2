using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{

    //Shooting
    public GameObject target;
    public GameObject bullet;
    public float rate_of_fire = 0.2f;

    private float shoot_timer = 0;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        //If there is tme on the timer...
        if (shoot_timer > 0)
        {
            //Countdown but subtracting delta time.
            shoot_timer -= Time.deltaTime;
        }
        else // Otherwise...
        {
            //Spawn Bullets
            //Rest the timer
            shoot_timer = rate_of_fire;
            //Create new bullet
            GameObject new_bullet = Instantiate(bullet);
            //Move the bullet to the position of the turret.
            new_bullet.transform.position = transform.position;


            //Find the direction the bullet will travel in.
            Vector3 bullet_dir = (target.transform.position - transform.position).normalized;
            // Pass the direction to the bullet.
            new_bullet.GetComponent<Bullet>().SetVelocity(bullet_dir);
        }
    }
}
