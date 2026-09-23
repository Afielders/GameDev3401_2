using Mono.Cecil.Cil;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{

    //Shooting
    public GameObject target;
    public GameObject bullet;
    public float rate_of_fire = 0.2f;
    private float shoot_timer = 0;

    private Vector3 dir_to_target = Vector3.zero;

    public float spread = 5f; //In degrees.

    public float view_angle = 25; //In degrees.

    private float dot_needed_to_see;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Find the dot product needed for the turret to see its target.
        dot_needed_to_see = Mathf.Cos(view_angle / 2 * Mathf.Deg2Rad);
    }


    // Update is called once per frame
    void Update()
    {
        //If there is time on the timer...
        if (shoot_timer > 0)
        {
            //Countdown but subtracting delta time.
            shoot_timer -= Time.deltaTime;
        }
        else // Otherwise...
        {
            //Find the direction the bullet will travel in.
            dir_to_target = (target.transform.position - transform.position).normalized;

            if (TargetInView())
            {
                //Spawn Bullets
                //Rest the timer
                shoot_timer = rate_of_fire;
                //Create new bullet
                GameObject new_bullet = Instantiate(bullet);
                //Move the bullet to the position of the turret.
                new_bullet.transform.position = transform.position;

                //Axis to rotate bullet_dir by.
                Vector3 axis = Vector3.Cross(dir_to_target, Vector3.up);
                //Rotate bullet_dir along axis by spread.
                Vector3 spread_dir = Quaternion.AngleAxis(Random.Range(0f, spread), axis) * dir_to_target;
                //Rotate the spread_dir along the bullet dir by some angle between 0 and 360 degrees.
                Vector3 final_dir = Quaternion.AngleAxis(Random.Range(0f, 360f), dir_to_target) * spread_dir;


                // Pass the direction to the bullet.
                new_bullet.GetComponent<Bullet>().SetVelocity(final_dir);
            }
           
        }
    }

    private bool TargetInView()
    {
        //Flatten the dir_to_target to make it 2D.
        //We no longer have to worry about up/down, just about what is infront of the turret.
        Vector3 dir_to_target_flat = new Vector3(dir_to_target.x, 0, dir_to_target.z);

        if (Vector3.Dot(dir_to_target_flat, transform.forward) > dot_needed_to_see)
        {
            return true;
        }
        else
        {
            return false;
        }

    }


    private void OnDrawGizmos()
    {
        //Draw the vector representing the turret's forward direction.
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2);


        //Draw the vetor that represents our bullet's direction
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + dir_to_target * 2);


        #region Bullet Spread Visual Code
        //Line representing the up vector
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * 2);

        //Axis we will rotate bullet direction on to add spread to the turret.
        Gizmos.color = Color.cyan;
        Vector3 axis = Vector3.Cross(dir_to_target, Vector3.up);
        Gizmos.DrawLine(transform.position, transform.position + axis * 2);

        //Spread Direction, how much spread will be applied to the bullet direction.
        Gizmos.color = Color.yellow;
        Vector3 spread_dir = Quaternion.AngleAxis(spread, axis) * dir_to_target;
        Gizmos.DrawLine(transform.position, transform.position + spread_dir * 2);

        //Show the cone of potential directions the bullet can travel in.
        Gizmos.color = Color.magenta;
        Vector3 final_dir;
        for (int i = 1; i <= 15; i++)
        {
            final_dir = Quaternion.AngleAxis(22.5f * i, dir_to_target) * spread_dir;
            Gizmos.DrawLine(transform.position, transform.position + final_dir * 2);
        }
        #endregion

        #region Vision Code Visuals

        float half_angle = view_angle / 2;

        //Diretection for the left and right side of the view cone.
        Vector3 right = Quaternion.AngleAxis(half_angle, Vector3.up) * transform.forward;
        Vector3 left = Quaternion.AngleAxis(-half_angle, Vector3.up) * transform.forward;


        //Draw each side of the view cone.
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position + right * 10);
        Gizmos.DrawLine(transform.position, transform.position + left * 10);

        //Flatted dir_to_target that is used to detect if the atarget is infront of the turret.
        Vector3 dir_to_target_flat = new Vector3(dir_to_target.x, 0, dir_to_target.z).normalized;
        Gizmos.color = new Color(1f, 0.2705882f, 0f, 1f);
        Gizmos.DrawLine(transform.position, transform.position + dir_to_target_flat * 2);


        #endregion
    }






}
