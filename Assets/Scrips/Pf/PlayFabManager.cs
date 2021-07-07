using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
public class PlayFabManager : MonoBehaviour
{
    private SaveName saveName = null;
    private TextTyping textTyping = null;
    [SerializeField] private GameObject rowPrefab;
    [SerializeField] private Transform rowsParent;
    private int a = 0;
    void Start()
    {
        Login();
    }
    void Update()
    {
        if(saveName == null)
        {
            saveName = FindObjectOfType<SaveName>();
        }
        if(saveName != null && a == 0)
        {
            SubmitNameButton();
            a++;
        }
    }
    void Login(){
        var request = new LoginWithCustomIDRequest{
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams{
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }
    public void SubmitNameButton()
    {
        var request = new UpdateUserTitleDisplayNameRequest {
            DisplayName = saveName.myName,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
    }
    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("이름 완료!");
    }

        void OnSuccess(LoginResult result) {
            Debug.Log("접속 성공");
            string name = null;
            if(result.InfoResultPayload.PlayerProfile != null){
            name = result.InfoResultPayload.PlayerProfile.DisplayName;}
        }
        void OnError(PlayFabError error) {
            Debug.Log("접속 실패");
            Debug.Log(error.GenerateErrorReport());
        }

    public void SendLeaderboard(int Highscore)
    {
        var request  = new UpdatePlayerStatisticsRequest{
            Statistics = new List<StatisticUpdate>{
                new StatisticUpdate{
                    StatisticName = "HIGHSCORE",
                    Value = Highscore
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, onLeaderboardUpdate, OnError);
    }

    void onLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("성공적 전송");
    }
    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest{
            StatisticName = "HIGHSCORE",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }
    void OnLeaderboardGet(GetLeaderboardResult result){
        Debug.Log("여기까지 왔어");
        foreach (Transform item in rowsParent)
        {
            Destroy(item.gameObject);
        }
        Debug.Log("여기까지 왔어1");

        foreach (var item in result.Leaderboard){
            GameObject newGo = Instantiate(rowPrefab, rowsParent);
            UnityEngine.UI.Text[] texts = newGo.GetComponentsInChildren<UnityEngine.UI.Text>();
            texts[0].text = (item.Position+1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();

            Debug.Log(string.Format("순위: {0} | 이름: {1} | 점수: {2}",
            item.Position, item.PlayFabId, item.StatValue));

        }
    }
}
