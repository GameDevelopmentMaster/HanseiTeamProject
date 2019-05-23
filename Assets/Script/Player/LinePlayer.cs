using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePlayer : MonoBehaviour
{

    private void OnEnable()
    {
        transform.position = GameObject.Find("Pos").transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(1.5f, 0));
    }

    public static void Laser()
    {
        
    }

}
