using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HBScript : MonoBehaviour
{
    // Start is called before the first frame update
public Slider slider;
public void SetMaxHealth(int health)
{
    slider.maxValue=health;
    slider.value=health;
}
public void SetHealth(int health)
{
    slider.value=health ;
}

}