using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;

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
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        // Direção baseada na câmera
        Vector3 forward = camTransform.forward;
        Vector3 right = camTransform.right;

        // Zera componente vertical
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * z + right * x;

        // Aplica movimento
        cc.Move(moveDirection * moveSpeed * Time.deltaTime);

        // Rotaciona player na direção do movimento
        if (moveDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        }

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
