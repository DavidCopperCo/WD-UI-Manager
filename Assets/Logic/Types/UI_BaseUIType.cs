using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BaseUIType : MonoBehaviour
{
    [SerializeField] public Canvas canvasManager;
    [HideInInspector] public UI_Manager manager;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void Awake()
    {

    }

    public virtual void Active()
    {
        
    }

    

}
