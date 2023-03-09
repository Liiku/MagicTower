using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Unity.VisualScripting;

public class MapController : MonoBehaviour
{
    public int floor = 1;

    public int[,] map;//地图二维数组
    public int[,] enemy;//敌人二维数组
    public int[,] interactive;//物品二维数组

    public MapElement[] mapElements;
    public EnemyElement[] enemyElements;
    public InteractiveElement[] interactiveElements;

    public Transform parent;
    void Start()
    {
        PrintFloor();
    }

    public void PrintFloor()
    {
        PrintMap("/Floors/" + floor + "/map.txt");
        PrintEnemy("/Floors/" + floor + "/enemy.txt");
        PrintInteractive("/Floors/" + floor + "/interactive.txt");
    }

    //生成地图
    public void PrintMap(string targetFileAddress)
    {
        string url = Application.dataPath + "/assets" + targetFileAddress;
        if (!File.Exists(url))
        {
            Debug.LogError("配置文件出错！！！");
            return;
        }
        string[] rows = File.ReadAllLines(url);//读取文件所有行

        //确定行列
        int mapLength = rows.Length;
        int mapWidth = rows[0].Split('|').Length;

        //创建对应大小的二维数组
        map = new int[mapLength, mapWidth];

        //遍历所有行 分割所有行 赋值列
        for (int i = 0; i < rows.Length; i++)
        {
            string[] cols = rows[i].Split('|');
            for (int j = 0; j < cols.Length; j++)
            {
                map[i, j] = int.Parse(cols[j]);
            }
        }
    
        //遍历数组 创建地图
        for (int i = 0; i < mapLength; i++)
        {
            for (int j = 0; j < mapWidth; j++)
            {
                switch ((MapElement.ElementType)map[i,j])
                {
                    case MapElement.ElementType.Road:
                        GameObject.Instantiate(mapElements[0],new Vector2(j,-i),Quaternion.identity,parent);
                        break;
                    case MapElement.ElementType.Wall:
                        GameObject.Instantiate(mapElements[1],new Vector2(j,-i),Quaternion.identity,parent);
                        break;
                    case MapElement.ElementType.Sky:
                        GameObject.Instantiate(mapElements[2],new Vector2(j,-i),Quaternion.identity,parent);
                        break;
                    default:
                        break;
                }                  
            }
        }
    }

    //生成敌人
    public void PrintEnemy(string targetFileAddress)
    {
        string url = Application.dataPath + "/assets" + targetFileAddress;
        if (!File.Exists(url))
        {
            Debug.LogError("配置文件出错！！！");
            return;
        }
        string[] rows = File.ReadAllLines(url);//读取文件所有行

        //确定行列
        int mapLength = rows.Length;
        int mapWidth = rows[0].Split('|').Length;

        //创建对应大小的二维数组
        enemy = new int[mapLength, mapWidth];

        //遍历所有行 分割所有行 赋值列
        for (int i = 0; i < rows.Length; i++)
        {
            string[] cols = rows[i].Split('|');
            for (int j = 0; j < cols.Length; j++)
            {
                enemy[i, j] = int.Parse(cols[j]);
            }
        }
    
        //遍历数组 创建地图
        for (int i = 0; i < mapLength; i++)
        {
            for (int j = 0; j < mapWidth; j++)
            {
                switch ((EnemyElement.ElementType)enemy[i, j])
                {
                    case EnemyElement.ElementType.none:
                        break;
                    case EnemyElement.ElementType.slim_1:
                        GameObject.Instantiate(enemyElements[1], new Vector2(j, -i), Quaternion.identity, parent);
                        break;
                    case EnemyElement.ElementType.slim_2:
                        GameObject.Instantiate(enemyElements[2], new Vector2(j, -i), Quaternion.identity, parent);
                        break;
                    case EnemyElement.ElementType.slim_3:
                        GameObject.Instantiate(enemyElements[3], new Vector2(j, -i), Quaternion.identity, parent);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    //生成物品
    public void PrintInteractive(string targetFileAddress)
    {
        string url = Application.dataPath + "/assets" + targetFileAddress;
        if (!File.Exists(url))
        {
            Debug.LogError("配置文件出错！！！");
            return;
        }
        string[] rows = File.ReadAllLines(url);//读取文件所有行

        //确定行列
        int mapLength = rows.Length;
        int mapWidth = rows[0].Split('|').Length;

        //创建对应大小的二维数组
        interactive = new int[mapLength, mapWidth];

        //遍历所有行 分割所有行 赋值列
        for (int i = 0; i < rows.Length; i++)
        {
            string[] cols = rows[i].Split('|');
            for (int j = 0; j < cols.Length; j++)
            {
                interactive[i, j] = int.Parse(cols[j]);
            }
        }

        //遍历数组 创建地图
        for (int i = 0; i < mapLength; i++)
        {
            for (int j = 0; j < mapWidth; j++)
            {
                switch ((InteractiveElement.ElementType)interactive[i, j])
                {
                    case InteractiveElement.ElementType.none:
                        break;
                    case InteractiveElement.ElementType.yellewKey:
                        GameObject.Instantiate(interactiveElements[1], new Vector2(j, -i), Quaternion.identity, parent);
                        break;
                    case InteractiveElement.ElementType.violetKey:
                        GameObject.Instantiate(interactiveElements[2], new Vector2(j, -i), Quaternion.identity, parent);
                        break;
                    case InteractiveElement.ElementType.redKey:
                        GameObject.Instantiate(interactiveElements[3], new Vector2(j, -i), Quaternion.identity, parent);
                        break;
                    case InteractiveElement.ElementType.yellowDoor:
                        GameObject.Instantiate(interactiveElements[4], new Vector2(j, -i), Quaternion.identity, parent);
                        break;
                    case InteractiveElement.ElementType.violetDoor:
                        GameObject.Instantiate(interactiveElements[5], new Vector2(j, -i), Quaternion.identity, parent);
                        break;
                    case InteractiveElement.ElementType.redDoor:
                        GameObject.Instantiate(interactiveElements[6], new Vector2(j, -i), Quaternion.identity, parent);
                        break;
                    case InteractiveElement.ElementType.lifePotion:
                        GameObject.Instantiate(interactiveElements[7], new Vector2(j, -i), Quaternion.identity, parent);
                        break;
                    case InteractiveElement.ElementType.upFloor:
                        GameObject.Instantiate(interactiveElements[8], new Vector2(j, -i), Quaternion.identity, parent);
                        break;
                    case InteractiveElement.ElementType.downFloor:
                        GameObject.Instantiate(interactiveElements[9], new Vector2(j, -i), Quaternion.identity, parent);
                        break;
                    default:
                        break;
                }
            }
        }
    }

}
