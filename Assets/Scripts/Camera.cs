using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 2f, -4f);
    public float mouseSensitivity = 100f;

    private float yaw = 0f;   // Rotação horizontal
    private float pitch = 10f; // Rotação vertical inicial

    void Update()
    {
        if (target == null) return;

        // Pegando movimento do mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Atualiza rotação da câmera
        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -35f, 60f); // Limita para não girar demais

        // Calcula posição da câmera
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 desiredPosition = target.position + rotation * offset;

        // Move a câmera
        transform.position = desiredPosition;
        transform.LookAt(target.position);
    }
}
