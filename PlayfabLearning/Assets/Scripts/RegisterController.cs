using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class RegisterController : MonoBehaviour
{
    [SerializeField] InputField account;
    [SerializeField] InputField passWord;
    [SerializeField] Button registerPass;

    private string _accountUser;
    private string _passWord;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRegister()
    {
        SaveDataLog();
        var request = new RegisterPlayFabUserRequest();
        request.Username = _accountUser;
        request.Password = _passWord;
        request.Email = "email" + Random.Range(0,1000) + "@nothing.com";
        PlayFabClientAPI.RegisterPlayFabUser(request, result => {
            Debug.Log("Register successful!");

        }, error => { Debug.Log("Error!"); });
    }
    public void OnLogin()
    {
        SaveDataLog();
        LoginWithPlayFabRequest loginRequest = new LoginWithPlayFabRequest
        {           
            Username = _accountUser,
            Password = _passWord,

        };
        PlayFabClientAPI.LoginWithPlayFab(loginRequest, result => { Debug.Log("Successful login!"); }, error => { Debug.Log("Error!!!!"); });
    }
    void SaveDataLog()
    {
        _accountUser = account.text;
        _passWord = passWord.text;
    }
}
