using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.Linq;

public class Health : MonoBehaviour
{
    private float _maxHealth;
    private float _health;
    [SerializeField] private UnityEngine.UI.Image[] hearts;
    public Sprite emptyHeart;
    public Sprite fullHeart;

    void Awake()
    {
        _maxHealth = hearts.Count();
        _health = 3;
        resetHeart();

        for (int i = (int)_maxHealth - 1; i >= _health; i--)
        {
            hearts[i].enabled = false;
        }

    }

    void resetHeart()
    {
        foreach (UnityEngine.UI.Image heart in hearts)
        {
            heart.sprite = fullHeart;
        }
    }

    public void dmgHealth(float num)
    {
        _health -= num;
        setHearts(emptyHeart, (int)_health);
    }

    void setHearts(Sprite newSprite, int idx)
    {
        Debug.Log(idx);
        hearts[idx].sprite = newSprite;
    }


}
