using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapElement : MonoBehaviour
{
    public enum ElementType//地图元素类型
    {
        Road,
        Wall,
        Sky,
    }

    public ElementType type;
} 
