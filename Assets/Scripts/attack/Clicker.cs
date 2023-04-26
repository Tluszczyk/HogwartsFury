using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    private AttackManager manager;

    public void Fire()
    {
        if (manager == null)
        {
            manager = GameObject.Find("Player").GetComponent<AttackManager>();
        }
        manager.Fire();
    }
}
