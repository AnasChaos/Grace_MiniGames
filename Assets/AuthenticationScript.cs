using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;
using Newtonsoft.Json;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class AuthenticationScript : MonoBehaviour
{
    [SerializeField] GameObject options;
    
    [Header("SIGN IN")]
    [SerializeField] GameObject signInHolder;
    [SerializeField] Text userName;
    [SerializeField] InputField password;
    [SerializeField] Button submitBtn;
    [SerializeField] Button createNewBtn;


    [Space(10)]
    [Header("SIGN UP")]
    [SerializeField] GameObject signUpHolder;
    [SerializeField] Text signUpUserName;
    [SerializeField] InputField signUpPass;
    [SerializeField] InputField signUpRePass;
    [SerializeField] Button signUpSubmit;
    [SerializeField] Button signUpCancel;


    private void Start()
    {
        submitBtn.onClick.AddListener(() => SignIn());
        signUpSubmit.onClick.AddListener(() => SignUp());
        signUpCancel.onClick.AddListener(() => ShowSignUpPanel(false));
        createNewBtn.onClick.AddListener(() => ShowSignUpPanel(true));


    }
    void SignIn()
    {
        UserProfile userProfile = GameManager.instance.users.userProfiles.Where(x => x.name == userName.text).FirstOrDefault();

        if (userProfile != null)
        {
            if(password.text != string.Empty) 
            {
                Debug.Log($"{password.text} == {userProfile.password}");
                if(password.text == userProfile.password)
                {
                    MesgBar.instance.show("Login Successfully");

                    UIManager.instance.PanelSwtiching(this.gameObject, options, 0.5f);
                    UIManager.instance.SaveUserProfile(userProfile);
                }
                else 
                {
                    MesgBar.instance.show("Password Incorrect..!!",true);
                }
            }
            else 
            {
                MesgBar.instance.show("Please Enter Password Please!!!", true);
            }
        }
        else 
        {
            MesgBar.instance.show("User Doesn't Exsist", true);        
        }


    }

    void SignUp() 
    {
        UserProfile userProfile = GameManager.instance.users.userProfiles.Where(x => x.name == signUpUserName.text).FirstOrDefault();
        
        if(userProfile == null) 
        {   
            if(signUpUserName.text == string.Empty) 
            {
                MesgBar.instance.show("Please Enter User Name", true);
            }
            else if(signUpPass.text == string.Empty || signUpRePass.text == string.Empty) 
            {
                MesgBar.instance.show("Please Enter Password",true);
            }
            else 
            {
                if(signUpPass.text == signUpRePass.text) 
                {
                    userProfile = new UserProfile();
                    userProfile.name = signUpUserName.text;
                    userProfile.password = signUpPass.text;
                    UIManager.instance.RegisterUser(userProfile);
                    ShowSignUpPanel(false);
                    MesgBar.instance.show("User Has been created successfully you can login with your create credential");
                }
                else 
                {
                    MesgBar.instance.show("Password Should be Matched",true);
                }
            }
        }
        else 
        {   
            if(signUpUserName.text == string.Empty) 
            {
                MesgBar.instance.show("Please Enter User Name", true);
            }
            else
            {
                MesgBar.instance.show("User Name Has been already Taken", true);                
            }
        }
    }
    
    void ShowSignUpPanel(bool isShow) 
    {   
        if(isShow) 
        {
            UIManager.instance.PanelSwtiching(signInHolder, signUpHolder, 0.5f);        
        }
        else
        { 
            UIManager.instance.PanelSwtiching(signUpHolder, signInHolder, 0.5f);
        }
    }
}

