using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
    public GameObject[] roadSign;
    public float speed;

    int signNum=0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKey(KeyCode.Space))
        {
            if(signNum<roadSign.Length-1)
            {

            }
        }
	}
}
