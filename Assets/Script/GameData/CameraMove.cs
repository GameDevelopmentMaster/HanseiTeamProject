using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    Vector3 TagetPos;
    Vector3 StartPos;
    // Start is called before the first frame update
    void Start()
    {
     
        StartPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("PlayerImage") != null)
        {
            TagetPos = new Vector3(GameObject.Find("PlayerImage").transform.position.x, GameObject.Find("PlayerImage").transform.position.y + 2.96f, -101.3f);
            transform.position = Vector3.Lerp(transform.position, TagetPos, Time.deltaTime);
        }


    }
}
