using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelFeatures : MonoBehaviour
{
    //This is for level objects and contain level's data
    public enum LevelType { basicLevel,imageToCubeLevel,timeRemaningLevel,AILevel}
    public LevelType levelType;
    public Texture2D image;
    public int timeRemaning;
    public int cubeNumber;
}
