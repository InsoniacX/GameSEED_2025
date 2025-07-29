using UnityEngine;

public class Footstep : MonoBehaviour
{
    public AudioSource footstepsSound;

    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            footstepsSound.enabled = true;
        }
        else
        {
            footstepsSound.enabled = false;
            //sprintSound.enabled = false;
        }
    }
}
