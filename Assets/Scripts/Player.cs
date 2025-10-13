using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 3.5f;

    private CharacterController cc;
    private Vector3 velocity;

    private Transform camTransform;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cc = GetComponent<CharacterController>();

        // Guarda a referência da câmera principal
        if (Camera.main != null)
            camTransform = Camera.main.transform;
    }

    void Update()
    {
        if (camTransform == null) return;

        // Entrada WASD
        float x = Input.GetAxisRaw("Horizontal"); // A e D
        float z = Input.GetAxisRaw("Vertical");   // W e S

        // Movimento local do player
        Vector3 moveDirection = transform.right * x + transform.forward * z;
        moveDirection.Normalize();

        // Aplica movimento
        cc.Move(moveDirection * moveSpeed * Time.deltaTime);

        // Gravidade
        if (cc.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Pulo
        if (Input.GetButtonDown("Jump") && cc.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
    }
}
