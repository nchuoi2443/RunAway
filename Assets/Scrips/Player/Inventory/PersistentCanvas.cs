using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentCanvas : Singleton<PersistentCanvas>
{
    protected override void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
