using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageManager : UIManager
{
    public Sprite[] PlayerSprte;
    public GameObject[] EnemyCount;
    public GameObject PlayerCharacter;
    int Stage;

    private void Awake()
    {
      
    }

    private void OnDestroy()
    {
        
    }
    private void OnEnable()
    {
        
    }
    // Start is called before the first frame update
    public void Start()
    {
       
    }

    // Update is called once per frame
   override public void Update()
    {
        switch (SceneName())
        {
            case "MainScene":
                Stage = 0;
                break;
            case "Stage1":
                PlayerCharacter.SetActive(true);
                Stage = 1;
                break;
            case "Stage2":
                Stage = 2;
                break;
            case "Stage3":
                Stage = 3;
                break;
            case "BossScene":
              GameObject.Find("Canvas").transform.GetChild(4).GetComponent<Text>().text = "Stage : Boss";
                break;

        }

        base.Update();
        if(SceneName() == "MainScene" && GameObject.Find("Player") != null)
        {
            GameObject.Find("Player").transform.position = new Vector3(-6.7f, 2.748f,0);
        }
        EnemyCount = GameObject.FindGameObjectsWithTag("Enemy");
        if(EnemyCount.Length != 0)
        {
            SetEnemyCount(EnemyCount.Length.ToString());
            SetStage(Stage.ToString());
        }
        else
        {
            StageEnd();
        }
    }

    public void SceneEnd()
    {
        DontDestroyOnLoad(GameObject.Find("Main Camera"));
        DontDestroyOnLoad(transform.gameObject);
        DontDestroyOnLoad(PlayerCharacter);
        if (Stage < 3)
        {
            PlayerCharacter.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = PlayerSprte[Stage];
        }
        PlayerCharacter.transform.position = new Vector3(-6.7f, 2.748f, 0);
        SceneEnd(Stage+1);
    }
}
