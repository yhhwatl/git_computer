using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BtnManager : MonoBehaviour
{
	public Button[] btns = new Button[11];
	private GameObject lastIns = null;
    void Start()
    {
        for(int i=0;i<11;i++) {
            GameObject objText = GameObject.Find("Text"+i);
            Text t = objText.GetComponent<Text>();
            t.text = Const.BTNNAMES[i];

        }
        this.btns[0].onClick.AddListener (() => ButtonClicked(0));
        this.btns[1].onClick.AddListener (() => ButtonClicked(1));
        this.btns[2].onClick.AddListener (() => ButtonClicked(2));
        this.btns[3].onClick.AddListener (() => ButtonClicked(3));
        this.btns[4].onClick.AddListener (() => ButtonClicked(4));
        this.btns[5].onClick.AddListener (() => ButtonClicked(5));
        this.btns[6].onClick.AddListener (() => ButtonClicked(6));
        this.btns[7].onClick.AddListener (() => ButtonClicked(7));
        this.btns[8].onClick.AddListener (() => ButtonClicked(8));
        this.btns[9].onClick.AddListener (() => ButtonClicked(9));
        this.btns[10].onClick.AddListener (() => ButtonClicked(10));


        this.ButtonClicked(0);
    }
    void ButtonClicked(int idx) {
		Debug.Log("ButtonClicked " + idx);
		if(this.lastIns) {
			GameObject.Destroy(this.lastIns);
			this.lastIns = null;
		}
		this.loadPrefab(idx);
		GameObject objText = GameObject.Find("Text");
    	Text t = objText.GetComponent<Text>();
    	t.text = this.cleanString(Const.DESCS[idx]);
    }
    void loadPrefab(int idx) {
    	GameObject obj = Resources.Load(Const.PBNAMES[idx]) as GameObject;
    	GameObject ins = (GameObject)Instantiate(obj);
    	this.lastIns = ins;
        ins.gameObject.name = Const.TOUCHOBJ;
    	ins.transform.parent = GameObject.Find("root").transform;
        ins.gameObject.layer = 8;
        
        float tposz = -5.678f;
        float tscalz = 0.3f;
        if(idx == 3) {//显卡
            tposz = -3.0f;
        } else if(idx == 4 || idx == 5) {
            ins.transform.localScale = new Vector3(tscalz,tscalz,tscalz);
        } else if(idx == 6) {
            ins.transform.localScale = new Vector3(1,1,1);
        } else if(idx == 8) {
            ins.transform.localScale = new Vector3(1,1,1);
            tposz = -5.88f;
        } else if(idx == 10) {
            ins.transform.localScale = new Vector3(3,3,3);
        }
        if(idx == 0) {
        	ins.transform.localPosition = new Vector3(-0.22f,0.28f,2.94f);
        } else if(idx == 7) {
        	ins.transform.localScale = new Vector3(tscalz/2,tscalz/2,tscalz/2);
            ins.transform.localPosition = new Vector3(-0.02f,-0.011f,tposz);
        } else if(idx == 8) {
            ins.transform.localPosition = new Vector3(-0.11f,-0.011f,tposz);
        } 
        else {
            ins.transform.localPosition = new Vector3(-0.052f,-0.011f,tposz);
        }
        // int childcnt = ins.transform.childCount;
        // for(int i=0;i<childcnt;i++) {
        //     GameObject chid = ins.transform.GetChild(i).gameObject;
        //     chid.gameObject.layer = 8;
        //     this.setLayerName(chid.transform);
        // }
        this.setLayerName(ins.transform);
    }
    void setLayerName(Transform transform) {
        int childcnt = transform.childCount;
        for(int i=0;i<childcnt;i++) {
            GameObject chid = transform.GetChild(i).gameObject;
            chid.gameObject.layer = 8;
            this.setLayerName(chid.transform);
        }
    }
    private string cleanString(string newStr)
    {
        string tempStr = newStr.Replace(" ", "");
        tempStr = tempStr.Replace("\n", "");
        return tempStr = tempStr.Replace("\r", "");
    }
}
