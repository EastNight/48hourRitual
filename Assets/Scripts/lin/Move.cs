using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
    	public GameObject[] roadSign;
    	public float speed;

	int signNum=0;
	void Update () {
		if(Input.GetKey(KeyCode.Space)){
		            if(signNum<roadSign.Length-1){
		            }
		}
	}
}
