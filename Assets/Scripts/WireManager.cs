using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireManager : MonoBehaviour
{
    public void PlayAnimation()
    {
        WireAnimation[] anims = GetComponentsInChildren<WireAnimation>();

        for(int i = 0; i < anims.Length; i++)
        {
            anims[i].PowerUp();
        }
    }
}
