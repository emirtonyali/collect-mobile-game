using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditor : MonoBehaviour
{

    public Texture2D image;
    public float scale = 1.0f;
    public Mesh Cube;
    public int cubesNum;
    public List<GameObject> cubes = new List<GameObject>();
    [SerializeField] private ObjectPool objectPool;
    public Slider slider;
    public levelSystem levelSystem;
    public GameObject playerUI;
    public GameObject AIUI;
    public GameObject TimeRemaningUI;
    public LevelFeatures levelFeatures;
    public Transform centerTransform;
    public float spawnInterval;
    public GameObject timeRemaningThings;
    public bool AILevelBool;
    public bool counterLevelBool;
    public CollectAreaForAI collectAreaForAI;
    public CollectArea collectAreaForPlayer;
    public TimeRemaining timeRemaining;
    private void Update()
    {

        LevelWinFailCont();

    }
    public void BasicLevel()
    {
        SpawnOneTime();

        slider.maxValue = levelFeatures.cubeNumber;
    }
    public void ImageToCube()
    {
        image = levelFeatures.image;
        int width = image.width;
        int height = image.height;
        Color[] pixels = image.GetPixels();


        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int index = y * width + x;
                Color pixel = pixels[index];

                GameObject cube = objectPool.GetPoolObject();
                cube.AddComponent<BoxCollider>();
                cube.GetComponent<MeshFilter>().mesh = Cube;
                cube.transform.position = new Vector3(x * scale, y * scale, 0.0f);
                cube.GetComponent<MeshRenderer>().material.color = pixel; // enable gpu instancing
                cube.transform.localScale = new Vector3(scale, scale, scale);
                cube.transform.parent = this.gameObject.transform;
                cube.AddComponent<Rigidbody>();
                cube.AddComponent<Collectable>();
                cube.tag = "collectable";
                cubesNum++;
                cubes.Add(cube);

            }
        }
        slider.maxValue = cubesNum;
        transform.localEulerAngles = new Vector3(90, 0, 0);
    }
    public void TimeRemainingLevel()
    {
        TimeRemaningUI.SetActive(true);
        StartCoroutine(SpawnCountinously());
        cubesNum = 1000;
        slider.gameObject.SetActive(false);
        timeRemaningThings.SetActive(true);
        counterLevelBool = true;


    }
    public void AILevel()
    {
        ImageToCube(); //we can use again imageTocube
        AIUI.SetActive(true);
        AILevelBool = true;
    }
    private IEnumerator SpawnCountinously()
    {
        while (true)
        {
            var cube = objectPool.GetPoolObject();
            cube.transform.position = centerTransform.position;
            if (cube.GetComponent<BoxCollider>() == null)
            {
                cube.AddComponent<BoxCollider>();
                cube.GetComponent<MeshFilter>().mesh = Cube;
                cube.transform.localScale = new Vector3(scale, scale, scale);
                cube.transform.parent = this.gameObject.transform;
                cube.AddComponent<Rigidbody>();
                cube.AddComponent<Collectable>();

            }

            cube.GetComponent<MeshRenderer>().material.color = Color.white;
            cube.tag = "collectable";
            cube.layer = 0;
            //cubesNum++;
            // cubes.Add(cube);
            yield return new WaitForSeconds(spawnInterval);
        }

    }
    private void SpawnOneTime()
    {
        for (int i = 0; i < levelFeatures.cubeNumber; i++)
        {

            var cube = objectPool.GetPoolObject();
            cube.transform.position = centerTransform.position;
            if (cube.GetComponent<BoxCollider>() == null)
            {
                cube.AddComponent<BoxCollider>();
                cube.GetComponent<MeshFilter>().mesh = Cube;
                cube.transform.localScale = new Vector3(scale, scale, scale);
                cube.transform.parent = this.gameObject.transform;
                cube.AddComponent<Rigidbody>();
                cube.AddComponent<Collectable>();

            }

            cube.GetComponent<MeshRenderer>().material.color = Color.white;
            cube.tag = "collectable";
            cube.layer = 0;
            cubesNum++;
            cubes.Add(cube);
            
        }


    }
   
    public void EditorEssentials()
    {
        levelFeatures = levelSystem.currentLevel.gameObject.GetComponent<LevelFeatures>();
        if (levelFeatures.levelType == LevelFeatures.LevelType.basicLevel)
        {
            BasicLevel();

        }
        if (levelFeatures.levelType == LevelFeatures.LevelType.imageToCubeLevel)
        {

            ImageToCube();
        }
        if (levelFeatures.levelType == LevelFeatures.LevelType.timeRemaningLevel)
        {
            TimeRemainingLevel();

        }
        if (levelFeatures.levelType == LevelFeatures.LevelType.AILevel)
        {

            AILevel();
        }
    }
    private void LevelWinFailCont()
    {
        if (!counterLevelBool)
        {
            if (slider.value >= cubesNum)
            {
                
                if (!AILevelBool)
                {
                    levelSystem.WinLevel();
                }
                if (AILevelBool)
                {
                    Debug.Log("secondstep");
                    if (collectAreaForAI.collectedNumberAI >= collectAreaForPlayer.collectedNumber)
                    {
                        Debug.Log("secondstep1");
                        levelSystem.FailMenu();
                    }
                    else
                    {
                        Debug.Log("secondstep2");
                        levelSystem.WinLevel();
                    }
                }

            }

        }
        if (counterLevelBool)
        {
            if (timeRemaining.timeRemaining <= 0)
            {
                levelSystem.WinLevel();
            }
        }

    }
}

