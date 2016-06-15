using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour {
    public GameObject[] playerObjList;
    public GameObject[] roadSign0;
    public GameObject[] roadSign1;
    public GameObject[] roadSign2;
    public GameObject[] roadSign3;
    public GameObject[] roadSign4;
    public Sprite[] playerFigure;
    public Camera camera;
    public float[] LevelY;
    public GameObject[] BG;
    public StartScene startScene;
    public Trigger trigger;
    public SkeletonAnimation startSA;

    public static PlayerMove Instance;
    public float[] Speed;
    float speed;
    public int level;
    public float accessDistance;
    public float verticleSpeed;
    public BGMove bgmove;
    public GameObject target;

    public int SignNow { get { return signNow; }
        set { signNow = value; }
    }
   public int signNow=0;
    public bool move = false;
   public Vector3 destination;
    float testTime = 0;

    void Start () {
        Instance = this;
        SetLevel(level);
    }
    void Update () {
        if (move)
        {
            MoveSeveralRoad();
        }
        testTime += Time.deltaTime;
    }

    public void Level1Move(Vector3 pos)
    {
        if(level != 1) return;
        GameObject[] roadSign = GetRoadSign();
        {
            int bias = 1;
            Vector3 hitpos = camera.ScreenToWorldPoint(pos);
            float min = float.PositiveInfinity;
            int minSign = 0;
            float distance = 0;
            for (int i = 0; i < roadSign.Length - 1; i++)
            {

                if (roadSign[i].transform.position.y <= hitpos.y && hitpos.y <= roadSign[i + 1].transform.position.y)
                {
                    if (Vector3.Distance(hitpos, roadSign[i].transform.position) > Vector3.Distance(hitpos, roadSign[i + 1].transform.position))
                    {
                        minSign = i + 1;
                        bias = -1;
                    }
                    else
                    {
                        minSign = i;
                        bias = 1;
                    }

                    break;
                }
            }
            var ratio = Mathf.Abs(
                (roadSign[minSign].transform.position.y - hitpos.y) / (roadSign[minSign].transform.position.y - roadSign[minSign + bias].transform.position.y));
            var x = roadSign[minSign].transform.position.x + (roadSign[minSign + bias].transform.position.x - roadSign[minSign].transform.position.x) * ratio;
            float y = hitpos.y;
            if (y < roadSign[0].transform.position.y)
            {
                return;
            }
            if (y > roadSign[roadSign.Length - 1].transform.position.y)
            {
                return;
            }
            destination = new Vector3(x, y);
            move = true;
            target.transform.position = new Vector3(destination.x, destination.y, 0);
        }
    }
    public void MoveSeveralRoad()
    {
        GameObject[] roadSign = GetRoadSign();

        if((transform.position.y>destination.y) && (transform.position.y>roadSign[SignNow].transform.position.y))
        {
            GameObjectMove(roadSign,SignNow+1,SignNow);
            
        }
        else if((transform.position.y < destination.y)   && (transform.position.y > roadSign[SignNow].transform.position.y) )
        {
            GameObjectMove(roadSign, SignNow, SignNow+1); 
        }
        else if((transform.position.y - destination.y) > 0 && (transform.position.y < roadSign[SignNow].transform.position.y) )
        {
            GameObjectMove(roadSign, SignNow, SignNow - 1); 
        }
        else if((transform.position.y < destination.y) && (transform.position.y < roadSign[SignNow].transform.position.y) )
        {
            GameObjectMove(roadSign, SignNow -1, SignNow);
        }
        else if (transform.position.y == roadSign[SignNow].transform.position.y && transform.position.y < destination.y)
        {
            GameObjectMove(roadSign, SignNow, SignNow + 1);
            
        }
        else if (transform.position.y == roadSign[SignNow].transform.position.y && transform.position.y > destination.y)
        {
            GameObjectMove(roadSign, SignNow, SignNow-1);
            
        }

    }
    void GameObjectMove(GameObject[] roadSign,int from ,int to)
    {
        Vector3 direction=Vector3.zero;
        if(from==-1 ||to==-1)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f);
            if(from==-1)
            {
                from = to + 1;
            }
            if(to==-1)
            {
                to = from + 1;
            }
        }
        Vector3 posNow=transform.position;
        direction = roadSign[to].transform.position - roadSign[from].transform.position;
        direction = Vector3.Normalize(direction);
        
        verticleSpeed = (direction * speed).y;
        bgmove.VerticleMove(verticleSpeed,level);
        transform.Translate(direction * speed * Time.deltaTime);
        if (direction.x > 0)
        {
            if(level == 2)
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            else
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        else if (direction.x < 0)
        {
            if(level == 2)
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            else
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        if ((transform.position.y - destination.y) * (posNow.y - destination.y) < 0)
        {
            transform.position = destination;
            move = false;
            return;
        }
        if ((transform.position.y - roadSign[to].transform.position.y)*(transform.position.y - roadSign[from].transform.position.y) >0 
            && (posNow.y - roadSign[to].transform.position.y)*(posNow.y - roadSign[from].transform.position.y)<0)
        {
            Turn();
            SignNow = to;
            transform.position = roadSign[to].transform.position;
        }
    }
    void Turn()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    GameObject[] GetRoadSign()
    {
        switch (level)
        {
            case 0:
            return roadSign0;
            case 1:
            return  roadSign1;
            case 2:
            return roadSign2;
            case 3:
            return roadSign3;
            case 4:
            return roadSign4;
            default:
            Debug.LogWarning("Moving error: input level error");
            return null;
        }
    }
    int index;
    void PlayerShow()
    {
        playerObjList[index].SetActive(true);
    }
    public void SetLevel(int level)
    {
        for(int i = 0 ; i <playerObjList.Length ; i ++){
            
            playerObjList[i].SetActive(false);
            if (i == level)
            {
                index = i;
                Invoke("PlayerShow", 0.3f);
            }
        }
        this.level = level;
        SignNow = 0;
        move = false;
        speed=Speed[level];
        var roadSign = GetRoadSign();
        transform.position=roadSign[0].transform.position;
        if(level==0)
        {
            StartScene ss = camera.gameObject.AddComponent<StartScene>();
            startScene = ss;
            ss.rectTrans = transform.GetComponent<RectTransform>();
            camera.orthographicSize = 750;
            Invoke("CamerPosUp", 0.1f);
        }

        if(level==2||level ==3)
        {
            destination = roadSign[roadSign.Length - 1].transform.position;
        }
        if(level==4)
        {
            camera.gameObject.GetComponent<EndScene>().enabled=true;

        }
        camera.transform.position =new Vector3(0, LevelY[level]);
        for(int i=0;i<BG.Length;i++)
        {
            if(level==i)
            {
                BG[i].SetActive(true);
            }
            else
            {
                BG[i].SetActive(false);
            }
        }
    }
    void CamerPosUp()
    {
        camera.transform.position = new Vector3(0, 250, 0);
    }

    public void SetDisable()
    {
        Instance.enabled = false;
    }
    public void SetEnable()
    {
        Instance.enabled = true;
        if(trigger!=null && trigger.duankai !=null)
        {
            trigger.duankai.SetActive(false);
        }

    }
    void ChangePlayerFigure()
    {
        transform.GetComponent<Image>().sprite=playerFigure[level];
    }
}
