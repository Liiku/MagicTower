using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MouseClick : MonoBehaviour
{
    public TextMeshProUGUI HP;
    public TextMeshProUGUI ATK;
    public TextMeshProUGUI DEF;
    public TextMeshProUGUI GOLD;
    public TextMeshProUGUI EXP;
    public Image Image;

    public GameObject enemy_UI;

    public bool Click = true;
    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }


    void OnMouseDown()
    {
        enemy_UI.SetActive(Click);
        Click = !Click;
        HP.text = "HP:    " + enemy.enemyHealth;//״̬��Ѫ��
        ATK.text = "ATK:    " + enemy.enemyAttack;//״̬������
        DEF.text = "DEF:    " + enemy.enemyDefense;//״̬������    
        GOLD.text = "GOLD:    " + enemy.enemyGold;//״̬�����
        EXP.text = "EXP:    " + enemy.enemyExp;//״̬������
        Image.sprite = enemy.enemyImage;
    }


}
