using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePlayer : MonoBehaviour
{
    Dir dir;

    private void OnEnable()
    {
        if (GameObject.Find("Player").transform.GetChild(0).GetComponent<SpriteRenderer>().flipX)
        {
            dir = Dir.Left;
            transform.position = GameObject.Find("Player").transform.GetChild(0).transform.position - transform.right;
        }
        else
        {
            dir = Dir.Right;
            transform.position = GameObject.Find("Player").transform.GetChild(0).transform.position + transform.right;    
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        switch (dir)
        {
            case Dir.Right:
                transform.Translate(new Vector2(1.5f, 0));
                break;
            case Dir.Left:
                transform.Translate(new Vector2(-1.5f, 0));
                break;

        }

        
    }

    public static void Laser()
    {
        
    }

}
