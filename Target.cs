using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
	private int[] colnums = {0,0,0,0};
	private int cunt = 0;
	private bool found = false;
	private bool finish = false;
	private float lasttime = 0;
	// public Vector3 tarpos = new Vector3(-1.81f,0.26f,-1.2f);
	public GameObject[] collobjs = new GameObject[4];
	public float[] pos = new float[3];
	public float[] rota = new float[3];
    private Transform tparent = null;
    public GameObject yuanshen = null;
    public int idx = 0;//
    // Start is called before the first frame update
    void Start()
    {
        MoveObj mb = (MoveObj)GetComponent (typeof(MoveObj));
        this.idx = mb.idx;
        // transform.localRotation = Quaternion.Euler(this.rota[0],this.rota[1],this.rota[2]);
    }

    // Update is called once per frame
    void Update()
    {
    	if(!this.found) return;
        if(this.found) {
        	
        	Debug.Log("lasttime = " + this.lasttime);
        	this.showTips();
        }
        if(this.found && Time.time - this.lasttime > 1.0f) {
        	this.hideTips();
        	Debug.Log("Time.deltaTime = " + Time.time);
        	this.found = false;
        }
    }
   //  void OnTriggerEnter(Collider other){
   // //  	if(other.tag == ("coll_xianka"))
			// // Debug.Log("OnTriggerEnter");   
			// // for(int i=0;i<4;i++) {
   // //  			if(other.name == this.collobjs[i].name) 
			// // 		Debug.Log("OnTriggerEnter");  
   // //  		} 
   //  }
    void OnTriggerStay(Collider other){
    	if(this.finish)return;
    	// if(other.tag == ("coll_xianka")) {
    		for(int i=0;i<4;i++) {
    			if(other.name == this.collobjs[i].name) 
    				this.colnums[i] = 1;
    		}
    		this.cunt = 0;
    		for(int i=0;i<4;i++) {
    			if(this.colnums[i] == 1)
    				this.cunt++;
    		}
    		if(this.cunt >= 4 && !this.found) {
    			Debug.Log("found");
                GameObject txt_success = GameObject.Find("txt_success");
                Text t = txt_success.GetComponent<Text>();
                if(!comManager.getInstance().getState(2) && this.idx != 2) {//CPU 未安装
                    t.text = "请先安装CPU";return;
                }
    			this.cunt = 0;
    			this.finish = true;
    			this.lasttime = Time.time;
    			this.found = true;//找到后设置指定位置
    			// GameObject.Destroy(gameObject);
    			MoveObj mb = (MoveObj)GetComponent (typeof(MoveObj));
    			GameObject.Destroy(mb);//不能移动 
                BoxCollider box = GetComponent<BoxCollider>();
                box.enabled = false;
                // box.isTrigger = false;
    			// Debug.Log("mb "+mb.enabled);
    			// transform.localPosition = this.tarpos;
    		}
    	// }
		Debug.Log("OnTriggerStay " + other.name);   
        for(int i=0;i<4;i++) {
            // if(other.name == this.collobjs[i].name) 
            //     this.colnums[i] = 1;
            Debug.Log("collnums idx " + this.colnums[i] + this.collobjs[i].name);
        } 
    	
    }
    void OnTriggerExit(Collider other){
        if(this.finish)return;
    	// if(other.tag == ("coll_xianka")) {
    		for(int i=0;i<4;i++) {
    			if(other.name == this.collobjs[i].name) 
    				this.colnums[i] = 0;
    		}
			Debug.Log("OnTriggerExit " + other.name);    
    	// }
    }
    void showTips() {
        if(!this.collobjs[0])return;
    	GameObject txt_success = GameObject.Find("txt_success");
    	Text t = txt_success.GetComponent<Text>();
    	t.text = "安装成功";
    	comManager.getInstance().updateState(this.idx);
        comManager.getInstance().check();
        
        this.tparent = this.collobjs[0].transform.parent.parent;
        if(this.yuanshen) {
            this.yuanshen.SetActive(true);
            GameObject.Destroy(this.gameObject);
            // return;
        } else {
            transform.parent = this.tparent;
            transform.localPosition = new Vector3(this.pos[0],this.pos[1],this.pos[2]);
        }
    	
        //不参与碰撞检查
        for(int i=0;i<4;i++) {
            if(this.collobjs[i]) {
                GameObject go = GameObject.Find(this.collobjs[i].name);
                if(go){
                    BoxCollider box = go.GetComponent<BoxCollider>();
                    box.isTrigger = false;
                    // GameObject.Destroy(go);
                    // this.collobjs[i] = null;
                }
            }
        }
    }
    void hideTips() {
	    GameObject txt_success = GameObject.Find("txt_success");
    	Text t = txt_success.GetComponent<Text>();
    	t.text = "";
    }
}
