using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool playerInput;
    public GameManager Instance;
    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        playerInput = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
