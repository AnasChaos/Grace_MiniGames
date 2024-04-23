using Newtonsoft.Json;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Users users;
    public UserProfile userProfile;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                if (instance == null)
                {
                    GameObject gameManager = new GameObject(typeof(GameManager).Name);
                    instance = gameManager.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        string users = Resources.Load<TextAsset>("UserData").ToString();
        Debug.Log(users);
        this.users = JsonConvert.DeserializeObject<Users>(users);
        this.userProfile = JsonConvert.DeserializeObject(PlayerPrefs.GetString(Global.userProfile)) as UserProfile;
    }

    public void RegisterUser(UserProfile userProfile) 
    {
        userProfile.id = users.userProfiles.Count + 1;
        userProfile.character = "";
        userProfile.saveState = "Scene1";

        users.userProfiles.Add(userProfile);
        this.userProfile = userProfile;
        string saveData = JsonConvert.SerializeObject(users);
        System.IO.File.WriteAllText("Assets/Resources/UserData.Json", saveData);
    }
    
    public void SaveUserState(string stateUserState) 
    {
        UserProfile userProfile = users.userProfiles.Where(x => x.name == this.userProfile.name).FirstOrDefault();
        userProfile.saveState = stateUserState;
        string saveData = JsonConvert.SerializeObject(users);
        System.IO.File.WriteAllText("Assets/Resources/UserData.Json", saveData);

    }

    public void SaveSelectedCharacter(string selectedCharacter) 
    {
        UserProfile userProfile = users.userProfiles.Where(x => x.name == this.userProfile.name).FirstOrDefault();
        userProfile.character = selectedCharacter;
        string saveData = JsonConvert.SerializeObject(users);
        System.IO.File.WriteAllText("Assets/Resources/UserData.Json", saveData);
    }
    
    public void SaveUserProfile(UserProfile userProfile) 
    {
        string saveData = JsonConvert.SerializeObject(userProfile);
        PlayerPrefs.SetString(Global.userProfile, saveData);
    }

}
