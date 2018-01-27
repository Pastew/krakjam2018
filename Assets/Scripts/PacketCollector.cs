﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketCollector : MonoBehaviour
{

    private ParticleSystem redParticleSystem, greenParticleSystem;
    private PacketType curentPacketType = PacketType.Good;

    void Start()
    {
        redParticleSystem = transform.Find("RedParticleSystem").GetComponent<ParticleSystem>();
        greenParticleSystem = transform.Find("GreenParticleSystem").GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Packet packet = collision.GetComponent<Packet>();

        if (packet == null)
        {
            Debug.LogError("Packet collector collided with something else than Packet, " +
                "that's weird, check this! Collided with" + collision.name);
            return;
        }

        packet.Assign(curentPacketType);
    }

    public void OpenCollector()
    {
        if (curentPacketType != PacketType.Good)
            SetColor(PacketType.Good);
    }

    public void CloseCollector()
    {
        if (curentPacketType != PacketType.Bad)
            SetColor(PacketType.Bad);
    }

    private void SetColor(PacketType packetType)
    {
        curentPacketType = packetType;

        if (packetType == PacketType.Bad)
        {
            redParticleSystem.Play();
            greenParticleSystem.Stop();
        }
        else if (packetType == PacketType.Good)
        {
            redParticleSystem.Stop();
            greenParticleSystem.Play();
        }
    }
}
