using UnityEngine;

public class BatMovement : MonoBehaviour
{
    private float amplitude; //how big of a wave to fly in
    private float speed; //how fast to go back and forth

    [Header("Amplitude Range")]
    public float minAmp;
    public float maxAmp;

    [Header("Speed Range")]
    public float minSpeed;
    public float maxSpeed;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        amplitude = Random.Range(minAmp, maxAmp);
        speed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}