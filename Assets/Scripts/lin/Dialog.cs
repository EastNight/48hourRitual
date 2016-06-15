using UnityEngine;
using System.Collections;

public class Dialog : MonoBehaviour {
    public Subtitle[] subtitle;
    public float WaitTime = 0;
    bool[] played;
    public float second;
    public bool StartDialog = false;
    float time = 0;
    void Start () {
        played = new bool[25];
        for(int i=0;i<25;i++)
        {
            played[i] = false;
        }
            time = 0;
    }
	
    void Update () {
        if(StartDialog)
        {
            time += Time.deltaTime;
            for (int i = 0; i < subtitle.Length; i++)
            {
                if (time > (i * second+WaitTime) && played[i] == false)
                {
                    played[i] = true;
                    subtitle[i].SetEnable();
                }
            }
            if(time>second*subtitle.Length + WaitTime)
            {
                Destroy(this);
            }
        }
    }
    
}
