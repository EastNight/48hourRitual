using UnityEngine;
using System.Collections;

public class FingerEvent : MonoBehaviour {

	public void OnEnable(){
		// EasyTouch.On_TouchStart += EasyTouch_On_TouchStart; 
		// EasyTouch.On_TouchUp += EasyTouch_On_TouchUp; 
		EasyTouch.On_SimpleTap += EasyTouch_On_SimpleTap; 
		EasyTouch.On_LongTap += EasyTouch_On_LongTap; 
		EasyTouch.On_LongTapEnd += EasyTouch_On_LongTapEnd; 

	}

	public void OnDisable(){
		// EasyTouch.On_TouchStart -= EasyTouch_On_TouchStart; 
		// EasyTouch.On_TouchUp -= EasyTouch_On_TouchUp; 
		EasyTouch.On_SimpleTap -= EasyTouch_On_SimpleTap; 
		EasyTouch.On_LongTap -= EasyTouch_On_LongTap; 
		EasyTouch.On_LongTapEnd -= EasyTouch_On_LongTapEnd; 
	}

	// public void EasyTouch_On_TouchStart(Gesture gesture)  
 //    {  
 //        print("Touch Down Position:" + gesture.position + " "  +Screen.width   + "  " + Screen.height) ;  
 //    }  

	// public void EasyTouch_On_TouchUp(Gesture gesture)  
 //    {  
 //        print("Touch Up Position:" + gesture.position + " "  +Screen.width   + "  " + Screen.height);  
 //    }  

	public void EasyTouch_On_SimpleTap(Gesture gesture)  
    {  
        // print("Simple Tap Position:" + gesture.position + " "  +Screen.width   + "  " + Screen.height) ; 
        if(PlayerMove.Instance.level==0)
        {
            PlayerMove.Instance.startScene.OnClick();
        } 
    	 PlayerMove.Instance.Level1Move(gesture.position);
        GameMain.instance.Tap(GetArea(gesture.position));
    }  

	public void EasyTouch_On_LongTap(Gesture gesture)  
    {  
        // print("Long Tap Position:" + gesture.position + " "  +Screen.width   + "  " + Screen.height) ;  
        if (PlayerMove.Instance.level == 2 || PlayerMove.Instance.level == 3)
		  PlayerMove.Instance.move = true;
        GameMain.instance.LongTap(GetArea(gesture.position));
    }  

    	public void EasyTouch_On_LongTapEnd(Gesture gesture)  
    {  
        if (PlayerMove.Instance.level == 2 || PlayerMove.Instance.level == 3)
		  PlayerMove.Instance.move = false;
    }  

    int GetArea(Vector3 pos){
    	float width = Screen.width;
    	float height  = Screen.height;
    	if(pos.x >= 0 && pos.x <= width/2){
	    	if(pos.y >= 0 && pos.y <= height/2){
	    		return 2;
	    	}
	    	else{
	    		return 1;
	    	}
    	}
    	else{
	    	if(pos.y >= 0 && pos.y <= height/2){
	    		return 3;
	    	}
	    	else{
	    		return 4;
	    	}
    	}
    }
}
