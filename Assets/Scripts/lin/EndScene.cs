using UnityEngine;
using System.Collections;

public class EndScene : MonoBehaviour {
    public float WaitTime=0f;
    public float subTilesTime1;
    public float subTilesTime2;
    public Subtitle[] subtitles;
    float time = 0;
	// Use this for initialization
	void Start () {
    	PlayAnim();
	}
    bool white = false;
    bool sub1 = false;
    bool sub2 = false;
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if(time>WaitTime && white==false)
        {
            white = true;
            BGMove.Instance.FadeIntoWhite(2f);
        }

        //if(time>WaitTime+subTilesTime1 && sub1==false)
        //{
        //    subtitles[0].SetEnable();
        //    sub1 = true;
        //}
        //if(time> WaitTime + subTilesTime1 + subTilesTime2 && sub2 == false)
        //{
        //    subtitles[1].SetEnable();
        //    sub2 = true;
        //}
    }

    public void PlayAnim(){
        SkeletonAnimation sucAnim = PlayerMove.Instance.transform.Find("anim4").GetComponent<SkeletonAnimation>();
        sucAnim.Reset();
        sucAnim.AnimationName = "1";
    }
}
