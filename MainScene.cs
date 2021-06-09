using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button btn_return = this.GetComponent<Button> ();
        if(btn_return)
	        btn_return.onClick.AddListener (() => ButtonClicked());
    }
    void ButtonClicked() {
		SceneManager.LoadScene("main");
    }
}
