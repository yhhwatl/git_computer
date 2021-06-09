using UnityEngine;
using System.Collections;
using System.Reflection;
public class TouchObj : MonoBehaviour
{


    private float fingerActionSensitivity = Screen.width * 0.0005f; //手指动作的敏感度，这里设定为 二十分之一的屏幕宽度.
                                                                  //
    private float fingerBeginX;
    private float fingerBeginY;
    private float fingerCurrentX;
    private float fingerCurrentY;
    private float fingerSegmentX;
    private float fingerSegmentY;
    //
    private int fingerTouchState;
    //
    private int FINGER_STATE_NULL = 0;
    private int FINGER_STATE_TOUCH = 1;
    private int FINGER_STATE_ADD = 2;
    private GameObject touchObj = null;
    private Transform target;
    private float offx = 0;
    private float offy = 0;
    private float offz = 0;
    private bool inited = false;
    private int cha = 0;
    private float moveOff = 3.0f;
    private float lastx = 0;
    private float lasty = 0;
    private float lastz = 0;

    // Use this for initialization
    void Start()
    {
        fingerActionSensitivity = Screen.width * 0.0005f;

        fingerBeginX = 0;
        fingerBeginY = 0;
        fingerCurrentX = 0;
        fingerCurrentY = 0;
        fingerSegmentX = 0;
        fingerSegmentY = 0;

        fingerTouchState = FINGER_STATE_NULL;
        
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            if (fingerTouchState == FINGER_STATE_NULL)
            {
                fingerTouchState = FINGER_STATE_TOUCH;
                fingerBeginX = Input.mousePosition.x;
                fingerBeginY = Input.mousePosition.y;
            }

        }

        if (fingerTouchState == FINGER_STATE_TOUCH)
        {
            fingerCurrentX = Input.mousePosition.x;
            fingerCurrentY = Input.mousePosition.y;
            fingerSegmentX = fingerCurrentX - fingerBeginX;
            fingerSegmentY = fingerCurrentY - fingerBeginY;

        }


        if (fingerTouchState == FINGER_STATE_TOUCH)
        {
            float fingerDistance = fingerSegmentX * fingerSegmentX + fingerSegmentY * fingerSegmentY;

            if (fingerDistance > (fingerActionSensitivity * fingerActionSensitivity))
            {
                toAddFingerAction();
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            fingerTouchState = FINGER_STATE_NULL;
        }
        this.touchObj = GameObject.Find(Const.TOUCHOBJ);
        if(this.touchObj){
            this.target = this.touchObj.transform;
            if(!this.inited) {
                // this.target.localRotation = Quaternion.Euler(0,0,0);
                float offx = this.touchObj.transform.eulerAngles.x;
                float offz = this.touchObj.transform.eulerAngles.z;
                float offy = this.touchObj.transform.eulerAngles.y;
                this.lastx = offx;
                this.lasty = offy;
                this.lastz = offz;
                this.inited = true;
            }
            // this.target.Rotate(2, 0, 0, Space.Self);
            // StartCoroutine(ChangeRotation());
        }
    }

