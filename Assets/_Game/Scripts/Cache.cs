using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Cache
{
    private static Dictionary<Collider, Character> diccharacter = new Dictionary<Collider, Character>();

    public static Character Character(Collider collider)
    {
        if (!diccharacter.ContainsKey(collider))
        {
            Character character = collider.GetComponent<Character>();
            diccharacter.Add(collider, character);
        }
        return diccharacter[collider];
    }
}
