﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAppearance : MonoBehaviour
{
    private Character character;

    public Character Character
    {
        get { return character; }
        set { character = value; }
    }

    public void SetAppearanceItem(KeyValuePair<AppearanceItemType, AppearanceItem> appearanceItem)
    {
        if (character.appearenceItemLocations[appearanceItem.Key].childCount != 0)
        {
            Destroy(character.appearenceItemLocations[appearanceItem.Key].GetChild(0).gameObject);
        }
        Instantiate(appearanceItem.Value, character.appearenceItemLocations[appearanceItem.Key]);
    }

    public void SetSkin(Color color)
    {
        character.body.GetComponent<Renderer>().material.SetColor("BaseColor", color);
    }
}