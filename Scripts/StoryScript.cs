using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using FMOD.Studio;

public class StoryScript : MonoBehaviour
{
    static StoryScript instance;
    // Start is called before the first frame update
    public int deathCount;
    public int deathPhase;
    public int deathInterval;
    public int gameplayPhase;
    public float timeInterval;
    public float phaseOneTimeInterval;
    public float phaseTwoTimeInterval;
    public float phaseThreeTimeInterval;
    public float phaseFourTimeInterval;

    public Image deathUI;
    public Sprite death1;
    public Sprite death2;
    public Sprite death3;
    public int currentDeathImage;

    /*[FMODUnity.EventRef]
    public string VODeathPhase="event:/VO/VODeathPhase";
    public string VOGameplayPhase0="event:/VO/VOGameplayPhase0";
    public string VOGameplayPhase1="event:/VO/VOGameplayPhase1";
    public string VOGameplayPhase2="event:/VO/VOGameplayPhase2";
    public string VOGameplayPhase3="event:/VO/VOGameplayPhase3";
    public string VOGameplayPhase4="event:/VO/VOGameplayPhase4";
    */
    //string[] VOGameplayPhases;

    StudioEventEmitter FMODEE;

    GameObject player;
    float firstStartTime;
    public float startTime;
    public bool playerDead;

    public StudioEventEmitter VODeathPhase;
    public StudioEventEmitter VOGameplayPhase0;
    public StudioEventEmitter VOGameplayPhase1;
    public StudioEventEmitter VOGameplayPhase2;
    public StudioEventEmitter VOGameplayPhase3;
    public StudioEventEmitter VOGameplayPhase4;
    StudioEventEmitter[] VOGameplayPhases;
    StudioEventEmitter currentVO;
    StudioEventEmitter musicEE;

    bool dumbTest=true;


    void Awake(){
        FMODEE=GetComponent<StudioEventEmitter>();
        VOGameplayPhases = new StudioEventEmitter[5]{VOGameplayPhase0,VOGameplayPhase1,VOGameplayPhase2,VOGameplayPhase3,VOGameplayPhase4};
        currentVO=VOGameplayPhase1;
        if(instance == null)
        {    
            instance = this; // In first scene, make us the singleton.
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this)
        {
            Destroy(gameObject); // On reload, singleton already set, so destroy duplicate.
        } 
        player=GameObject.FindWithTag("Player");
        playerDead=false;
        deathCount=0;
        //deathInterval=3;
        //gameplayPhase=1;
        firstStartTime=Time.time;
        startTime=Time.time;
        //timeInterval=5f;
        SceneManager.sceneLoaded += ResetStuff;
        SceneManager.sceneLoaded +=UpdateDeathUI;
        musicEE=GameObject.FindGameObjectWithTag("Music").GetComponent<StudioEventEmitter>();
    }

    void Start()
    {
        player.GetComponent<PlayerController>().RecalcRooms(gameplayPhase);
    }
    public void ReloadGame(){
        deathCount++;
        //VODeathPhase.PlayEvent=EmitterGameEvent.ObjectStart;
        
        if(deathCount%deathInterval==0){
            deathPhase++;
            
            SendToFModDeath(deathPhase);
        }
        
        
        gameplayPhase=1;
        startTime=Time.time;
        firstStartTime=Time.time;
        SceneManager.LoadScene("SampleScene");
        SendToFModProgress(gameplayPhase);
        VODeathPhase.SetParameter("DeathPhase",(float)deathPhase,false);
        Debug.Log(VODeathPhase);
        MusicParam();
        currentVO=VOGameplayPhases[gameplayPhase];
        VODeathPhase.Play();
        //VODeathPhase.Stop();
        
        //UpdateDeathUI();
        
        
    }
    // Update is called once per frame
    void Update()
    {
        //Random Comment
        if(Time.time-startTime>=timeInterval&&!playerDead){
            player=GameObject.FindWithTag("Player");
            startTime=Time.time;
            gameplayPhase++;
            if (gameplayPhase>4){
                gameplayPhase=4;
            }
            player.GetComponent<PlayerController>().RecalcRooms(gameplayPhase);
            player.GetComponent<PlayerController>().IncreaseSpeed();

            currentVO=VOGameplayPhases[gameplayPhase];
            MusicParam();
            SendToFModProgress(gameplayPhase);//ERROR SOMEWHERE
        }
        if((Time.time-startTime)%(timeInterval/2f)==0&&(!dumbTest)){
            dumbTest=false;
            Debug.Log("Periodic Play");
            currentVO.Play();
        }
        dumbTest=false;
    }
    void ResetStuff(Scene scene, LoadSceneMode mode){
        playerDead=false;
    }
    public void SendToFModProgress(int i){
        //TODO
        //FMODEE.event=(VOGameplayPhases[i]);
        //FMODEE.Play();
        //FMODEE.Stop();
    }
    public void SendToFModDeath(int i){
        //TODO
    }

    public void PlayProgress(){

    }
    public void PlayDeath(){

    }
    public void MusicParam(){
        musicEE=GameObject.FindGameObjectWithTag("Music").GetComponent<StudioEventEmitter>();
        musicEE.SetParameter("GameplayPhase",(float)gameplayPhase,false);
    }
    void UpdateDeathUI(Scene scene, LoadSceneMode mode){
        deathUI=GameObject.FindWithTag("DeathImage").GetComponent<Image>();
        if (deathPhase==1){
            deathUI.sprite=death2;

        }
        else if(deathPhase==2){
             deathUI.sprite=death3;
        }
        
        currentDeathImage++;
    }
}
