using UnityEngine;

public class GeneradorObstaculos : MonoBehaviour
{
    public GameObject prefabObstaculo;
    public float intervaloGeneracion = 1.5f;

    private bool activo = false;
    private Camera camara;
    private float anchoPantalla;
    private float altoPantalla;

    void Start()
    {
        camara = Camera.main;
        altoPantalla = camara.orthographicSize;
        anchoPantalla = altoPantalla * camara.aspect;
    }

    public void ActivarGeneracion()
    {
        if (!activo)
        {
            activo = true;
            InvokeRepeating(nameof(GenerarObstaculo), 0f, intervaloGeneracion);
        }
    }

    void GenerarObstaculo()
    {
        float yAleatoria = Random.Range(-altoPantalla + 1, altoPantalla - 1);
        bool ladoIzquierdo = Random.value > 0.5f;

        float xInicio = ladoIzquierdo ? -anchoPantalla - 1 : anchoPantalla + 1;
        Vector2 posicion = new Vector2(xInicio, yAleatoria);

        GameObject nuevoObstaculo = Instantiate(prefabObstaculo, posicion, Quaternion.identity);

        Vector2 direccion = ladoIzquierdo ? Vector2.right : Vector2.left;
        nuevoObstaculo.GetComponent<Obstaculo>().Configurar(direccion);
    }
}