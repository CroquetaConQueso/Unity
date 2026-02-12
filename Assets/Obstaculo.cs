using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstaculo : MonoBehaviour
{
    public float velocidad = 5f;
    private Vector2 direccionMovimiento;

    public void Configurar(Vector2 direccion)
    {
        direccionMovimiento = direccion;
        Destroy(gameObject, 6f);
    }

    void Update()
    {
        transform.Translate(direccionMovimiento * velocidad * Time.deltaTime);
    }

}