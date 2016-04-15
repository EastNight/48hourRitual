using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LevelCtrl : MonoBehaviour {

	public int  type = 0;
	[HideInInspector]
	public GameObject tipObject;

	public List<int> leftUp = new List<int>();
	public List<int> leftDown = new List<int>();
	public List<int> rightUp = new List<int>();
	public List<int> rightDown = new List<int>();

	public int mvType1 =  1;
	public RectTransform tarObj1;
	public Vector3 rightPos1;
	public Vector3 rightRotation1;

	public int mvType2 =  2;
	public RectTransform tarObj2;
	public Vector3 rightPos2;
	public Vector3 rightRotation2;


	public int mvType3 =  3;
	public RectTransform tarObj3;
	public Vector3 rightPos3;
	public Vector3 rightRotation3;


	public int mvType4 =  4;
	public RectTransform tarObj4;
	public Vector3 rightPos4;
	public Vector3 rightRotation4;

	[HideInInspector]
	public int level;
	[HideInInspector]
	public int progress;

	public void Init(){
		tipObject = transform.Find("tip").gameObject;
		string [] str = gameObject.name.Split('_');
		level = int.Parse(str[0]) - 1;
		progress = int.Parse(str[1]) - 1;
		gameObject.SetActive(false);
	}

	public List<LevelPiece> GetList(){
		List<LevelPiece> result = new List<LevelPiece>();
		result.Add(new LevelPiece(mvType1,tarObj1,rightPos1,rightRotation1));
		result.Add(new LevelPiece(mvType2,tarObj2,rightPos2,rightRotation2));
		result.Add(new LevelPiece(mvType3,tarObj3,rightPos3,rightRotation3));
		result.Add(new LevelPiece(mvType4,tarObj4,rightPos4,rightRotation4));
		return result;
	}

	public void ShowState(int j , int curProgress){
		tipObject.SetActive(j == curProgress);
	}
}


public class LevelPiece{
	public int mvType;
	public RectTransform tarObj;
	public Vector3 rightPos;
	public Vector3 rightRotation;

	public LevelPiece( int mvType,RectTransform tarObj,Vector3 rightPos,Vector3 rightRotation){
		this.mvType = mvType;
		this.tarObj = tarObj;
		this.rightPos = rightPos;
		this.rightRotation = rightRotation;
	}
}