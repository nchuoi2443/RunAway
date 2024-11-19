using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentCanvas : MonoBehaviour
{
    private static PersistentCanvas instance;

    private void Awake()
    {
        // Kiểm tra nếu đã tồn tại một Canvas
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this; 
        DontDestroyOnLoad(gameObject);
    }
}