    private void toAddFingerAction()
    {
        // if(++this.cha<10)return;
        // // fingerTouchState = FINGER_STATE_ADD;
        // this.cha = 0;
        if (Mathf.Abs(fingerSegmentX) > Mathf.Abs(fingerSegmentY))
        {
            fingerSegmentY = 0;
        }
        else
        {
            fingerSegmentX = 0;
        }
        // this.touchObj = GameObject.Find(Const.TOUCHOBJ);
        // this.target = this.touchObj.transform;
        // target.localEulerAngles = new Vector3(0, 0, 90);
        
        this.offx = this.offy = 0;
        if (fingerSegmentX == 0)
        {
            if (fingerSegmentY > 0)
            {
                Debug.Log("up");
                // offx+=this.moveOff;
                this.offy = this.moveOff;
            }
            else
            {
                Debug.Log("down");
                // offx-=this.moveOff;
                this.offy = -this.moveOff;
            }
        }
        else if (fingerSegmentY == 0)
        {
            if (fingerSegmentX > 0)
            {
                Debug.Log("right");
                // offz+=this.moveOff;
                this.offx = this.moveOff;
            }
            else
            {
                Debug.Log("left");
                // offz-=this.moveOff;
                this.offx = -this.moveOff;
            }
        }
        // float x = this.touchObj.transform.localRotation.x;
        // float z = this.touchObj.transform.localRotation.z;
        Debug.Log("touchObj rotation x " + this.lastx);
        Debug.Log("touchObj rotation y " + this.lasty);
        Debug.Log("touchObj rotation z " + this.lastz);
        // this.lastx = offx;
        // this.lasty = offy;
        // this.lastz = offz;
        

        // if (offx < -360) offx = 360;
        // if (offz > 360) offz  = -360;
        // if (offx>=-1 && offx<=1) offx = 360;
        // if (offz>=-1 && offz<=1) offz = 360;
        // this.touchObj.transform.localRotation = Quaternion.Euler(offx, offy, offz);
        // GameObject rawimg = getComponent<RawImage>().GameObject;
        // RenderTexture.ReleaseTemporary(rawimg.texture);
        // var original = ShowRotationLikeInspector(target);
        // original.x = offx;original.y = offy;original.z = offz;
        // SetRotationLikeInspector(target, original);
        // rotationObj(target,this.offx,this.offy);
        // if(this.cha++ >= 100) {
            // this.cha = 0;
            if(this.offy != 0) {
                this.lasty -= this.offy;
                // float toff = offy - this.offy;
                // Vector3 tar = new Vector3(offx,toff,offz);
                // this.target.transform.Rotate(tar);
            }
            if(this.offx != 0) {
                this.lastx -= this.offx;
                // float toff = offx - this.offx;
                // Vector3 tar = new Vector3(toff,offy,offz);
                // this.target.transform.Rotate(tar);
            }
        // }
        this.target.transform.Rotate(this.offx,this.offy,0);
        
    }
    private IEnumerator ChangeRotation()
    {
        yield return new WaitForSeconds(1f);
        if(target != null) {
            var original = ShowRotationLikeInspector(target);
            original.x += this.offx;original.y += this.offy;
            SetRotationLikeInspector(target, original);
            this.offx = this.offy = 0;
        };
        
    }
    public Vector3 ShowRotationLikeInspector(Transform t)
    {
        var type = t.GetType();
        var mi = type.GetMethod("GetLocalEulerAngles", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        var rotationOrderPro = type.GetProperty("rotationOrder", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        var rotationOrder = rotationOrderPro.GetValue(t, null);
        var EulerAnglesInspector = mi.Invoke(t, new[] {rotationOrder});
        return (Vector3) EulerAnglesInspector;
    }

    public void SetRotationLikeInspector(Transform t, Vector3 v)
    {
        var type = t.GetType();
        var mi = type.GetMethod("SetLocalEulerAngles", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        var rotationOrderPro = type.GetProperty("rotationOrder", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        var rotationOrder = rotationOrderPro.GetValue(t, null);
        mi.Invoke(t, new[] {v, rotationOrder});
    }

    private float radius = 1080;
    private Vector3 originalDir = new Vector3(0f,0f,1080f);
    private Vector3 CenterPos = new Vector3(0, 0, 0);
    private Vector2 startPos;
    private Vector2 tempPos;
    private Vector3 tempVec;
    private Vector3 normalAxis;
    private float angle;
    void rotationObj(Transform t,float tempX,float tempY) {
        // float tempX = tempPos.x - startPos.x;

        // float tempY = tempPos.y - startPos.y;

        //tempPos = Input.GetTouch(0).deltaPosition;
        //float tempX = tempPos.x;

        //float tempY = tempPos.y;

        float tempZ = Mathf.Sqrt(radius * radius - tempX * tempX - tempY * tempY);

        tempVec = new Vector3(tempX, tempY, tempZ);

        angle = Mathf.Acos(Vector3.Dot(originalDir.normalized, tempVec.normalized)) * Mathf.Rad2Deg;

        normalAxis = getNormal(CenterPos, originalDir, tempVec);

        t.rotation = Quaternion.AngleAxis(2 *angle, normalAxis);
    }
    private Vector3 getNormal(Vector3 p1,Vector3 p2,Vector3 p3)
    {
        float a = ((p2.y - p1.y) * (p3.z - p1.z) - (p2.z - p1.z) * (p3.y - p1.y));

        float b = ((p2.z - p1.z) * (p3.x - p1.x) - (p2.x - p1.x) * (p3.z - p1.z));

        float c = ((p2.x - p1.x) * (p3.y - p1.y) - (p2.y - p1.y) * (p3.x - p1.x));
        //a对应的屏幕的垂直方向，b对应的屏幕的水平方向。
        return new Vector3(a, -b, c);
    }
}