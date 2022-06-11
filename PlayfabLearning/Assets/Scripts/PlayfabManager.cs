using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;
using UnityEngine.UI;
using Newtonsoft.Json;


public class PlayfabManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] CharacterBox[] characterBoxes;
    [SerializeField] Text messageText;
    // Start is called before the first frame update
    void Start()
    {
        
       // Login();
    }

    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };      
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

    void OnSuccess(LoginResult result)
    {
        Debug.Log("Successful login/accout create!");
        GetTitleData();
        GetCharacter();
        
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Error while logging in/creating account!");
        Debug.Log(error.GenerateErrorReport());
    }
    
    public void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "PlatformScore",
                    Value = score
                },
                new StatisticUpdate
                {
                    StatisticName = "GAMA",
                    Value = score
                }
                
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successfulf leaderboard sent");
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "PlatformScore",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
        
    }

    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        foreach (var item in result.Leaderboard)
        {
            Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
        }
    }

    //Get Player Data
    public void GetAppearance()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataRecieved, OnError);

    }

    void OnDataRecieved(GetUserDataResult result)
    {
        Debug.Log("Recieved user data!");
        if(result.Data != null && result.Data.ContainsKey("Hat") && result.Data.ContainsKey("Skin") && result.Data.ContainsKey("Coat"))
        {
            Debug.Log(result.Data["Hat"].Value + " | " + result.Data["Skin"].Value + " | " + result.Data["Coat"].Value);

        }
        else
        {
            Debug.Log("Empty!!");
        }
    }

    public void SaveAppearance()
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { "Hat", "JustinBeiber" },
                { "Skin", "Dior" },
                { "Coat", "Luis Vuitton" }
            },
            Permission = UserDataPermission.Private
            
            
        };
        
        PlayFabClientAPI.UpdateUserData(request,OnDataSend, OnError);
    }

    void OnDataSend(UpdateUserDataResult result)
    {
        Debug.Log("Successful user data send!");
    }

    //Get Title data(episode 4)
    void GetTitleData()
    {
        PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(), OnTitleDataRecieved, OnError);

    }

    void OnTitleDataRecieved(GetTitleDataResult result)
    {
        // StartCoroutine(OnTitleLoad(result));
        if (result.Data == null || result.Data.ContainsKey("Dragon") == false)
        {
            Debug.Log("No message");
            return;
        }
        messageText.text = result.Data["Dragon"];
    }

    //Handling JSON
    public void SaveCharacters()
    {
        List<Character> characters = new List<Character>();
        foreach(var item in characterBoxes)
        {
            characters.Add(item.ReturnClass());
        }
        Debug.Log(JsonUtility.ToJson(characters[0]));
       
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"Characters", JsonConvert.SerializeObject(characters)}
                

            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
    }

    public void GetCharacter()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnCharactersDataRecieved, OnError);
    }

    void OnCharactersDataRecieved(GetUserDataResult result)
    {
        Debug.Log("Recieved characters data!");
        if(result.Data != null && result.Data.ContainsKey("Characters")){
            List<Character> characters = JsonConvert.DeserializeObject<List<Character>>(result.Data["Characters"].Value);
            //List<Character> characters = JsonHelper.FromJson<Character>(result.Data["Characters"].Value);
            //ListCharacter characters = JsonConvert.DeserializeObject<ListCharacter>(result.Data["Characters"].Value);
            Debug.Log(characterBoxes.Length);
            for(int i = 0; i < characterBoxes.Length; i++)
            characterBoxes[i].SetUI(characters[i]);
        }
    }


   

    
}

