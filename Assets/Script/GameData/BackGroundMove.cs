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

        for (int i = 0; i < 6; i+=3)
        {
            BackGround[i].transform.localPosition = new Vector3(BackGround[i].transform.localPosition.x - Time.deltaTime * (Spped+30), BackGround[i].transform.localPosition.y,1f);
            BackGround[i + 1].transform.localPosition = new Vector3(BackGround[i+1].transform.localPosition.x - Time.deltaTime * (Spped+10), BackGround[i+1].transform.localPosition.y,0.5f);
            BackGround[i + 2].transform.localPosition = new Vector3(BackGround[i+2].transform.localPosition.x - Time.deltaTime * Spped, BackGround[i+2].transform.localPosition.y,0.3f);
        }
        for (int i=0; i<6; i++)
        {
            if (BackGround[i].transform.localPosition.x < -17.77f)
            {
                switch (i) {
                    case 0:
                        BackGround[i].transform.localPosition = new Vector3(BackGround[3].transform.position.x+17.74f, 0, 1f);
                        break;
                    case 3:
                        BackGround[i].transform.localPosition = new Vector3(BackGround[0].transform.position.x+17.74f, 0,1f);
                        break;
                    case 1:
                        BackGround[i].transform.localPosition = new Vector3(BackGround[4].transform.position.x +17.74f, 0, 0.5f);
                        break;
                    case 4:
                        BackGround[i].transform.localPosition = new Vector3(BackGround[1].transform.position.x + 17.74f, 0,0.5f);
                        break;
                    case 2:
                        BackGround[i].transform.localPosition = new Vector3(BackGround[5].transform.position.x + 17.74f, 0,0.3f);
                        break;
                    case 5:
                        BackGround[i].transform.localPosition = new Vector3(BackGround[2].transform.position.x + 17.74f, 0,0.3f);
                        break;
                }
                
            }
        }
    }
}
