﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplittedImage : MonoBehaviour {

    Dictionary<int, int> lengthToColumsDict = new Dictionary<int, int>()
    {
        {2, 2 },
        {6, 3 },
        {12, 4 },
        {20, 5 },
        {30, 6 }
    };

    private Stack<Packet> packetStack;
    private int columns;
    private int rows;

    public SplittedImage(Texture2D texture)
    {
        Sprite[] imageSprites = Resources.LoadAll<Sprite>(texture.name);
        columns = lengthToColumsDict[imageSprites.Length];
        rows = columns - 1;

        packetStack = new Stack<Packet>();
        int i = 0;
        for (int col = 0; col < columns; ++col)
            for (int row = 0; row < rows; ++row)
                packetStack.Push(new Packet(col, row, imageSprites[i++]));

        Utils.Shuffle(packetStack);
    }

    internal Packet GetNextPacket()
    {
        if(packetStack.Count == 0)
        {
            Debug.LogError("There is no packet left! This should NEVER happen. Fix this!");
            return null;
        }

        return packetStack.Pop();
    }

    internal int GetUnusedPacketsLeft()
    {
        int unusedPacketsLeft = 0;
        foreach(Packet packet in packetStack)
        {
            if (packet.GetState() == PacketState.NotLaunchedYet)
                ++unusedPacketsLeft;
        }

        return unusedPacketsLeft;
    }
}
