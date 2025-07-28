using UnityEngine;
using System.Collections;


public class MovingObjectScript : MonoBehaviour
{
    public float speed;
    public int startingPoint;
    public Transform[] points;
    private int i;
    public bool isPlayerOnBoat = false;
    private Vector2 lastPosition;
    public bool isMovingRight;

    private void Start()
    {
        transform.position = points[startingPoint].position;
        lastPosition = transform.position;
    }

    private void Update()
    {
       if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if (i == points.Length)
            {
                i = 0;
            }
        }

        Vector2 currentPosition = transform.position;
        Vector2 direction = currentPosition - lastPosition;
        isMovingRight = direction.x > 0;
        lastPosition = currentPosition;

        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
            isPlayerOnBoat = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //StartCoroutine(DetachFromBoatDelayed(collision.transform));
            collision.transform.SetParent(null);
            isPlayerOnBoat = false;
        }
    }

    //private IEnumerator DetachFromBoatDelayed(Transform player)
    //{
    //    yield return null;
    //    player.SetParent(null);
    //}
}
