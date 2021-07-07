using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Name : MonoBehaviour
{
    private SaveName saveName = null;

    [SerializeField] UnityEngine.UI.Text NameText;
    private string nameOver;
    public string playerName;
    private TextTyping textTyping = null;
    void Start()
    {
        saveName = FindObjectOfType<SaveName>();
    }
    void Update()
    {
        textTyping = FindObjectOfType<TextTyping>();
        if(Input.GetKeyDown(KeyCode.Return)) off();
    }
    public void off()
    {
            playerName = NameText.text;
            saveName.GetName(playerName);
            textTyping.St(playerName);
            this.gameObject.SetActive(false);
    }

}
