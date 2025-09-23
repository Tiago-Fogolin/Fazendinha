using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 2f, -4f); // posi��o atr�s e acima do alvo
    public float mouseSensitivity = 100f;
    public float minPitch = -20f; // limitar para n�o ir abaixo do ch�o
    public float maxPitch = 50f;

    private float yaw = 0f;
    private float pitch = 10f;

    void LateUpdate()
    {
        if (target == null) return;

        // Movimento do mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // Rota��o baseada em yaw/pitch
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);

        // Offset rotacionado em torno do alvo
        Vector3 desiredPosition = target.position + rotation * offset;

        transform.position = desiredPosition;

        // Olhar para o alvo (subindo um pouco para mirar na cabe�a)
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}
