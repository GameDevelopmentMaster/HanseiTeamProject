using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StageManager : UIManager
{
    public Sprite[] PlayerSprte;
    public List<GameObject> EnemyCount;
    public GameObject PlayerCharacter;
    int Stage;

    private void Start()
    {
        DontDestroyOnLoad(GameObject.Find("Main Camera"));
        DontDestroyOnLoad(transform.gameObject);
        DontDestroyOnLoad(PlayerCharacter);
    }
    
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {       

    }

    public void GameOverButton()
    {
        GameOver();
    }

    public void SceneEnd()
    {
        StartCoroutine(SceneLoadCorutine(Stage+1));
        PlayerCharacter.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = PlayerSprte[Stage];
        if (Stage < 3)
            Stage++;
        PlayerCharacter.transform.GetChild(0).position = new Vector3(-6.7f, 2.748f, 0);
    }

    public void Restart()
    {
        StartCoroutine(SceneLoadCorutine(Stage));

    }
  
    void SceneChangeData()
    {
        GameObject[] games = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < games.Length; i++)
        {
            EnemyCount.Add(games[i]);
        }
        SetEnemyCount(EnemyCount.Count.ToString());
    }

    IEnumerator SceneLoadCorutine(int LoadIndex)
    {
        yield return StartCoroutine(CameraFadeOut());
        AsyncOperation async = SceneManager.LoadSceneAsync(LoadIndex);
        while(!async.isDone)
        {
            yield return null;
        }

        PlayerCharacter.SetActive(true);
        yield return StartCoroutine(CameraFadeIn());
        SceneChangeData();
        
    }

    public void EnemyDestorty(GameObject DestoryData)
    {
        EnemyCount.Remove(DestoryData);
        SetEnemyCount(EnemyCount.Count.ToString());
        if(EnemyCount.Count == 0)
        {
            StageEnd();
        }
    }

}
