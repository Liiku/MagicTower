using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveElement : MonoBehaviour
{
    public enum ElementType//��ͼԪ������
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
