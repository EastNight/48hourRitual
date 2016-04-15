using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Subtitle : MonoBehaviour {
    Text t;
    public float FadeOutTime;
    public float FadeInTime;
    public float NormalTime;
	// Use this for initialization
	void Start () {
	    t= gameObject.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public void SetEnable()
    {
        gameObject.SetActive(true);
        FadeIn();
        Invoke("FadeOut", FadeInTime + NormalTime);
    }
    void FadeIn()
    {
        Color color = transform.GetComponent<Text>().color;
        Color startColor = new Color(color.r, color.g, color.b, 0f);
        Color endColor = new Color(color.r, color.g, color.b, 1f);
        transform.GetComponent<Text>().CrossFadeColor(startColor, 0f, false, true);
        transform.GetComponent<Text>().CrossFadeColor(endColor, FadeInTime, false, true);
    }
    public float FadeOut()
    {
        t = gameObject.GetComponent<Text>();
        t.CrossFadeAlpha(0, FadeOutTime, true);
        return FadeOutTime;
    }
    void SetDisable()
    {
        gameObject.SetActive(false);
    }
}
