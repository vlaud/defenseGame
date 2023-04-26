using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    private bool isInput;
    private char _inputChar;
    private event Action<char> OnCharacterInput = delegate { };

    private void ReadInputCharacter()
    {
        foreach (char c in Input.inputString)
        {
            if (c == '\b') // has backspace/delete been pressed?
            {
                return;
            }
            else if ((c == '\n') || (c == '\r')) // enter/return
            {
                return;
            }
            else if (char.IsLetter(c))
            {
                _inputChar = c;
                OnCharacterInput(_inputChar);
            }
        }
    }
    private void Awake()
    {
        isInput = false;
    }
    private void Update()
    {
        if(isInput) ReadInputCharacter();
    }

    public void AssignOnInputListener(Action<char> listener)
    {
        OnCharacterInput += listener;
    }

    public void UnssignOnInputListener(Action<char> listener)
    {
        OnCharacterInput -= listener;
    }
    public void OnValueChangedEvent(string str)
    {
    }
    public void OnEndEditEvent(string str)
    {
    }
    public void OnSelectEvent(string str)
    {
        isInput = true;
    }
    public void OnDeselectEvent(string str)
    {
        isInput = false;
    }
    public void ResetText()
    {
        inputField.Select();
        inputField.text = "";
    }
}
