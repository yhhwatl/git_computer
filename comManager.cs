using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class comManager : MonoBehaviour
{
	private bool[] states = {true,true,false,false,false,false,
		false,false,false};//安装状态
	private static comManager ins = null;
	public int finish_cnt = 0;
	private comManager() {
		Debug.Log("comManager ");
	}
	public static comManager getInstance() {
		Debug.Log("ins ",comManager.ins);
		if(null == comManager.ins)
			comManager.ins = (comManager)GameObject.FindObjectOfType(typeof(comManager));
		return comManager.ins;
	}
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void updateState(int idx) {
    	this.states[idx] = true;
    }
    public bool getState(int idx) {
    	return this.states[idx] || false;
    }
    public void check() {
    	// Debug.Log("finish_cnt " + this.finish_cnt);
    	// this.finish_cnt = this.finish_cnt + 1;
    	bool is_finish = true;
    	for(int i=0;i<9;i++){
    		if(this.states[i] == false) {
    			is_finish = false;
    			break;//5728634
    		}
    	}
    	if(is_finish) {
    		GameObject btn_obj = GameObject.Find("btn_finish").gameObject;
    		Image btn = btn_obj.GetComponent<Image>();
    		btn.enabled = true;
    		GameObject txt_obj = GameObject.Find("txt_finish").gameObject;
    		Text txt = txt_obj.GetComponent<Text>();
    		txt.enabled = true;
    	}
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
