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
    private float startY;


    private void Start()
    {
        startPos = transform.position;
        InvokeRepeating("ChangeTarget", 0, WaitBetweenWobbles);
        startY = transform.localPosition.y;
    }

    private void Update()
    {
        Vector3 localPos = transform.localPosition;
        localPos.y = startY + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.localPosition = localPos;
    }

    void ChangeTarget()
    {
        var intensity = Random.Range(0.1f, Intensity);
        var curve = Mathf.Sin(Random.Range(0, Mathf.PI * 2));
        _targetAngle = Quaternion.Euler(Vector3.forward * curve * intensity);
    }
}
