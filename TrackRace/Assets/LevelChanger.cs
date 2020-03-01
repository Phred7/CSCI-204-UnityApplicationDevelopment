using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{

    public Animator levelChanger;
    private string levelToLoadS;
    private int levelToLoad;
    private float auxTime = 0.0f;
    private int podiumSceneName = 1;
    private int trackSceneName = 0;
    private string podiumSceneNameS = "Podium";
    private string trackSceneNameS = "TrackAndField";


    // Update is called once per frame
    void Update()
    {
        if (gameController.instance.getPlayersFinished())
        {
            auxTime += Time.deltaTime;
            if (auxTime >= 5.0f)
            {
                FadeToLevel(podiumSceneName);
            }
        }
    }

    public void TriggerFadeToLevel()
    {
        
    }
    private void FadeToLevel(int LevelName)
    {
        levelToLoad = LevelName;
        levelChanger.SetTrigger("FadeOut");

    }

    /*private void FadeToLevel(string LevelNameS)
    {
        levelToLoadS = LevelNameS;
        levelChanger.SetTrigger("FadeOut");

    }*/

    private void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    /*private void OnFadeCompleteS()
    {
        SceneManager.LoadScene(levelToLoadS);
    }*/
}
