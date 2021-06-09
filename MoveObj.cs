using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveObj : MonoBehaviour
{
    private Vector3 _vec3TargetScreenSpace;// 目标物体的屏幕空间坐标
	private Vector3 _vec3TargetWorldSpace;// 目标物体的世界空间坐标
	private Transform _trans;// 目标物体的空间变换组件
	private Vector3 _vec3MouseScreenSpace;// 鼠标的屏幕空间坐标
	private Vector3 _vec3Offset;// 偏移
	
	private bool isUp = false;
	void Awake( ) { _trans = transform; this.originposy = _trans.localPosition.y;} 
	private float offposy = 0.5f;
	private float originposy = 0.0f;
	IEnumerator OnMouseDown( ) 
		
	{
        Debug.Log("in onmousedonw");
        if(this.idx != 7 && this.idx != 5) {
        	if(this.idx == 6) this.offposy = 2.5f;
	        _trans.localPosition = new Vector3(_trans.localPosition.x,this.offposy + this.originposy,_trans.localPosition.z); 
        }
		// 把目标物体的世界空间坐标转换到它自身的屏幕空间坐标 
		
		_vec3TargetScreenSpace = Camera.main.WorldToScreenPoint(_trans.position);
		// 存储鼠标的屏幕空间坐标（Z值使用目标物体的屏幕空间坐标） 
		
		_vec3MouseScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _vec3TargetScreenSpace.z);
		
		// 计算目标物体与鼠标物体在世界空间中的偏移量 
		
		_vec3Offset = _trans.position - Camera.main.ScreenToWorldPoint(_vec3MouseScreenSpace);
		
		// 鼠标左键按下 
		
		while ( Input.GetMouseButton(0) ) {
            // Debug.Log("getmousebutton");
			// 存储鼠标的屏幕空间坐标（Z值使用目标物体的屏幕空间坐标）
			// float tmpz = _vec3TargetScreenSpace.z;
			// if(Input.GetAxis("Mouse ScrollWheel")) {
			// 	tmpz += Input.GetAxis("Mouse ScrollWheel");
			// }
			_vec3MouseScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _vec3TargetScreenSpace.z);
			
			// 把鼠标的屏幕空间坐标转换到世界空间坐标（Z值使用目标物体的屏幕空间坐标），加上偏移量，以此作为目标物体的世界空间坐标
			
			_vec3TargetWorldSpace = Camera.main.ScreenToWorldPoint(_vec3MouseScreenSpace) + _vec3Offset;              
			
			// 更新目标物体的世界空间坐标 
			
			_trans.position = _vec3TargetWorldSpace;

			this.check();
			// 等待固定更新 
			
			yield return new WaitForFixedUpdate();
		}
		if(!Input.GetMouseButton(0)){
			this.hideTip();
		}
	}
	private Vector3 pos;
	public int idx = 0;
	private float lasttime = 0;
	void Start() {
		this.pos = transform.position;
	}
	void check() {
		
		if(Time.time - this.lasttime > 1.0f) {
			this.lasttime = Time.time;
			Vector3 tpos = transform.position;
			if(tpos != this.pos) {
				this.showTip();
				if(!this.isUp) {
					this.isUp = true;
					// transform.position = new Vector3(tpos.x,tpos.y+1,tpos.z);
				}
			} else {
				this.hideTip();
			}	
			this.pos = tpos;
		}
		
	}
	void showTip() {
		GameObject txt_success = GameObject.Find("txt_success");
    	Text t = txt_success.GetComponent<Text>();
    	t.text = Const.TIPS[this.idx];
	}
	void hideTip() {
	    GameObject txt_success = GameObject.Find("txt_success");
    	Text t = txt_success.GetComponent<Text>();
    	t.text = "";
    }

}
