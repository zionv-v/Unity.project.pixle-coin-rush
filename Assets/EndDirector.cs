using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndDirector : MonoBehaviour
{
    public TextMeshProUGUI timeResultText;
    public TextMeshProUGUI pointResultText;

    void Start()
    {
        Debug.Log("EndDirector Start 실행됨");
        Debug.Log("받은 점수: " + GameDirector.finalPoint);
        Debug.Log("받은 시간: " + GameDirector.finalTime);

        float time = GameDirector.finalTime;
        int point = GameDirector.finalPoint;
        int minutes = (int)(time / 60);
        float seconds = time % 60f;

        timeResultText.text = $"Time: {minutes:00}:{seconds:00.0}";
        pointResultText.text = $"Point: {point}";
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("StartScene");
        }
    }
}
