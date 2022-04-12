using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System;

public class PlayFabLogin : MonoBehaviourPunCallbacks
{
    private string userEmail;
    private string userPassword;
    private string username;
    bool setReigst = false;

    public GameObject loginPanel;
    public InputField EmailinputField;

    public InputField PasswordinputField;
    public InputField UsernameinputField;

    public void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
        //PlayFabSettings.DeveloperSecretKey = "";
    }
   

    public void Start()
    {

        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {                        
          // Please change this value to your own titleId from PlayFab Game Manager
        }
        if (PlayerPrefs.HasKey("EMAIL"))
        {
            userEmail = PlayerPrefs.GetString("EMAIL");
            userPassword = PlayerPrefs.GetString("PASSWORD");

            EmailinputField.text = userEmail;
            PasswordinputField.text = userPassword;
            
        }
        
    }
    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        loginPanel.SetActive(false);
       
        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);
        PhotonNetwork.JoinLobby();
        PlayFab.ClientModels.GetAccountInfoRequest getAccountInfoRequest = new GetAccountInfoRequest();
        PlayFab.PlayFabClientAPI.GetAccountInfo(getAccountInfoRequest, resultCallback, OnRegisterFailure, null, null);
       
    }
    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
       
        Debug.Log("RegistCongratulations, you made your first successful API call!");
        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);
        loginPanel.SetActive(false);
    }

    private void resultCallback(GetAccountInfoResult result)
    {
        GetComponent<IDInfo>().getNickname(result.AccountInfo.Username);
    }

    private void OnLoginFailure(PlayFabError error)
    {
        var registerRequest = new RegisterPlayFabUserRequest { Email = userEmail, Password = userPassword, Username = username };
        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, OnRegisterFailure);
    }
    private void OnRegisterFailure(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }
    public void GetUserEmail(string emailIn)
    {
        userEmail = emailIn;
    }
    public void GetUserPassword(string passwordIn)
    {
        userPassword = passwordIn;
    }
    public void GetUsername(string usernameIn)
    {
        username = usernameIn;
    }
    public void OnClickLogin()
    {
        var request = new LoginWithEmailAddressRequest { Email = userEmail, Password = userPassword };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }
    public void OnClickReigst()
    {
        var addLoginRequest = new RegisterPlayFabUserRequest { Email = userEmail, Password = userPassword, Username = username };
        PlayFabClientAPI.RegisterPlayFabUser(addLoginRequest, OnAddLoginSuccess, OnRegisterFailure);
        OnlCickSetReigst();
    }
    public void OnlCickSetReigst()
    {
        if (!setReigst)
        {
            setReigst = true;
            loginPanel.transform.GetChild(0).gameObject.SetActive(true);
            loginPanel.transform.GetChild(3).gameObject.SetActive(false);
            loginPanel.transform.GetChild(4).gameObject.SetActive(true);
        }
        else
        {
            setReigst = false;
            loginPanel.transform.GetChild(0).gameObject.SetActive(false);
            loginPanel.transform.GetChild(3).gameObject.SetActive(true);
            loginPanel.transform.GetChild(4).gameObject.SetActive(false);
        }
    }
    private void OnAddLoginSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);
    }

}
