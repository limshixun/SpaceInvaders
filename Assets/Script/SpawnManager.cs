using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _gridArea;
    [SerializeField] private GameObject enemy1Prefab;
    [SerializeField] private GameObject enemy2Prefab;
    [SerializeField] private Camera cam;
    private int _columns;
    private int _rows;
    private float spacing;
    private Vector2 startPos;
    private float moveTimer = 0f;
    private float movespeed = 0.5f;
    private float moveAmount = 0.25f;
    private float direction;
    void Awake()
    {
        moveTimer = movespeed;
        _columns = 4;
        _rows = 8;
        spacing = 1;
        startPos = transform.position;
        direction = 1;

        if (_gridArea == null)
        {
            _gridArea = GetComponent<BoxCollider2D>();
        }

        var maxX = Mathf.Round(_gridArea.bounds.max.x);
        var maxY = Mathf.Round(_gridArea.bounds.max.y);
        Debug.Log(maxX + " " + maxY);

        var enemyPrefab = enemy1Prefab;
        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _columns; j++)
            {
                if (j == 0)
            {
                enemyPrefab = enemy2Prefab;
            }
            else
            {
                enemyPrefab = enemy1Prefab;
            }
                var spawnPos = startPos + new Vector2(i * spacing, -j * spacing);
                Instantiate(enemyPrefab, spawnPos, Quaternion.identity, transform);
            }
        }
    }

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        moveTimer -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (moveTimer <= 0)
        {
            MoveHorde();
            moveTimer = movespeed;
        }
    }

    private void MoveHorde()
    {
        var movement = Vector3.right * moveAmount * direction;
        if (HasReachedEnd())
        {
            direction *= -1;
            movement = Vector3.down * moveAmount;
        }
        transform.position += movement;
    }

    private bool HasReachedEnd()
    {
        foreach (Transform enemy in transform)
        {
            if (enemy == null) continue;
            Vector3 viewportPos = cam.WorldToViewportPoint(enemy.position);

            if (direction > 0 && viewportPos.x >= 0.95f) return true;
            if (direction < 0 && viewportPos.x <= 0.05f) return true;
        }
        return false;
    }
}
