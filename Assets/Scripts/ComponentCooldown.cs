﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LadybugShoot))]
public class ComponentCooldown : MonoBehaviour
{
	public float m_cooldownTimer = 0.2f;
    private bool onCooldown = false;
	private float m_timeStamp;
    private LadybugShoot ls;

    void Awake()
    {
        ls = GetComponent<LadybugShoot>();
    }
	// Use this for initialization
	void Start ()
	{
		m_timeStamp = Time.time + m_cooldownTimer;
	}

	// Update is called once per frame
	void Update ()
	{
        if (onCooldown && Time.time >= m_timeStamp + m_cooldownTimer)
		{
            onCooldown = false;
            ls.SetCooldown(false);
		}
	}

    public void SetCooldownTime(float cd)
    {
        m_cooldownTimer = cd;
    }

    public void StartCooldown()
    {
        onCooldown = true;
        m_timeStamp = Time.time;
    }
}
