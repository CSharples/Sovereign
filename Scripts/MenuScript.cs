using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject creditsPanel;
    public bool creditOn;

    void Start()
    {
        creditOn=false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(){
        SceneManager.LoadScene("SampleScene");
    }
    public void Options(){

    }
    public void Credits(){
        Debug.Log("Test");
        creditOn=!creditOn;
        creditsPanel.SetActive(creditOn);
    }
    public void QuitGame(){
        Application.Quit();
    }
}
