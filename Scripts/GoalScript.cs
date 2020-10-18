using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    float startingOffset;
    public float offset;
    void Awake()
    {
        startingOffset=-player.transform.position.x+offset;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        //transform.Translate(Vector3.right * Time.deltaTime*player.GetComponent<PlayerController>().AutoHorizontalSpeed,Space.World);
        transform.position=new Vector3(player.transform.position.x+startingOffset,transform.position.y,transform.position.z);
    }
}
