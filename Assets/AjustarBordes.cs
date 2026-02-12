using UnityEngine;

public class AjustarBordes : MonoBehaviour
{
    public Transform muroIzquierdo;
    public Transform muroDerecho;
    public Transform techo;

    
    void Update()
    {
        // Esto es para calcular las coordenadas
        Vector2 esquinaInferiorIzquierda = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector2 esquinaSuperiorDerecha = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        // Para el muro izquierdo
        if (muroIzquierdo != null)
        {
            muroIzquierdo.position = new Vector3(esquinaInferiorIzquierda.x - 0.5f, 0, 0);
            muroIzquierdo.localScale = new Vector3(1, esquinaSuperiorDerecha.y * 2 + 2, 1);
        }

        // Para el muro derecho
        if (muroDerecho != null)
        {
            muroDerecho.position = new Vector3(esquinaSuperiorDerecha.x + 0.5f, 0, 0);
            muroDerecho.localScale = new Vector3(1, esquinaSuperiorDerecha.y * 2 + 2, 1);
        }

        // Para el techo
        if (techo != null)
        {
            techo.position = new Vector3(0, esquinaSuperiorDerecha.y + 0.5f, 0);
            float anchoPantalla = (esquinaSuperiorDerecha.x - esquinaInferiorIzquierda.x);
            techo.localScale = new Vector3(anchoPantalla + 2, 1, 1);
        }
    }
}