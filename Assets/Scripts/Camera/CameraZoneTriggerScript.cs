using Unity.Cinemachine;
using UnityEngine;
using System.Collections;

public class CameraZoneTriggerScript : MonoBehaviour
{
    [SerializeField] private CinemachineCamera newCamera;
    [SerializeField] private bool isHasExit = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            newCamera.Priority = 20;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isHasExit)
        {
            if (other.CompareTag("Player"))
            {
                newCamera.Priority = 10;
            }
        }
    }
}
