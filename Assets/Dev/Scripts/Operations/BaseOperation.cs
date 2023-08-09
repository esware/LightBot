using System;
using System.Collections;
using System.Collections.Generic;
using Dev.Scripts.Character;
using UnityEngine;

public abstract class BaseOperation : MonoBehaviour
{
    protected CharacterControl Control;

    private void Awake()
    {
       Init();
    }

    public void Init()
    {
         Control = FindObjectOfType<CharacterControl>();
    }

    public abstract bool IsValid();
    public abstract System.Collections.IEnumerator Run();
    
}