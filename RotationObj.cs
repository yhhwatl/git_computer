using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationObj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //如果是围绕自身的x轴进行旋转，就是transform.Rotate(new Vector3(1, 0, 0));
		// 如果是围绕自身的y轴进行旋转，就是transform.Rotate(new Vector3(0, 1, 0));
		//如果是围绕自身的z轴进行旋转，就是transform.Rotate(new Vector3(0, 0, 1));
		transform.Rotate(new Vector3(0, 1, 0));
    }
}
