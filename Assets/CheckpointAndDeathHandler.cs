using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointAndDeathHandler : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private Image overlayImage; // Посилання на UI Image для переходу кольору
    [SerializeField] private Color startColor = Color.white; // Початковий колір UI
    [SerializeField] private Color endColor = Color.red; // Кінцевий колір UI
    [SerializeField] private float transitionDuration = 2f; // Тривалість переходу кольору
    [SerializeField] private float returnDuration = 2f; // Тривалість повернення до початкового кольору

    [Header("Respawn Settings")]
    [SerializeField] private float respawnDelay = 2f; // Затримка перед відродженням

    private Vector3 lastCheckpointPosition; // Позиція останнього контрольного пункту
    private bool isRespawning = false; // Флаг для уникнення дублювання процесу відродження

    private void Start()
    {
        // Встановлення початкового кольору UI
        if (overlayImage != null)
        {
            overlayImage.color = startColor;
        }

        // Ініціалізація позиції контрольного пункту
        lastCheckpointPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Якщо вже відбувається респавн, не виконуємо дії
        if (isRespawning) return;

        // Перевірка тегів
        if (other.CompareTag("Checkpoint"))
        {
            // Збереження позиції контрольного пункту
            Debug.Log("Checkpoint reached!");
            lastCheckpointPosition = other.transform.position;
        }
        else if (other.CompareTag("Death"))
        {
            // Обробка смерті
            Debug.Log("Death zone entered!");
            StartCoroutine(HandleDeath());
        }
    }

    private IEnumerator HandleDeath()
    {
        isRespawning = true;

        // Плавний перехід кольору UI до кінцевого кольору
        if (overlayImage != null)
        {
            float elapsedTime = 0f;
            while (elapsedTime < transitionDuration)
            {
                elapsedTime += Time.deltaTime;
                overlayImage.color = Color.Lerp(startColor, endColor, elapsedTime / transitionDuration);
                yield return null;
            }
        }

        // Очікування перед респавном
        yield return new WaitForSeconds(respawnDelay);

        // Повернення до контрольного пункту
        transform.position = lastCheckpointPosition;

        // Плавне повернення кольору UI до початкового
        if (overlayImage != null)
        {
            float elapsedTime = 0f;
            while (elapsedTime < returnDuration)
            {
                elapsedTime += Time.deltaTime;
                overlayImage.color = Color.Lerp(endColor, startColor, elapsedTime / returnDuration);
                yield return null;
            }
        }

        isRespawning = false;
    }
}
