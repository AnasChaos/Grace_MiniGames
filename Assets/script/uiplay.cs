using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class uiplay : MonoBehaviour
{
    // Start is called before the first frame update
    Soundmanager Sou;
    public bool ui = false;
    public GameObject pp;
    public GameObject log;
    int panels;

    void Start()
    {
        Sou = Soundmanager.instance;
        if(ui == true)
        {
            panels = PlayerPrefs.GetInt("panelopen");
            if(panels ==1)
            {
                pp.SetActive(true);
                log.SetActive(false);
            }
        }
    }
    public void ppopen()
    {
        PlayerPrefs.SetInt("panelopen",1);
        pp.SetActive(true);
        log.SetActive(false);
    }
    public void ppclose()
    {
        PlayerPrefs.SetInt("panelopen", 0);
        pp.SetActive(false);
        log.SetActive(true);
    }
    public void quits()
    {
        Application.Quit();
    }



    public void Restart()
    {
        // Get the current scene's build index and reload it
        int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneBuildIndex);
    }


    public void tower()
    {
        Sou.Play("click");
        SceneManager.LoadScene("tower");

    }
    public void Cellar_Game()
    {
        Sou.Play("click");
        SceneManager.LoadScene("Cellar Game");
    }

    public void Speller()
    {
        Sou.Play("click");
        SceneManager.LoadScene("Notes Speller");
    }

    public void chest()
    {
        Sou.Play("click");
        SceneManager.LoadScene("chestgame");
    }


    public void Door()
    {
        Sou.Play("click");
        SceneManager.LoadScene("Cell Door");
    }


    public void back()
    {
        Sou.Play("click");
        SceneManager.LoadScene("mainui");
    }

    public void scale_game()
    {
        Sou.Play("click");
        SceneManager.LoadScene("scale_game");
    }
    public void clicksound()
    {
        Sou.Play("click");
    }
    public void matchgame()
    {
        Sou.Play("click");
        SceneManager.LoadScene("matchgame");
    }
    public void plays(string map)
    {
        Sou.Play("click");
        SceneManager.LoadScene(map);
    }
}
