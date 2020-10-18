using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameControl : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject backgroundSpriteA;
    public GameObject backgroundSpriteB;
    
    public GameObject player;
    Renderer ARend;
    Renderer BRend;


    void Awake()
    {
        ARend = backgroundSpriteA.GetComponent<Renderer>();
        BRend = backgroundSpriteB.GetComponent<Renderer>();
        ARend.shadowCastingMode=ShadowCastingMode.Off;
        BRend.shadowCastingMode=ShadowCastingMode.Off;
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
        BackgroundScroll();
    }
    void BackgroundScroll(){
        if(ARend.isVisible == false&&player.transform.position.x>0){
            backgroundSpriteA.transform.position = backgroundSpriteB.transform.position + new Vector3(2*10.21f, 0, 0);
            //Debug.Log("BonkA");
        }
        if(BRend.isVisible == false&&player.transform.position.x>0){
            backgroundSpriteB.transform.position = backgroundSpriteA.transform.position + new Vector3(2*10.21f, 0, 0);
            //Debug.Log("BonkB");
        }

    }
}
