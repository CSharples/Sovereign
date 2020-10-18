using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float horizontalSpeed =3f;
    public float AutoHorizontalSpeed =3f;
    public float verticalSpeed =3f;
    public float horizontalSpeedIncrement=2f;
    public float verticalSpeedIncrement=2f;

    bool dead;

    public bool playerControlHorizontal;
    private Rigidbody2D rb2d;

    public GameObject[] RoomPrefabs=new GameObject[10];

    public GameObject[] EasyRooms;

    public GameObject[] MediumRooms;

    public GameObject[] HardRooms;

    public float deadPause;

    Vector3 roomDistance=new Vector3(19.825f,0f,0f);
    Vector3 newestRoomPos;

    float deadTime;
    public StoryScript storyManager;

    public int movementMode; //For Experimentation
    //0==Translate
    //1==AddForce
    //2==Velocity


    void Awake(){
        storyManager=GameObject.FindGameObjectsWithTag("StoryManager")[0].GetComponent<StoryScript>();
    }
    void Start()
    {
        RoomPrefabs=EasyRooms;
        newestRoomPos=roomDistance*2;
        rb2d = GetComponent<Rigidbody2D>();
        dead=false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HorizontalMovement1(float m){
        if(playerControlHorizontal){
            if(Mathf.Abs(m)>0.1){
                transform.Translate(Vector3.right * Time.deltaTime*m*horizontalSpeed,Space.World);
            }
        }
        else{
            transform.Translate(Vector3.right * Time.deltaTime*AutoHorizontalSpeed,Space.World);
        }
        
    }
    void VerticalMovement1(float m){
        if(Mathf.Abs(m)>0.1){
            transform.Translate(Vector3.up * Time.deltaTime*m*verticalSpeed,Space.World);
        }
    }

    void FixedUpdate()
    {
        if(!dead){

            HorizontalMovement1(Input.GetAxis("Horizontal"));

            VerticalMovement1(Input.GetAxis("Vertical"));
            

        }
        if (dead && Time.time - deadTime > deadPause)
        {
            storyManager.ReloadGame();
            
        }       
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag=="Hazard"){
            Die();
        }
        else{

        }
        
    }
    void OnTriggerEnter2D(Collider2D col){
        CreateRoom();

    }

    void CreateRoom(){
        Instantiate(RoomPrefabs[Random.Range(0,RoomPrefabs.Length)],newestRoomPos,Quaternion.identity);
        newestRoomPos+=roomDistance;
        //TODO: Room Creation


    }
    void Die(){
        Debug.Log("Dead");
        dead=true;
        storyManager.playerDead=true;
        storyManager.startTime=Time.time;
        GetComponent<MeshRenderer>().enabled=(false);
        deadTime = Time.time;
    }
    public void RecalcRooms (int stage){
        if(stage==1){
            RoomPrefabs=EasyRooms;
        }
        else if(stage==2){
            RoomPrefabs=new GameObject[EasyRooms.Length + MediumRooms.Length];
            EasyRooms.CopyTo(RoomPrefabs,0);
            MediumRooms.CopyTo(RoomPrefabs,EasyRooms.Length);
        }
        else if(stage==3){
            RoomPrefabs=MediumRooms;
        }
        else if(stage==4){
            RoomPrefabs=HardRooms;
        }
    }
    public void IncreaseSpeed(){
        verticalSpeed+=verticalSpeedIncrement;
        horizontalSpeed+=horizontalSpeedIncrement;
        AutoHorizontalSpeed+=horizontalSpeedIncrement;
    }
}
