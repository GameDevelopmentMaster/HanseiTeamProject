using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int CheckImage;
    public GameObject Nulclaer;
    public GameObject HomingMissile;
    public GameObject CircleBullet;
    public float Speed;
    public float CircleTiming;
    public Transform Pos;
    public GameObject LaserObject;
    CamaraShake camaraShake;

    float BulletTiming;
    float LaserWidth = 0;

    int HomingMissileCount;
    int NulclaerCount;
    int JumpCount;
    bool EneryBoom = false;

    // Start is called before the first frame update
    void Start()
    {
        camaraShake = GameObject.Find("Main Camera").GetComponent<CamaraShake>();
        JumpCount = 1;
        HomingMissileCount = 3;
        BulletTiming = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (BulletTiming > 0.05f && EneryBoom == false)
        {
            for (int i = CheckImage + 3; i < this.transform.parent.childCount; i++)
            {
                if (this.transform.parent.GetChild(i).gameObject.activeSelf == false)
                {
                    this.transform.parent.GetChild(i).position = Pos.position;
                    this.transform.parent.GetChild(i).gameObject.SetActive(true);
                    break;
                }
         
            }
            BulletTiming = 0;
        }


        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += transform.right * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= transform.right * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space) && JumpCount > 0)
        {
            Rigidbody2D Check = GetComponent<Rigidbody2D>();
            Check.AddForce(new Vector2(0, 300));
            JumpCount -= 1;
        }

        

        if (Input.GetKey(KeyCode.R))
        {
            LineRenderer Line = transform.parent.GetComponent<LineRenderer>();
            Vector3[] Linepos = new Vector3[4];
            Linepos[0] = new Vector3(GameObject.Find("Pos").transform.position.x, GameObject.Find("Pos").transform.position.y + LaserWidth, 0);
            Linepos[1] = new Vector3(GameObject.Find("Pos").transform.position.x + 30, GameObject.Find("Pos").transform.position.y+LaserWidth, 0);
            Linepos[2] = new Vector3(GameObject.Find("Pos").transform.position.x + 30, GameObject.Find("Pos").transform.position.y- LaserWidth, 0);
            Linepos[3] = new Vector3(GameObject.Find("Pos").transform.position.x, GameObject.Find("Pos").transform.position.y-LaserWidth, 0);
            Line.SetPositions(Linepos);
            Line.enabled = true;

            if (LaserWidth < 0.5f)
                LaserWidth += 0.3f * Time.deltaTime;
            EneryBoom = true;
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            camaraShake.ShakeStart = true;
            PhysicsLaser(transform.parent.GetComponent<LineRenderer>());
            //LaserObject.SetActive(true);
        }
        else
        {
            LineRenderer Line = transform.parent.GetComponent<LineRenderer>();
            Line.enabled = false;
            LaserWidth = 0;
            EneryBoom = false;
        }

        if (Input.GetKeyDown(KeyCode.W) && GameObject.Find("HomingMissile") == null)
        {
            for (int i = 0; i < HomingMissileCount; i++)
            {
                GameObject A = Instantiate(HomingMissile, Pos.position, Quaternion.identity);
                A.name = "HomingMissile";
            }

        }
        if (Input.GetKeyDown(KeyCode.Z) && NulclaerCount > 0)
        {

        }

        if (Input.GetKeyDown(KeyCode.Q) && CircleTiming > 0.5f && CircleBullet.activeSelf == false)
        {
            CircleBullet.transform.position = Pos.position;
            CircleBullet.SetActive(true);
            CircleTiming = 0;
        }

        BulletTiming += Time.deltaTime;
        CircleTiming += Time.deltaTime;
    }

    private void OnDestroy()
    {
        if (transform.parent.GetChild(1).tag != "Player")
        {
            Destroy(transform.parent.gameObject);
        }
        else
        {
            transform.parent.GetChild(1).gameObject.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            JumpCount = 1;
        }
    }

    private void OnDrawGizmos()
    {

        Vector3 Size = Dir(transform.parent.GetComponent<LineRenderer>().GetPosition(0), transform.parent.GetComponent<LineRenderer>().GetPosition(2));
        Vector3 Center = (transform.parent.GetComponent<LineRenderer>().GetPosition(0) + transform.parent.GetComponent<LineRenderer>().GetPosition(2))/2;
        Gizmos.DrawCube(Center, Size);
    }


    Vector3 Dir(Vector3 A, Vector3 B)
    {
        Vector3 C = new Vector3(Mathf.Abs(A.x) + Mathf.Abs(B.x), Mathf.Abs(A.y) + Mathf.Abs(B.y));
        return C;
    }

   public void PhysicsLaser(LineRenderer PhysicsLaser)
    {
        Vector2 Size = Dir(transform.parent.GetComponent<LineRenderer>().GetPosition(0), transform.parent.GetComponent<LineRenderer>().GetPosition(2));
        GameObject Laser = Instantiate(LaserObject);
        Laser.GetComponent<TrailRenderer>().startWidth = Laser.GetComponent<TrailRenderer>().endWidth = Size.y;
        RaycastHit2D[] hit = Physics2D.BoxCastAll(PhysicsLaser.GetPosition(0), Size ,0,  PhysicsLaser.GetPosition(2));
        for (int i=0; i<hit.Length; i++)
        {
            if (hit[i])
            {
                switch (hit[i].transform.gameObject.tag)
                {
                    case "Bullet":
                        if (hit[i].transform.GetComponent<Bullet>().Character == Bullet.CharacterList.EnemyCharacter)
                        {
                            hit[i].transform.gameObject.SetActive(false);
                        }
                        break;
                    case "Enemy":
                        Destroy(hit[i].transform.gameObject);
                        break;
                    default:
                        break;
                }
            }
        }
       
    }
}

