using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class CharacterEvents
{
    public static UnityEvent<GameObject, float, bool> characterTookDmg = new UnityEvent<GameObject, float, bool>();
}
