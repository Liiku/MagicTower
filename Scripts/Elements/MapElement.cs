using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapElement : MonoBehaviour
{
    public enum ElementType//��ͼԪ������
    {
        Road,
        Wall,
        Sky,
    }

    public ElementType type;
} 
