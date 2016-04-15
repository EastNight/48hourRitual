using UnityEngine;
using System.Collections;

public class Tool{
    public static float originX;
    public static float originY;
	public static Vector3  GetHorizontalPos(Vector3 from){

        var x = 2*originX- from.x ;
        
        
        return new Vector3(x,from.y);
	}

	public static Vector3  GetVerticalPos(Vector3 from, float? y){
        float posy;
        if (y==null)
        {
            posy = 2 * originY - from.y;
        }
        else
        {
            posy = (float)(2 * y - from.y);
        }
        

        return new Vector3(from.x,posy);
	}
    public static Vector3 GetOriginPos(Vector3 from, Vector2? origin)
    {
        var vec = new Vector3();
        if(origin==null)
        {
            vec.x= 2 * originX - from.x;
            vec.y= 2 * originY - from.y;
        }
        else
        {
            vec.x = 2 * ((Vector2)origin).x - from.x;
            vec.y = 2 * ((Vector2)origin).y - from.y;
        }
        return new Vector3(vec.x, vec.y);
    }
}
