using UnityEngine;
using System.Collections;

public class ColliderEnter : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if(GameMain.isGameStart == false) return;
		Debug.Log("JudgeResult 2 "  + transform.parent.parent.name + " " + gameObject.transform.parent.name + " " + other.transform.parent.name);
		if(IsPassSameObj(gameObject,other.transform.gameObject)) return;
		GameMain.instance.JudgeResult();
	}

	bool IsPassSameObj(GameObject obj1, GameObject obj2){
		LevelCtrl lc1 = obj1.transform.parent.parent.GetComponent<LevelCtrl>();
		LevelCtrl lc2 = obj1.transform.parent.parent.GetComponent<LevelCtrl>();
		int curLevel = GameMain.instance.curLevel;
		int curProgress = GameMain.instance.curProgress;
		bool is1Pass = false;
		if(lc1.level < curLevel) is1Pass = true;
		if(lc1.level == curLevel && lc1.progress < curProgress) is1Pass = true;

		bool is2Pass = false;
		if(lc2.level < curLevel) is2Pass = true;
		if(lc2.level == curLevel && lc2.progress < curProgress) is2Pass = true;
		return is1Pass && is2Pass;
	}
	
}
