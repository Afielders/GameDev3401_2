using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{

    //Shooting
    public GameObject target;
    public GameObject bullet;
    public float rate_of_fire = 0.2f;
    private float shoot_timer = 0;

    private Vector3 bullet_dir = Vector3.zero;

    public float spread = 5f; //In degrees.


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        //REMOVE, WE ARE TESTING
        bullet_dir = (target.transform.position - transform.position).normalized;
        return;

        //If there is time on the timer...
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
            bullet_dir = (target.transform.position - transform.position).normalized;
            // Pass the direction to the bullet.
            new_bullet.GetComponent<Bullet>().SetVelocity(bullet_dir);
        }
    }

    private void OnDrawGizmos()
    {

        //Draw the vetor that represents our bullet's direction
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + bullet_dir * 2);

        //Line representing the up vector
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * 2);

        //Axis we will rotate bullet direction on to add spread to the turret.
        Gizmos.color = Color.cyan;
        Vector3 axis = Vector3.Cross(bullet_dir, Vector3.up);
        Gizmos.DrawLine(transform.position, transform.position + axis * 2);

        //Spread Direction, how much spread will be applied to the bullet direction.
        Gizmos.color = Color.yellow;
        Vector3 spread_dir = Quaternion.AngleAxis(spread, axis) * bullet_dir;
        Gizmos.DrawLine(transform.position, transform.position + spread_dir * 2);

        //Show the cone of potential directions the bullet can travel in.
        Gizmos.color = Color.magenta;
        Vector3 final_dir;
        for (int i = 1; i <= 15; i++)
        {
            final_dir = Quaternion.AngleAxis(22.5f * i, bullet_dir) * spread_dir;
            Gizmos.DrawLine(transform.position, transform.position + final_dir * 2);
        }
    }

}
