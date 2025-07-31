using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;
    Camera cam;

    public float maxCamX = 124f; 
    public float minCamX = 21f;    
    public float maxCamY = 7.5f;
    public float cameraZOffset = -10f;

    private bool isFollowing = false;

    void Start()
    {
        Camera.main.transform.position = new Vector3(21f, 4f, 0f);
        player = GameObject.Find("Player");
        cam = Camera.main;
    }

    void LateUpdate()
    {
        Vector3 camPos = transform.position;

        if (!isFollowing && player.transform.position.x > 21f)
        {
            isFollowing = true;
        }

        if (isFollowing)
        {
            camPos.x = player.transform.position.x;

            float camHeight = cam.orthographicSize * 2f;
            camPos.y = player.transform.position.y + camHeight * 0.25f;

            camPos.x = Mathf.Clamp(camPos.x, minCamX, maxCamX);
            camPos.y = Mathf.Min(camPos.y, maxCamY);
        }

        camPos.z = cameraZOffset;
        transform.position = camPos;
    }
}
