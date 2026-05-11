using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalTrigger : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            winPanel.SetActive(true); // muestra el panel de victoria
            // Opcional: detener movimiento del Player
            // other.GetComponent<PlayerMovement>().enabled = false;
        }
    }

    // Método opcional para botón "Menú Principal"
    public void LoadMenu()
    {
        SceneManager.LoadScene("MenuPrincipal"); // cambia por el nombre de tu escena de menú
    }
}
