using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Name : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Text NameText;
    private string nameOver;
    public string playerName;
    private TextTyping textTyping = null;
    void Update()
    {
        textTyping = FindObjectOfType<TextTyping>();
        if(Input.GetKeyDown(KeyCode.Return)) off();
    }
    public void off()
    {
            playerName = NameText.text;
            textTyping.St(playerName);
            this.gameObject.SetActive(false);
    }

}
