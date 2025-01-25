using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointAndDeathHandler : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private Image overlayImage; // ��������� �� UI Image ��� �������� �������
    [SerializeField] private Color startColor = Color.white; // ���������� ���� UI
    [SerializeField] private Color endColor = Color.red; // ʳ������ ���� UI
    [SerializeField] private float transitionDuration = 2f; // ��������� �������� �������
    [SerializeField] private float returnDuration = 2f; // ��������� ���������� �� ����������� �������

    [Header("Respawn Settings")]
    [SerializeField] private float respawnDelay = 2f; // �������� ����� �����������

    private Vector3 lastCheckpointPosition; // ������� ���������� ������������ ������
    private bool isRespawning = false; // ���� ��� ��������� ���������� ������� ����������

    private void Start()
    {
        // ������������ ����������� ������� UI
        if (overlayImage != null)
        {
            overlayImage.color = startColor;
        }

        // ����������� ������� ������������ ������
        lastCheckpointPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���� ��� ���������� �������, �� �������� 䳿
        if (isRespawning) return;

        // �������� ����
        if (other.CompareTag("Checkpoint"))
        {
            // ���������� ������� ������������ ������
            Debug.Log("Checkpoint reached!");
            lastCheckpointPosition = other.transform.position;
        }
        else if (other.CompareTag("Death"))
        {
            // ������� �����
            Debug.Log("Death zone entered!");
            StartCoroutine(HandleDeath());
        }
    }

    private IEnumerator HandleDeath()
    {
        isRespawning = true;

        // ������� ������� ������� UI �� �������� �������
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

        // ���������� ����� ���������
        yield return new WaitForSeconds(respawnDelay);

        // ���������� �� ������������ ������
        transform.position = lastCheckpointPosition;

        // ������ ���������� ������� UI �� �����������
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
