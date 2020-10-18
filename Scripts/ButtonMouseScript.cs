using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonMouseScript : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Sprite defaultImage;
    public Sprite hoverImage;
    private Button pb;
    void Start()
    {
        pb=GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        pb.image.sprite = hoverImage; ;
        Debug.Log("Mouse Enter");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        pb.image.sprite = defaultImage; ;
        Debug.Log("Mouse Exit");
    }
}
