using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //Components
    private Rigidbody rb;

    //Input

    //private InputAction move_action;
    //private InputAction jump_action;

    //Movement
    public float speed = 5;

    private Vector3 direction = Vector3.zero;

    private Vector3 velocity = Vector3.zero;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Get Components.
        rb = GetComponent<Rigidbody>();

        //Find references to the actions.
        //move_action = InputSystem.actions.FindAction("Move");
        //jump_action = InputSystem.actions.FindAction("Jump");
    }

    // Update is called once per frame
    void Update()
    {

        //Get Input
       //direction.x = Input.GetAxisRaw("Horizontal");
       //direction.z = Input.GetAxisRaw("Vertical");
       //direction = direction.normalized;

        //direction.x = move_action.ReadValue<Vector2>().x;
        //direction.z = move_action.ReadValue<Vector2>().y;
       // direction = direction.normalized;
    }

    private void FixedUpdate()
    {
        //Contruct velocity vector.
        velocity = direction * speed;
        velocity *= Time.fixedDeltaTime;


        //Move player.
        rb.MovePosition(rb.position + velocity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; 
        Gizmos.DrawLine(transform.position, transform.position + direction * 2);

    }

    public void ReadMoveInput(InputAction.CallbackContext context)
    {
        direction.x = context.ReadValue<Vector2>().x;
        direction.z = context.ReadValue<Vector2>().y;
    }
}

