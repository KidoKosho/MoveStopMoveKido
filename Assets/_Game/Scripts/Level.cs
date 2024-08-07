using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int botCount;
    public float length => 10 * transform.localScale.x;
    public float width => 10 * transform.localScale.y;
    public Vector3 position => transform.position + Vector3.up;
    public int CharacterPlayingGame;
    public void OnDespawn()
    {
        Destroy(gameObject);
    }
}
