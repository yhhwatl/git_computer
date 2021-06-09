using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class finish : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    	Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener (() => ButtonClicked());
    }

    void ButtonClicked()
    {
        SceneManager.LoadScene("finish");
    }
}
