using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownGround : MonoBehaviour
{
    [SerializeField]
    bool Check;
    float Speed = 1;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Check)
        {
            transform.position += -transform.up * Time.deltaTime * Speed;
            Speed += 1;
        }
    }

  public void SetCheck()
    {
        Check = true;
    }
}
