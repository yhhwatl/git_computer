using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class TransObj : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler,IDropHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update () 
	{
		//Zoom out
		if (Input.GetAxis("Mouse ScrollWheel") <0)
        {
           if(Camera.main.fieldOfView<=100)
           Camera.main.fieldOfView +=2;
           if(Camera.main.orthographicSize<=20)
           Camera.main.orthographicSize +=0.5F;
        }
		//Zoom in
	    if (Input.GetAxis("Mouse ScrollWheel") > 0)
	    {
	      if(Camera.main.fieldOfView>2)
			Camera.main.fieldOfView-=2;
		  if(Camera.main.orthographicSize>=1)
		    Camera.main.orthographicSize-=0.5F;
	    }
	}

    private bool onDrag = false;  //是否被拖拽    
	public float speed = 6f;      //旋转速度    
	private float tempSpeed;      //阻尼速度 
	private float axisX = 1;      //鼠标沿水平方向移动的增量   
	private float axisY = 1;      //鼠标沿竖直方向移动的增量   
	private float cXY;

	
	public void OnBeginDrag(PointerEventData eventData)
	{
		Debug.Log("P:" + gameObject.name + "  OnBeginDrag");
		//接受鼠标按下的事件// 
		axisX = 0f; axisY = 0f;
	}

	public void OnDrag(PointerEventData eventData)
	{
		Debug.Log("P:" + gameObject.name + "  OnDrag");
		onDrag = true;
		axisX = -Input.GetAxis("Mouse X");
		//获得鼠标增量 
		axisY = Input.GetAxis("Mouse Y");
		cXY = Mathf.Sqrt(axisX * axisX + axisY * axisY); //计算鼠标移动的长度//
		if (cXY == 0f) { cXY = 1f; }
		//这个是是按照之前方向一直慢速旋转
		this.transform.Rotate(new Vector3(axisY, axisX, 0) * Rigid(), Space.World); 
	}
	//计算阻尼速度
	float Rigid()      
	{
		if (onDrag)
		{
			tempSpeed = speed;
		}
		else
		{
			if (tempSpeed > 0)
			{
				//通过除以鼠标移动长度实现拖拽越长速度减缓越慢
				tempSpeed -= speed * 2 * Time.deltaTime / cXY; 
			}
			else
			{
				tempSpeed = 0;
			}
		}
		return tempSpeed;
	}

	public void OnDrop(PointerEventData eventData)
	{
		Debug.Log("P:" + gameObject.name + "  OnDrop"+"  frmo:"+eventData.pointerDrag.name);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		Debug.Log("P:" + gameObject.name + "  OnEndDrag");
	}
}
