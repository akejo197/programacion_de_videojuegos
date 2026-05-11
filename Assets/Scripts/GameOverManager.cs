using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private float fallThreshold = -5f;

    private bool isGameOver = false;

    void Update()
    {
        if (!isGameOver && transform.position.y < fallThreshold)
        {
            TriggerGameOver();
        }
    }

    void TriggerGameOver()
    {
        isGameOver = true;
        gameOverPanel.SetActive(true); // muestra el panel
        // ❌ Ya no reiniciamos automáticamente
    }

    // Este método lo llamará el botón
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
