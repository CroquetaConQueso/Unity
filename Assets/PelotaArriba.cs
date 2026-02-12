using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PelotaArriba : MonoBehaviour
{
    [Header("Configuración del Salto")]
    public float fuerzaSaltoInicial = 6f;
    public float fuerzaSaltoMaxima = 15f;
    public float sensibilidadHorizontal = 5f;
    public float multiplicadorDificultad = 1.1f;

    [Header("Configuración de Tamaño")]
    [Tooltip("Porcentaje que se reduce cada 15 clics (0.95 = reduce un 5%)")]
    public float factorReduccion = 0.95f;
    [Tooltip("Tamaño mínimo permitido (0.6 = 60% del tamaño original)")]
    public float limiteTamanoMinimo = 0.6f;

    [Header("Interfaz y Enemigos")]
    public TextMeshProUGUI textoPuntuacion;
    public GeneradorObstaculos scriptGenerador;

    private Rigidbody2D rb;
    private float fuerzaSaltoActual;
    private Camera camaraPrincipal;
    private float bordeInferiorPantalla;
    private bool juegoTerminado = false;
    private int puntuacionActual = 0;

    private Vector3 escalaOriginal;
    private int contadorClicksParaAchicar = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        camaraPrincipal = Camera.main;
        fuerzaSaltoActual = fuerzaSaltoInicial;

        escalaOriginal = transform.localScale;

        float alturaPantalla = camaraPrincipal.orthographicSize * 2;
        bordeInferiorPantalla = -(alturaPantalla / 2) - 2f;

        ActualizarTextoPuntuacion();
    }

    void Update()
    {
        if (juegoTerminado) return;

        DetectarEntrada();
        VerificarFinJuego();
    }

    void DetectarEntrada()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 posicionEntrada = camaraPrincipal.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D[] impactos = Physics2D.RaycastAll(posicionEntrada, Vector2.zero);

            foreach (RaycastHit2D impacto in impactos)
            {
                if (impacto.collider.gameObject == gameObject)
                {
                    Rebotar(posicionEntrada);
                    break;
                }
            }
        }
    }

    void Rebotar(Vector2 puntoToque)
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);

        float diferenciaX = transform.position.x - puntoToque.x;
        float empujeHorizontal = diferenciaX * sensibilidadHorizontal;

        Vector2 fuerza = new Vector2(empujeHorizontal, fuerzaSaltoActual);
        rb.AddForce(fuerza, ForceMode2D.Impulse);

        puntuacionActual++;
        ActualizarTextoPuntuacion();
        IncrementarDificultad();

        GestionarTamaño();
        GestionarEnemigos();
    }

    void GestionarTamaño()
    {
        contadorClicksParaAchicar++;

        if (contadorClicksParaAchicar >= 15)
        {
            Vector3 nuevoTamano = transform.localScale * factorReduccion;
            float tamanoLimite = escalaOriginal.x * limiteTamanoMinimo;

            if (nuevoTamano.x >= tamanoLimite)
            {
                transform.localScale = nuevoTamano;
            }

            contadorClicksParaAchicar = 0;
        }
    }

    void GestionarEnemigos()
    {
        if (puntuacionActual == 10 && scriptGenerador != null)
        {
            scriptGenerador.ActivarGeneracion();
        }
    }

    void ActualizarTextoPuntuacion()
    {
        if (textoPuntuacion != null)
        {
            textoPuntuacion.text = puntuacionActual.ToString();
        }
    }

    void IncrementarDificultad()
    {
        fuerzaSaltoActual *= multiplicadorDificultad;
        if (fuerzaSaltoActual > fuerzaSaltoMaxima)
        {
            fuerzaSaltoActual = fuerzaSaltoMaxima;
        }

        GetComponent<SpriteRenderer>().color = Random.ColorHSV(0f, 1f, 0.8f, 1f, 0.8f, 1f);
    }

    void VerificarFinJuego()
    {
        if (transform.position.y < bordeInferiorPantalla)
        {
            juegoTerminado = true;
            ReiniciarJuego();
        }
    }

    void ReiniciarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}