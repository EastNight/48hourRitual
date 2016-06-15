using UnityEngine;
using System.Collections;

public class EndScene : MonoBehaviour {
    public float WaitTime=0f;
    public float subTilesTime1;
    public float subTilesTime2;
    public Subtitle[] subtitles;
    float time = 0;
    bool white = false;
    bool sub1 = false;
    bool sub2 = false;
    void Start () {
    	PlayAnim();
    }

    void Update () {
        time += Time.deltaTime;
        if(time>WaitTime && white==false)
        {
            white = true;
            BGMove.Instance.FadeIntoWhite(2f);
        }
    }

    public void PlayAnim(){
        SkeletonAnimation sucAnim = PlayerMove.Instance.transform.Find("anim4").GetComponent<SkeletonAnimation>();
        sucAnim.Reset();
        sucAnim.AnimationName = "1";
    }
}
