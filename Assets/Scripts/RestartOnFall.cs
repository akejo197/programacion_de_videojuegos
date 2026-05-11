using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartOnFall : MonoBehaviour
{
    [SerializeField] private float fallThreshold = -5f; 
    // Ajusta este valor según la altura mínima antes de considerar que el jugador "cayó"

    void Update()
    {
        if (transform.position.y < fallThreshold)
        {
            // Reinicia la escena actual
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
