using UnityEngine;
using System.Collections.Generic;

public class StartScene : MonoBehaviour {
    bool start=false;
    public MainTitle title;
    public float resizeTime;
    public RectTransform rectTrans;
    float WaitTime = 1f;
    float time = 0;
    float deltaSize;
    bool titleShow = true;
    bool once = false;
    bool startAnimat = false;
    bool Go = false;
    int click = 0;
    int index = 0;
    List<string> saName;
    List<float> times;
    SkeletonAnimation sa;

    void Start () {
        title = GameObject.Find("SubtitleCanvas/Title").GetComponent<MainTitle>();
        saName =new List<string> (){ "stand", "wake", "walk2" };
        times = new List<float>() { 1,      6.3f,       2f,     1f,1f,1f };
        sa = PlayerMove.Instance.startSA;
        sa.Reset();
        sa.AnimationName = saName[0];
    }
    public void TitleFadeOut()
    {
        float outTime= title.FadeOut();
        Invoke("SetTitleShowFalse", outTime);
    }
    void SetTitleShowFalse()
    {
        titleShow = false;
    }
	public bool GameStartSignal()
    {
        if (titleShow)
            return false;


        sa.Reset();
        sa.AnimationName = saName[1];
        index = 1;
        startAnimat = true;
        Invoke("Animation", times[1]);
       
        return true;
    }

    void Animation()
    {
        index++;
        sa.Reset();
        
        if(time>5f && Go==false)
        {
            Go = true;
            transform.GetComponent<Animator>().enabled = true;
            LeanTweenTool.AnchoredPosition(rectTrans, WaitTime, new Vector3(0, 600, 0), new Vector3(0, 685, 0));
        }
        if(index< saName.Count)
        {
            sa.AnimationName = saName[index];
            Invoke("Animation", times[index]);
        }
        
    }

    void Update () {
        if (startAnimat)
            time += Time.deltaTime;

        if (camera.orthographicSize >= 1000)
        {
            start = false;
            camera.orthographicSize = 1000;
            transform.GetComponent<Animator>().enabled=false;
            Invoke("NextLevel", WaitTime);
        }
    }
    public void OnClick()
    {
        if(click==0)
        {
            TitleFadeOut();
            
        }
        if(titleShow==false)
        {
            if(once==false)
             once=GameStartSignal();
        }
        click++;
    }
    void NextLevel()
    {
        title.gameObject.SetActive(false);
        PlayerMove.Instance.SetLevel(1);
        Destroy(this);
    }
}
