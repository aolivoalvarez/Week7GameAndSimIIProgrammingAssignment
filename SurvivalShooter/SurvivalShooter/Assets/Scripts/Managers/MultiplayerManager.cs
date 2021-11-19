using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerManager : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    
    [SerializeField]
    Camera cam1;

    [SerializeField]
    Camera cam2;

    bool pressed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space") && (!pressed)) {
            GameObject p = Instantiate(player, transform.position, transform.rotation);
            getSplitScreen();
            pressed = true;
        }
    }

    public void getSplitScreen()
    {
        cam1.rect = new Rect(0, 0, 1, 0.5f);
        cam2.rect = new Rect(0, 0.5f, 1, 0.5f);
    }
}
