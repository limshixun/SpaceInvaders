using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.Linq;
using System.Linq.Expressions;
using TMPro;

public class Health : MonoBehaviour
{
    private float _maxHealth;
    private float _health;
    [SerializeField] private UnityEngine.UI.Image[] hearts;

    public Sprite emptyHeart;
    public Sprite fullHeart;

    void Awake()
    {


    }

    public void resetHeart()
    {
        _maxHealth = hearts.Count();
        _health = 1;
        foreach (UnityEngine.UI.Image heart in hearts)
        {
            heart.sprite = fullHeart;
        }
        for (int i = (int)_maxHealth - 1; i >= _health; i--)
        {
            hearts[i].enabled = false;
        }
    }

    public void dmgHealth(float num)
    {
        _health -= num;
        Debug.Log(_health);
        if (_health < 0)
        {
            GameManager.instance.Gameover();
        }
        else
        {
            setHearts(emptyHeart, (int)_health);
        }
    }

    void setHearts(Sprite newSprite, int idx)
    {
        Debug.Log(idx);
        hearts[idx].sprite = newSprite;
    }


}
