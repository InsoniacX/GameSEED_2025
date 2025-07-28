using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PortalScript : MonoBehaviour
{
    private HashSet<GameObject> portalObjects = new HashSet<GameObject>();
    [SerializeField] private Transform[] destinations;
    public bool isOnedestination = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        if (portalObjects.Contains(collision.gameObject))
        {
            return;
        }

        foreach (Transform destination in destinations)
        {
            if (destination.TryGetComponent(out PortalScript destinationPortal))
            {
                destinationPortal.portalObjects.Add(collision.gameObject);
            }
        }

        if (isOnedestination)
            collision.transform.position = destinations[0].position;
        else
            collision.transform.position = destinations[Random.Range(0, 5)].position;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        portalObjects.Remove(collision.gameObject);   
    }
}
