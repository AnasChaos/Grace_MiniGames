using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class MenuScreen : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject charaterSelectionPanel;
    [SerializeField] string selectedCharater;
    [SerializeField] SceneTransistion sceneTransistion;


    private void Update()
    {
        if(videoPlayer.frame == (long)videoPlayer.frameCount - 1) 
        {
            VideoEnd();
        }
    }
    private void VideoEnd() 
    {
        Debug.Log("Video End");
        SceneManager.LoadScene("Scene1");
    }
    public void StartGame()
    {
        if (PlayerPrefs.HasKey(Global.selectedCharaterKey)) 
        {
            sceneTransistion.StartTransisation(PlayerPrefs.GetString(Global.saveState,"Scene1"));
        }
        else
        {
            charaterSelectionPanel.transform.localScale = Vector3.zero;
            UIManager.instance.ShowPopup(charaterSelectionPanel);
        }
        
       
    }
    public void EndGame()
    {

    }
    public void CharaterSelection(string charaterName) 
    {        
        selectedCharater = charaterName;
        PlayerPrefs.SetString("selectedCharacter" , selectedCharater);
        UIManager.instance.ShowPopup(charaterSelectionPanel,false, ShowVideo);
    }

    private void ShowVideo() 
    {
        sceneTransistion.StartTransisation(SceneManager.GetActiveScene().name);
        menuPanel.SetActive(false);
        videoPlayer.gameObject.SetActive(true);
    }

}
