using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PicPieceCtrl : MonoBehaviour {

	public ColliderEnter colliderEnter;
	public Animator animator;
	public RuntimeAnimatorController animCtrl;
	public RectTransform rectTrans;
	public Image img;
	public RectTransform copyTo;
	public int copyToType;
	public bool isStop = true;
	public LevelPiece lc;

	public void Init(LevelPiece lc){
		this.animator = transform.GetComponent<Animator>();
		this.rectTrans = transform.Find("img").GetComponent<RectTransform>();
		this.img = transform.Find("img").GetComponent<Image>();
		this.colliderEnter = transform.Find("img").GetComponent<ColliderEnter>();
		this.isStop = true;
		this.lc = lc;
	}

	public void RunAnim(){
		Debug.Log("RunAnim : " + gameObject.name + " " + rectTrans.parent.parent.name + " " + rectTrans.parent.name);
		animator.enabled = true;
		animator.Play(gameObject.name , 0,0);
		isStop = false;
	}

	public void StopAnim(){
		animator.enabled = false;
		isStop = true;
	}

	public void RunCopy(RectTransform copyTo ,int copyToType){
		animator.enabled = false;
		this.copyTo = copyTo;
		this.copyToType = copyToType;
		isStop = false;
	}

	public void RotateOnce(){
		rectTrans.Rotate(new Vector3(0,0, GlobalConfig.rotateChange));
	}

	public void SetRight(){
		rectTrans.anchoredPosition3D = lc.rightPos;
		rectTrans.eulerAngles = lc.rightRotation;
		this.isStop = true;
		animator.enabled = false;
	}

	public void JudgeEnd(){
		GameMain.instance.JudgeResult();
	}

	public void RandomRotate(){
		rectTrans.eulerAngles = new Vector3(0, 0, Random.Range(90,270));
	}

	void Update(){
		if(!GameMain.isGameStart)  return;
		switch(this.copyToType){
			case 1:
			rectTrans.anchoredPosition3D = Tool.GetHorizontalPos(copyTo.anchoredPosition3D);
			break;
			case 2:
			rectTrans.anchoredPosition3D = Tool.GetVerticalPos(copyTo.anchoredPosition3D,375);
			break;	
			case 3:
			rectTrans.anchoredPosition3D = Tool.GetOriginPos(copyTo.anchoredPosition3D,new Vector2(0, 375));
			break;
		}
	}

}
