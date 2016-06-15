using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainTitle : MonoBehaviour {
    Image t;
    public float FadeOutTime;
    public float FadeInTime;
    public float NormalTime;
    void Start()
    {
        t = gameObject.GetComponent<Image>();
    }

    public void SetEnable()
    {
        gameObject.SetActive(true);
        FadeIn();
        Invoke("FadeOut", FadeInTime + NormalTime);
    }
    void FadeIn()
    {
        Color color = transform.GetComponent<Image>().color;
        Color startColor = new Color(color.r, color.g, color.b, 0f);
        Color endColor = new Color(color.r, color.g, color.b, 1f);
        transform.GetComponent<Image>().CrossFadeColor(startColor, 0f, false, true);
        transform.GetComponent<Image>().CrossFadeColor(endColor, FadeInTime, false, true);
    }
    public float FadeOut()
    {
        t = gameObject.GetComponent<Image>();
        t.CrossFadeAlpha(0, FadeOutTime, true);
        return FadeOutTime;
    }
    void SetDisable()
    {
        gameObject.SetActive(false);
    }
}
