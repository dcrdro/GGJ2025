using UnityEngine;

public class DynamicRandomCyclicMovement : MonoBehaviour
{
    [Header("Position Settings")]
    public Vector3 minPositionOffset; // Minimum position offset
    public Vector3 maxPositionOffset; // Maximum position offset

    [Header("Rotation Settings")]
    public Vector3 minRotationOffset; // Minimum rotation offset (Euler angles)
    public Vector3 maxRotationOffset; // Maximum rotation offset (Euler angles)

    [Header("Random Speed Settings")]
    public float minSpeed = 0.5f; // Minimum speed for random movement
    public float maxSpeed = 2.0f; // Maximum speed for random movement

    private Vector3 initialPosition;
    private Vector3 initialRotation;

    private float positionSpeedX;
    private float positionSpeedY;
    private float positionSpeedZ;

    private float rotationSpeedX;
    private float rotationSpeedY;
    private float rotationSpeedZ;

    private float randomSeedX;
    private float randomSeedY;
    private float randomSeedZ;

    private void Start()
    {
        // Save the initial position and rotation
        initialPosition = transform.position;
        initialRotation = transform.eulerAngles;

        // Randomize speeds for each axis
        positionSpeedX = Random.Range(minSpeed, maxSpeed);
        positionSpeedY = Random.Range(minSpeed, maxSpeed);
        positionSpeedZ = Random.Range(minSpeed, maxSpeed);

        rotationSpeedX = Random.Range(minSpeed, maxSpeed);
        rotationSpeedY = Random.Range(minSpeed, maxSpeed);
        rotationSpeedZ = Random.Range(minSpeed, maxSpeed);

        // Random seeds for noise (to avoid identical movement on all objects)
        randomSeedX = Random.Range(0f, 100f);
        randomSeedY = Random.Range(0f, 100f);
        randomSeedZ = Random.Range(0f, 100f);
    }

    private void Update()
    {
        // Dynamic random position using Mathf.PerlinNoise
        float posX = Mathf.Lerp(minPositionOffset.x, maxPositionOffset.x, Mathf.PerlinNoise(Time.time * positionSpeedX, randomSeedX));
        float posY = Mathf.Lerp(minPositionOffset.y, maxPositionOffset.y, Mathf.PerlinNoise(Time.time * positionSpeedY, randomSeedY));
        float posZ = Mathf.Lerp(minPositionOffset.z, maxPositionOffset.z, Mathf.PerlinNoise(Time.time * positionSpeedZ, randomSeedZ));
        transform.position = initialPosition + new Vector3(posX, posY, posZ);

        // Dynamic random rotation using Mathf.PerlinNoise
        float rotX = Mathf.Lerp(minRotationOffset.x, maxRotationOffset.x, Mathf.PerlinNoise(Time.time * rotationSpeedX, randomSeedX));
        float rotY = Mathf.Lerp(minRotationOffset.y, maxRotationOffset.y, Mathf.PerlinNoise(Time.time * rotationSpeedY, randomSeedY));
        float rotZ = Mathf.Lerp(minRotationOffset.z, maxRotationOffset.z, Mathf.PerlinNoise(Time.time * rotationSpeedZ, randomSeedZ));
        transform.eulerAngles = initialRotation + new Vector3(rotX, rotY, rotZ);
    }
}
