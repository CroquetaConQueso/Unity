using UnityEngine;
using UnityEngine.SceneManagement; 

public class MenuPrincipal : MonoBehaviour
{
    public void Jugar()
    {

        SceneManager.LoadScene(1);
    }

    public void Salir()
    {
        Debug.Log("Saliendo del juego...");


        Application.Quit();


        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}