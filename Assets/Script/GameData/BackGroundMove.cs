using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour
{
    public GameObject[] BackGround;
    public float Spped;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

       
        
    }

    public void RightMove()
    {
        for (int i = 0; i < 6; i += 3)
        {
            BackGround[i].transform.localPosition = new Vector3(BackGround[i].transform.localPosition.x - Time.deltaTime * (Spped + 30), BackGround[i].transform.localPosition.y, 1f);
            BackGround[i + 1].transform.localPosition = new Vector3(BackGround[i + 1].transform.localPosition.x - Time.deltaTime * (Spped + 10), BackGround[i + 1].transform.localPosition.y, 0.5f);
            BackGround[i + 2].transform.localPosition = new Vector3(BackGround[i + 2].transform.localPosition.x - Time.deltaTime * Spped, BackGround[i + 2].transform.localPosition.y, 0.3f);
        }
        for (int i = 0; i < 6; i++)
        {
            if (BackGround[i].transform.localPosition.x < -17.77f)
            {
                switch (i)
                {
                    case 0:
                    case 3:
                        SetRightBackGround(i, 1);
                        break;
                    case 1:
                    case 4:
                        SetRightBackGround(i, 0.5f);
                        break;
                    case 2:
                    case 5:
                        SetRightBackGround(i, 0.3f);
                        break;
                }

            }
        }

    }

    public void LeftMove()
    {
        for (int i = 0; i < 6; i += 3)
        {
            BackGround[i].transform.localPosition = new Vector3(BackGround[i].transform.localPosition.x - Time.deltaTime * (Spped - 30), BackGround[i].transform.localPosition.y, 1f);
            BackGround[i + 1].transform.localPosition = new Vector3(BackGround[i + 1].transform.localPosition.x - Time.deltaTime * (Spped - 10), BackGround[i + 1].transform.localPosition.y, 0.5f);
            BackGround[i + 2].transform.localPosition = new Vector3(BackGround[i + 2].transform.localPosition.x - Time.deltaTime * Spped, BackGround[i + 2].transform.localPosition.y, 0.3f);
        }
        for (int i = 0; i < 6; i++)
        {
            if (BackGround[i].transform.localPosition.x > 17.66f)
            {
                switch (i)
                {
                    case 0:
                    case 3:
                        SetLeftBackGround(i, 1);
                        break;
                    case 1:
                    case 4:
                        SetLeftBackGround(i, 0.5f);
                        break;
                    case 2:
                    case 5:
                        SetLeftBackGround(i, 0.3f);
                        break;
                }

            }
        }
    }

    void SetRightBackGround(int Value, float PosZ)
    {
        if(Value + 3 <= 5)
        {
            BackGround[Value].transform.localPosition = new Vector3(BackGround[Value + 3].transform.localPosition.x + 17.74f, BackGround[Value].transform.localPosition.y, PosZ);
        }
        else
        {
            BackGround[Value].transform.localPosition = new Vector3(BackGround[Value - 3].transform.localPosition.x + 17.74f, BackGround[Value].transform.localPosition.y, PosZ);
        }
    }
    void SetLeftBackGround(int Value, float PosZ)
    {
        if (Value + 3 <= 5)
        {
            BackGround[Value].transform.localPosition = new Vector3(BackGround[Value + 3].transform.localPosition.x - 17.66f, BackGround[Value].transform.localPosition.y, PosZ);
        }
        else
        {
            BackGround[Value].transform.localPosition = new Vector3(BackGround[Value - 3].transform.localPosition.x - 17.66f, BackGround[Value].transform.localPosition.y, PosZ);
        }
    }
}
