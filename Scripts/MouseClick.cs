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
        HP.text = "HP:    " + enemy.enemyHealth;//×´Ì¬À¸ÑªÁ¿
        ATK.text = "ATK:    " + enemy.enemyAttack;//×´Ì¬À¸¹¥»÷
        DEF.text = "DEF:    " + enemy.enemyDefense;//×´Ì¬À¸·ÀÓù    
        GOLD.text = "GOLD:    " + enemy.enemyGold;//×´Ì¬À¸½ð±Ò
        EXP.text = "EXP:    " + enemy.enemyExp;//×´Ì¬À¸¾­Ñé
        Image.sprite = enemy.enemyImage;
    }


}
