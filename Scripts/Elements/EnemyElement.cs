using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyElement : MonoBehaviour
{
    public enum ElementType//ÔªËØÀàĞÍ
    {
       none,
       slim_1,
       slim_2,
       slim_3,
    }

    public ElementType type;
}
