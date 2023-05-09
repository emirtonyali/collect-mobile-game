using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class CollectAreaForAI : MonoBehaviour
{
    [SerializeField] private Transform collectAreaPos;
    public Slider Slider;
    public LevelEditor levelEditor;
    public int collectedNumberAI;
    public TMP_Text collectedNumberTextAI;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("collectable"))
        {
            
            other.transform.DOMove(collectAreaPos.transform.position, 0.3f);
            other.GetComponent<Collectable>().CollectAreaProcesesAI();
            Slider.value++;
            collectedNumberAI++;
            collectedNumberTextAI.text = collectedNumberAI.ToString();

            levelEditor.cubes.Remove(other.gameObject);
           
           
        }
    }
}
