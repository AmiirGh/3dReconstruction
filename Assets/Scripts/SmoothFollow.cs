using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour {
    
    public GameObject target;
    public Vector3 cameraOffset;

    bool doRotate = true;
    
    void start () {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update () {
        this.transform.position = target.transform.position + cameraOffset;

        if (doRotate)
            this.transform.eulerAngles = new Vector3(90, 0, -target.transform.eulerAngles.y);
       
    }
}