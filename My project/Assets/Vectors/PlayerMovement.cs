using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Components
    private Rigidbody rb;


    //Movement
    public float speed = 5;

    private Vector3 direction = Vector3.zero;

    private Vector3 velocity = Vector3.zero;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Get Components.
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        //Get Input
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        //Contruct velocity vector.
        velocity = direction * speed;
        velocity *= Time.fixedDeltaTime;


        //Move player.
        rb.MovePosition(rb.position + velocity);
    }
}

