using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class changeScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button btn = this.GetComponent<Button> ();
		btn.onClick.AddListener (OnClick);
    }

    public void OnClick()
    {
			Debug.Log("P:" + gameObject.name + "  OnClick");
			SceneManager.LoadScene("preview");
        
    }
}
