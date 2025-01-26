using System.Collections;
using Game.Player;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointAndDeathHandler : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private Image overlayImage;
    [SerializeField] private Color startColor = Color.white;
    [SerializeField] private Color endColor = Color.red;
    [SerializeField] private float transitionDuration = 2f;
    [SerializeField] private float returnDuration = 2f;
    [SerializeField] private Player player;

    [Header("Respawn Settings")]
    [SerializeField] private float respawnDelay = 2f;

    private Vector3 lastCheckpointPosition;
    private bool isRespawning = false;

    private void Start()
    {
        if (overlayImage != null)
        {
            overlayImage.color = startColor;
        }
        lastCheckpointPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isRespawning) return;

        if (other.CompareTag("Checkpoint"))
        {
            Debug.Log("Checkpoint reached!");
            lastCheckpointPosition = other.transform.position;
        }
        else if (other.CompareTag("Death"))
        {
            Debug.Log("Death zone entered!");
            StartCoroutine(HandleDeath());
        }
    }

    private IEnumerator HandleDeath()
    {
        isRespawning = true;

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

        yield return new WaitForSeconds(respawnDelay);
        
        transform.position = lastCheckpointPosition;

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
        
        if (player != null)
        {
            player.Appear();
        }

        isRespawning = false;
    }
}
