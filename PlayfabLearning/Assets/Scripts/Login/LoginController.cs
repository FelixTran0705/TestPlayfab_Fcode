using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    
    [SerializeField] private InputField accountIpf;
    [SerializeField] private InputField passIpf;
    [SerializeField] private InputField emailIpf;
    [SerializeField] private Button registerBtn;
    [SerializeField] private Text messageTxt;

    //Register(episode 6)
    public void RegisterButton()
    {
        
        var request = new RegisterPlayFabUserRequest
        {
            
            Email = emailIpf.text,
            Password = passIpf.text,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        messageTxt.text = "Register and logged in!";
    }

    void OnLoginSuccess(LoginResult result)
    {
        messageTxt.text = "Login success!!!";
       
    }
    public void LoginButton()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailIpf.text,
            Password = passIpf.text
        };
        PlayFabClientAPI.LoginWithEmailAddress(request,OnLoginSuccess, OnError);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnError(PlayFabError error)
    {
        Debug.Log("Error while logging in/creating account!");
        Debug.Log(error.GenerateErrorReport());
    }

    public void ResetPasswordButton()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = emailIpf.text,
            TitleId = "73394"
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
    }

    void OnPasswordReset(SendAccountRecoveryEmailResult result)
    {
        messageTxt.text = "Password reset mail sent!";
    }
}
