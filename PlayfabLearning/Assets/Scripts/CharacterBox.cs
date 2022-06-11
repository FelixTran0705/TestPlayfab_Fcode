using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character
{
    public string name;
    public float level;
    public Character(string name, float level)
    {
        this.name = name;
        this.level = level;
    }
}

public class CharacterBox : MonoBehaviour
{
    [SerializeField] private InputField nameInput;
    [SerializeField] private Slider levelSlider;

    public Character ReturnClass()
    {
        return new Character(nameInput.text, levelSlider.value);
    }

    public void SetUI(Character character)
    {
        nameInput.text = character.name;
        levelSlider.value = character.level;
    }
}
