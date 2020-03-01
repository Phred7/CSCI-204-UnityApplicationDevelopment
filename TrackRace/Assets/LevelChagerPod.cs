using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChagerPod : MonoBehaviour
{

    public Animator levelChanger;
    private string levelToLoadS;
    private int levelToLoad;
    private float auxTime = 0.0f;
    private int podiumSceneName = 0;
    private int trackSceneName = 1;
    private string podiumSceneNameS = "Podium";
    private string trackSceneNameS = "TrackAndField";


    // Update is called once per frame
    void Update()
    {
        if(podiumController.instance.getTime() > 50.0f)
        {
            levelChanger.SetTrigger("FadeOut");
        }
    }

    public void TriggerFadeToLevel()
    {
        levelChanger.SetTrigger("FadeOut");
    }
    private void FadeToLevel(int LevelName)
    {
        levelToLoad = LevelName;
        levelChanger.SetTrigger("FadeOut");

    }

    private void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }

}

