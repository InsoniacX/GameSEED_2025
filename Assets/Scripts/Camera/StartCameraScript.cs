using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class StartCameraScript : MonoBehaviour
{
    [SerializeField] private CinemachineCamera firstCamera;
    [SerializeField] private CinemachineCamera secondCamera;
    [SerializeField] private CinemachineCamera mainCamera;
    [SerializeField] private Rigidbody2D playerBody;

    void Start()
    {
        mainCamera.Priority = 10;
        playerBody.simulated = false;
        StartCoroutine(reducePriority(2f));
    }

    private IEnumerator reducePriority(float delay)
    {
        yield return new WaitForSeconds(delay);
        firstCamera.Priority = 10;
        secondCamera.Priority = 20;
        StartCoroutine(mainPriority(2f));
    }

    private IEnumerator mainPriority(float delay)
    {
        yield return new WaitForSeconds(delay);
        secondCamera.Priority = 10;
        mainCamera.Priority = 20;
        playerBody.simulated = true;
    }
}
