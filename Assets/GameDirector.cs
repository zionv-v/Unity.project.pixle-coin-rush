using TMPro;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    GameObject timerText;
    float time = 0f;
    bool isCounting = false;

    GameObject pointText;
    int point = 0;

    GameObject player;
    public static float finalTime;
    public static int finalPoint;

    void Start()
    {
        timerText = GameObject.Find("Time");
        pointText = GameObject.Find("Point");
        player = GameObject.Find("Player");
    }

    public void GetCoin()
    {
        point += 20;
    }

    public void GetObstacle()
    {
        point -= 5;
    }

    void Update()
    {
        float playerSpeed = Mathf.Abs(player.GetComponent<Rigidbody2D>().linearVelocity.x);

        if (!isCounting && playerSpeed > 0.1f)
        {
            isCounting = true;
        }

        if (isCounting)
        {
            time += Time.deltaTime;

            int minutes = (int)(time / 60);
            float seconds = time % 60f;
            timerText.GetComponent<TextMeshProUGUI>().text =
                string.Format("{0:00}:{1:00.0}", minutes, seconds);
        }

        pointText.GetComponent<TextMeshProUGUI>().text = point + " point";
        finalTime = time;
        finalPoint = point;
    }
}