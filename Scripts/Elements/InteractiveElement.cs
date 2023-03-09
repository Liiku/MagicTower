using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveElement : MonoBehaviour
{
    public enum ElementType//地图元素类型
    {
        none,
        yellewKey,
        violetKey,
        redKey,
        yellowDoor,
        violetDoor,
        redDoor,
        lifePotion,
        upFloor,
        downFloor,
    }

    public ElementType type;
}
