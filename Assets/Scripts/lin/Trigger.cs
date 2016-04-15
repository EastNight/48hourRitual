using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour {
    public bool IsSubtitle;
    public Subtitle subtitle;
    public float WaitTime;
    public bool selfEnable;
    public int Level;

    public bool StopPlayer;

    public bool OnceTime;
    public bool IsMiniGame;
    public bool NextLevel;
    public int nextLevel;
    public AudioClip clip;

    public GameObject obj;
    public GameObject obj2;

    public GameObject duankai;
    public Dialog dialog;
    public SkeletonAnimation sa;
    public string saAnimation;

    bool done=false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (OnceTime && done)
            return;
            

        if(obj != null){
            obj.SetActive(true);
            if(obj2 != null){
                obj2.SetActive(false);
            }
        }
        if (selfEnable)
        {
            Invoke("EnablePlayer", WaitTime);
        }
        if (clip!=null)
        {
          AudioSource src = gameObject.GetComponent<AudioSource>();
          if(src == null)
            src =gameObject.AddComponent<AudioSource>();
     
            src.PlayOneShot(clip,1);
        }
        //Invoke("EnablePlayer", 2f);
        if(IsSubtitle)
        {
            SubtitleEnable();
        }
        
        if(OnceTime)
        {
            done = true;
        }
        if(IsMiniGame)
        {
            if(GameMain.instance.StartLevel(Level)){
                StopPlayer = true;
                PlayerMove.Instance.SetDisable();
                PlayerMove.Instance.trigger = this;
            }
        }
        if(NextLevel)
        {
            PlayerMove.Instance.SetLevel(nextLevel);
        }
        if(dialog!=null)
        {
            if (StopPlayer)
            {
                PlayerMove.Instance.SetDisable();
            }
            Debug.Log("dialgo");
            dialog.enabled = true;
            dialog.StartDialog = true;
        }
    }
    void EnablePlayer()
    {
        if(sa!=null)
        {
            sa.gameObject.SetActive(true);
            sa.Reset();
            sa.AnimationName = saAnimation;
            Invoke("DisableSA", 1f);
        }
        PlayerMove.Instance.SetEnable();
        
    }
    void DisableSA()
    {
        sa.enabled = false;
    }
    void SubtitleEnable()
    {
        subtitle.SetEnable();
    }

}
