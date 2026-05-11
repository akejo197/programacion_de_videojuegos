using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Campo para asignar el sonido desde el Inspector
    public AudioClip collectSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Reproduce el sonido en la posición de la moneda
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }

            // Desaparece la moneda
            gameObject.SetActive(false);

            Debug.Log("Moneda recogida con sonido y desaparecida");
        }
    }
}
