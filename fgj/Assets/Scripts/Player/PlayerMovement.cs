using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D body;
    public float movementSpeed = 10;
    Vector2 input;

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        transform.rotation = Quaternion.LookRotation(Vector3.forward, transform.forward);
    }

    void FixedUpdate()
    {
        Vector2 moveIncrement = input * movementSpeed * Time.fixedDeltaTime;
        body.MovePosition(body.position + moveIncrement);
    }
}
