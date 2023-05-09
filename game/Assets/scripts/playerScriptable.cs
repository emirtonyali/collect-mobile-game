using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new player type",menuName ="player type")]
public class playerScriptable : ScriptableObject
{
    public Color playerColor = Color.yellow;
    public int speed;
    public int turnSpeed;
}
