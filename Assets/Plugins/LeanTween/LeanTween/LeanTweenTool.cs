using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class LeanTweenTool{

	public static void Rotation(RectTransform rt,float time,Vector3 from,Vector3 to,Action callBackFun = null){
		if(rt != null){
			LeanTween.value(rt.gameObject,delegate(Vector3 obj) {
				rt.rotation = Quaternion.Euler(obj);
			},from,to,time).setOnComplete(delegate(){
				callBackFun();
			});
        }
	}

	public static void AnchoredPosition(RectTransform rt,float time,Vector2 from,Vector2 to,Action callBackFun = null,LeanTweenType easeType = LeanTweenType.notUsed){
		if(rt != null){
			LTDescr ltd = LeanTween.value(rt.gameObject,delegate(Vector3 obj) {
				rt.anchoredPosition3D = obj;
			},from,to,time).setOnComplete(callBackFun);
			ltd.setEase(easeType);
        }
	}

	public static void AnchorMax(RectTransform rt,float time,Vector2 from,Vector2 to,string callBackFunName =""){
		if(rt != null){
			LeanTween.value(rt.gameObject,delegate(Vector3 obj) {
				rt.anchorMax = obj;
			},from,to,time).setOnComplete(delegate(){
			});
        }
	}

	public static void AnchorMin(RectTransform rt,float time,Vector2 from,Vector2 to,string callBackFunName =""){
		if(rt != null){
			LeanTween.value(rt.gameObject,delegate(Vector3 obj) {
				rt.anchorMin = obj;
			},from,to,time).setOnComplete(delegate(){
			});
        }
	}

	public static void Scale(RectTransform rt,float time,Vector3 from,Vector3 to,string callBackFunName =""){
		if(rt != null){
			LeanTween.value(rt.gameObject,delegate(Vector3 obj) {
				rt.localScale = obj;
			},from,to,time).setOnComplete(delegate(){
			});
        }
	}

	public static void Pivot(RectTransform rt,float time,Vector2 from,Vector2 to,string callBackFunName =""){
		if(rt != null){
			LeanTween.value(rt.gameObject,delegate(Vector3 obj) {
				rt.pivot = obj;
			},from,to,time).setOnComplete(delegate(){
			});
        }
	}

	public static void SizeDelta(RectTransform rt,float time,Vector2 from,Vector2 to,string callBackFunName =""){
		if(rt != null){
			LeanTween.value(rt.gameObject,delegate(Vector3 obj) {
				rt.sizeDelta = obj;
			},from,to,time).setOnComplete(delegate(){
			});
        }
	}

	public static void ImageColorAlpha(Image image,float time,float from,float to,string callBackFunName =""){
		if(image != null){
			LeanTween.value(image.gameObject,delegate(float alpha) {
				Color next = new Color(image.color.r,image.color.g,image.color.b,alpha);
				image.color = next;
			},from,to,time).setOnComplete(delegate(){
			});
        }
	}

	public static void TextColorAlpha(Text text,float time,float from,float to,string callBackFunName =""){
		if(text != null){
			LeanTween.value(text.gameObject,delegate(float alpha) {
				Color next = new Color(text.color.r,text.color.g,text.color.b,alpha);
				text.color = next;
			},from,to,time).setOnComplete(delegate(){
			});
		}
	}
	
	public static void LeanTweenCancel(GameObject obj){
		LeanTween.cancel(obj);
	}
}
