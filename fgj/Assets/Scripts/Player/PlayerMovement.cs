using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D body;
    public float movementSpeed = 3;
    public GameObject sprite;
    Vector2 input;

    private AudioSource audioSource;
    public AudioClip walkAudio;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        Vector2 moveIncrement = input * movementSpeed * Time.fixedDeltaTime;
        body.MovePosition(body.position + moveIncrement);

        Debug.Log("magn" + moveIncrement.magnitude);
        if (moveIncrement.magnitude > 0.001)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(walkAudio);
            }
        }

        /*
        Vector3 uusiV3 = new Vector3(input.x, input.y, 0);
        sprite.transform.rotation = Quaternion.LookRotation(input);
    */
        // Get Angle in Radians
        float AngleRad = Mathf.Atan2(input.y * 1000 - sprite.transform.position.y, input.x * 1000 - sprite.transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object
        sprite.transform.rotation = Quaternion.Euler(0, 0, AngleDeg - 90);
    }
}
