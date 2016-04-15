using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameMain : MonoBehaviour {

	static GameMain _instance;
	public static GameMain instance{
		get{return _instance;}
	}

	public List<List<LevelCtrl>> levelList = new List<List<LevelCtrl>>();

	public AudioClip suc;
	public AudioClip fail;
	public AudioSource adSrc;

	RectTransform parentTrans;
	static GameObject gamePlayObj;
	List<GameObject> bgList = new List<GameObject>();
	List<RectTransform> bgRectTransList = new List<RectTransform>();
	List<Image> bgCoverList = new List<Image>();
	List<LevelPiece> levelPieceList = new List<LevelPiece>();
	Dictionary<int,PicPieceCtrl> picPieceDic = new Dictionary<int ,PicPieceCtrl>();

	[HideInInspector]
	public int curLevel = 0;
	[HideInInspector]
	public int curProgress = 0;
	float progressDistance = 700;
	Transform subTrans;

	SkeletonAnimation sucAnim;
	SkeletonAnimation failAnim;

	static bool _isGameStart;
	public static bool isGameStart{
		get{return _isGameStart;}
		set{
			_isGameStart = value;
			gamePlayObj.SetActive(value);
		}
	}

	void Start() {
		_instance = this;
		parentTrans = transform.Find("scene").GetComponent<RectTransform>();
		subTrans = GameObject.Find("SubtitleCanvas").transform;
		gamePlayObj = subTrans.Find("GamePlay").gameObject;
		sucAnim = Camera.main.transform.Find("suc").GetComponent<SkeletonAnimation>();
		failAnim = Camera.main.transform.Find("fail").GetComponent<SkeletonAnimation>();
		gamePlayObj.SetActive(false);
		for (int i = 1  ; i <= 3 ; i ++){
			bgList.Add(transform.Find("scene/bg_" + i).gameObject);
			bgRectTransList.Add(transform.Find("scene/bg_" + i).GetComponent<RectTransform>());
			bgCoverList.Add(transform.Find("scene/bg_" + i + "/bg_cover").GetComponent<Image>());
		}

		for(int i = 1 ; i <= 9 ; i++){	
			List<LevelCtrl> levelContianer = new List<LevelCtrl>();
			for(int j = 1 ; j <= 9 ; j ++){
				Transform trans = subTrans.Find("GamePlay/" + i + "_" + j);
				if(trans == null) break;
				LevelCtrl lc = trans.GetComponent<LevelCtrl>();
				lc.Init();
				levelContianer.Add(lc);
			}
			levelList.Add(levelContianer);
			// Debug.Log(i + " " + levelContianer.Count);
		}
		curLevel = 0;

	}

	public void MoveToNextLevel(){
		curLevel ++;
	}

	public void PlaySucAnimation(){
		sucAnim.gameObject.SetActive(true);
		sucAnim.Reset();
		sucAnim.AnimationName = "animation";
		adSrc.PlayOneShot(suc);
	}

	public void PlayFailAnimation(){
		failAnim.gameObject.SetActive(true);
		failAnim.Reset();
		failAnim.AnimationName = "animation";
		adSrc.PlayOneShot(fail);
	}

	public void RefreshLevelBg(){
		bgList[0].SetActive(curLevel >= 0 && curLevel < 3);
		bgList[1].SetActive(curLevel >= 3 && curLevel < 6);
		bgList[2].SetActive(curLevel >= 6 && curLevel < 9);
	}

	public bool IsPassLevel(int level){
		return curLevel > (level - 1);
	}

	public bool StartLevel(int level,bool isFirst = true){
		Debug.Log("Start Level " + level);
		if(IsPassLevel(level)) return false;
		curLevel = level - 1;
		curProgress = 0;
		RefreshLevelBg();
		if(curLevel == 9){
			// play to end
			return false;
		}

		Transform levelTxt = subTrans.Find("GamePlay/cover/Text" + level +  "_before");

		if(isFirst && levelTxt != null)
			StartCoroutine(StartLevelIE(levelTxt.gameObject));
		else
			GameStart();

		return true;
	}

 	IEnumerator StartLevelIE(GameObject levelTxtObj){
		isGameStart = true;
 		levelTxtObj.SetActive(true);
 		yield return new WaitForSeconds(2);
 		levelTxtObj.SetActive(false);
		GameStart();
 	}

	public void RefreshLevel(){
		for (int i = 0 ; i < levelList.Count ; i ++){
			for (int j = 0 ; j < levelList[i].Count ; j ++){
				// Debug.Log(i + " " + curLevel + " " + j  + " " + curProgress + " " + isGameStart);
				if (i == curLevel && isGameStart && j <= curProgress){
					levelList[i][j].gameObject.SetActive(true);
					levelList[i][j].ShowState(j , curProgress);
				}
				else{
					levelList[i][j].gameObject.SetActive(false);
				}
			}
		}
	}

	public void RefreshPicPieceCtrl(){
		picPieceDic.Clear();
		Debug.Log("curLevel " + curLevel + " " + curProgress);
	 	List<LevelPiece> pieceList  =  levelList[curLevel][curProgress].GetList();
		for(int i = 1 ; i <= 4 ; i ++) {
			Transform trans = subTrans.Find("GamePlay/" + (curLevel + 1) + "_" + (curProgress + 1) + "/obj" + i);
			if(trans != null){
				picPieceDic[i] = trans.GetComponent<PicPieceCtrl>(); 
				picPieceDic[i].Init(pieceList[i - 1]);
			}
		}
	}

	public void GameStart(){
		isGameStart = true;
		RefreshLevel();
		RefreshPicPieceCtrl();
		levelPieceList = levelList[curLevel][curProgress].GetList();
		bool randomRotate = false;
		if(levelList[curLevel][curProgress].type == 1){
			randomRotate = true;
		}

 		foreach(var pair in picPieceDic){
 			LevelPiece levelPiece = levelPieceList[pair.Key - 1];
 			if(levelPiece.mvType == 1 && levelPiece.tarObj != null){
 				pair.Value.RunCopy(levelPiece.tarObj,1);
 			}
 			if(levelPiece.mvType == 2 && levelPiece.tarObj != null){
 				pair.Value.RunCopy(levelPiece.tarObj,2);
 			}
 			if(levelPiece.mvType == 3 && levelPiece.tarObj != null){
 				pair.Value.RunCopy(levelPiece.tarObj,3);
 			}
 			if(levelPiece.mvType == 4){
 				pair.Value.RunAnim();
 			}
 			if(randomRotate){
 				pair.Value.RandomRotate();
 			}
 		}
	}

	public void StopGame(){
 		foreach(var pair in picPieceDic){
 			pair.Value.StopAnim();
 		}
	}

	public void GotoNext(){
		curProgress ++ ;
		Debug.Log(curProgress + " " + levelList[curLevel].Count);
		if(curProgress >= levelList[curLevel].Count){
			StartCoroutine(ShowSuc());
		}
		else{
			isGameStart = false;
			SetToRightPos();
			// Debug.Log("GotoNext GameStart");
			GameStart();
		}
	}

	IEnumerator ShowSuc(){
		PlaySucAnimation();
		yield return new WaitForSeconds(2);
		Transform levelTxt = subTrans.Find("GamePlay/cover/Text" + (curLevel + 1) +  "_after");
		if(levelTxt != null){
			levelTxt.gameObject.SetActive(true);
			yield return new WaitForSeconds(2);
			levelTxt.gameObject.SetActive(false);
		}
		sucAnim.gameObject.SetActive(false);
		curLevel ++;
		curProgress = 0;
		isGameStart = false;
		RefreshLevel();
		gamePlayObj.SetActive(false);
		PlayerMove.Instance.SetEnable();
	}

	public void SetToRightPos(){
 		foreach(var pair in picPieceDic){
 			pair.Value.SetRight();
 		}
	}

	public void JudgeResult(){
		if(isGameStart == false) return;
		StopGame();
		print("Judge if Lose");
		if(IsWin()){
			Debug.Log("You win");
			GotoNext();
		}
		else{
			StartCoroutine(FailAnim());
		}
	}

	IEnumerator FailAnim(){
		PlayFailAnimation();
		yield return new WaitForSeconds(1);
		failAnim.gameObject.SetActive(false);
		Debug.Log("You lose");
		curProgress = 0;
		StartLevel(curLevel + 1,false);
	}

	public bool IsWin(){
 		foreach(var pair in picPieceDic){
 			LevelPiece levelPiece = levelPieceList[pair.Key - 1];
 			PicPieceCtrl ctrl = pair.Value;

 			Debug.Log(levelPiece.rightPos  + " " + ctrl.rectTrans.anchoredPosition3D);
 			float posDistance = Vector3.Distance(levelPiece.rightPos, ctrl.rectTrans.anchoredPosition3D);
 			float rotateChange = levelPiece.rightRotation.z - ctrl.rectTrans.rotation.eulerAngles.z;
 			rotateChange = Mathf.Min(Mathf.Abs(rotateChange),360 - Mathf.Abs(rotateChange));
			Debug.Log("Change" + pair.Key  + ":" + posDistance + " " + rotateChange);

 			if(Mathf.Abs(posDistance) > GlobalConfig.positionDelta || Mathf.Abs(rotateChange) > GlobalConfig.rotationDelta) {
 				return false;
 			}
 		}
 		return true;
	}

	public void Tap(int type){
		Debug.Log(type);
		if(!isGameStart) return;
		if(curLevel >= levelList.Count) return ;
		if(curProgress >= levelList[curLevel].Count) return;
		LevelCtrl lc = levelList[curLevel][curProgress];
		if(lc.type == 0){
			List<int> stopObjList = null;
			switch(type){
				case 1:
				stopObjList = lc.leftUp;
				break;
				case 2:
				stopObjList = lc.leftDown;
				break;
				case 3:
				stopObjList = lc.rightDown;
				break;
				case 4:
				stopObjList = lc.rightUp;
				break;
			}

			for (int i = 0 ; i < stopObjList.Count; i ++){
				Debug.Log("stop :  " + i + " " + stopObjList[i]);
			}
			bool isAllStop  = true;
			foreach(var pair in picPieceDic){ 
				for (int i = 0 ; i < stopObjList.Count ; i ++){
					if(pair.Key == stopObjList[i]){
						pair.Value.StopAnim();
					}
				}
				if(pair.Value.isStop == false){
					isAllStop = false;
				}
			}

			if(isAllStop){
				Debug.Log("JudgeResult 1");
				JudgeResult();
			}
		}
	}

	public void LongTap(int type){
		if(!isGameStart) return;
		if(curLevel >= levelList.Count) return ;
		if(curProgress >= levelList[curLevel].Count) return;
		LevelCtrl lc = levelList[curLevel][curProgress];
		if(lc.type == 1){
			List<int> stopObjList = null;
			switch(type){
				case 1:
				stopObjList = lc.leftUp;
				break;
				case 2:
				stopObjList = lc.leftDown;
				break;
				case 3:
				stopObjList = lc.rightDown;
				break;
				case 4:
				stopObjList = lc.rightUp;
				break;
			}
			foreach(var pair in picPieceDic){ 
				for (int i = 0 ; i < stopObjList.Count ; i ++){
					if(pair.Key == stopObjList[i]){
						pair.Value.RotateOnce();
					}
				}
			}	
		}
	}

}
