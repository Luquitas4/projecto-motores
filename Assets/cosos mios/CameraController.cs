using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform jugador;
    public Vector3 distancia = new Vector3(0, 5, -8);
    public float suavizado = 8f;

    void LateUpdate()
    {
        if (jugador == null) return;
        Vector3 posDeseada = jugador.position + distancia;
        transform.position = Vector3.Lerp(transform.position, posDeseada, suavizado * Time.deltaTime);
        transform.LookAt(jugador);
    }
}
