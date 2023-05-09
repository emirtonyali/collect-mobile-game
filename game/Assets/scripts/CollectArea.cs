using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
public class CollectArea : MonoBehaviour
{
    //collect area code 
    [SerializeField] private Transform collectAreaPos;
 
    public Slider Slider;
    public LevelEditor levelEditor;
    public int collectedNumber;
    public TMP_Text collectedNumberText;
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("collectable"))
        {
            
            other.transform.DOMove(collectAreaPos.transform.position, 0.3f); //collectable objects position changing to center of collector
            other.GetComponent<Collectable>().CollectAreaProceses(); //some other arrangement for collect area situations
            Slider.value++; //slider value is increasing with each collectable
            levelEditor.cubes.Remove(other.gameObject);  //remove cubes from list 
            collectedNumber++; //player collected number 
            collectedNumberText.text = collectedNumber.ToString();
           
        }
    }
    
}
