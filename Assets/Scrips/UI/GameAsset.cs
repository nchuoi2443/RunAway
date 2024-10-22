using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAsset : MonoBehaviour
{
    private static GameAsset _instance;
    public static GameAsset Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Instantiate(Resources.Load<GameAsset>("GameAsset"));
                if (_instance == null)
                {
                    Debug.LogError("Failed to load GameAsset from Resources. Ensure the prefab is named 'GameAsset' and is located in the 'Resources' folder.");
                }
                else
                {
                    Debug.Log("GameAsset loaded successfully.");
                }
            }
            return _instance;
        }
    }
    // Add the missing sprite definitions
    public Sprite MaxHealthSprite;
    public Sprite MaxStaminaSprite;
    public Sprite DamageSprite;
    public Sprite SwordSprite;
    public Sprite ShieldSprite;
    public Sprite BowSprite;
    public Sprite StaffSprite;
}
