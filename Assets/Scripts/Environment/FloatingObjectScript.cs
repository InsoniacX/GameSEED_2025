using UnityEngine;

public class FloatingObjectScript : MonoBehaviour
{
    [Range(0.1f, 5f)]
    public float WaitBetweenWobbles = 0.5f;

    [Range(1f, 50f)]
    public float Intensity = 10f;

    Quaternion _targetAngle;

    [SerializeField] private float floatSpeed = 1f;
    [SerializeField] private float floatHeight = 0.5f;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
        InvokeRepeating("ChangeTarget", 0, WaitBetweenWobbles);
    }

    private void Update()
    {
        /*Vector3 floatOffset = new Vector3(
            Mathf.Sin(Time.time * 0.5f),
            Mathf.Cos(Time.time * 0.75f),
            0
        ) * 0.05f;

        transform.position += floatOffset * Time.deltaTime;*/
        //transform.rotation = Quaternion.Lerp(transform.rotation, _targetAngle, Time.deltaTime);

        //transform.rotation = Quaternion.Lerp(transform.rotation, _targetAngle, Time.deltaTime);
        transform.position = startPos + Vector3.up * Mathf.Sin(Time.time * floatSpeed) * floatHeight;
    }

    void ChangeTarget()
    {
        var intensity = Random.Range(0.1f, Intensity);
        var curve = Mathf.Sin(Random.Range(0, Mathf.PI * 2));
        _targetAngle = Quaternion.Euler(Vector3.forward * curve * intensity);
    }
}
