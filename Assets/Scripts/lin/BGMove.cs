using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BGMove : MonoBehaviour {
    public float playerHigh;
    public GameObject player;
    public Camera camera;
    public GameObject white;

    public GameObject[] header;
    public static BGMove Instance;
    void Start()
    {
        Instance = this;
    }
    public bool VerticleMove(float verticleSpeed,int level)
    {
        var playPos = camera.WorldToScreenPoint(player.transform.position);
        if (verticleSpeed < 0)
            return false;
        var headerPos = camera.WorldToScreenPoint(header[level].transform.position);
        if (headerPos.y<Screen.height)
        {
            return false;
        }
        if (playPos.y > playerHigh)
        {
            transform.Translate(new Vector3(0, verticleSpeed * Time.deltaTime));
            return true;
        }
        return false;
    }
    public void FadeIntoWhite(float FadeInTime)
    {
        white.SetActive(true);
        Color color = white.GetComponent<Image>().color;
        Color startColor = new Color(color.r, color.g, color.b, 0f);
        Color endColor = new Color(color.r, color.g, color.b, 1f);
        white.GetComponent<Image>().CrossFadeColor(startColor, 0f, false, true);
        white.GetComponent<Image>().CrossFadeColor(endColor, FadeInTime, false, true);
    }
}
