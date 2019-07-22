using UnityEngine;
using UnityEngine.UI;

public class CalcButton : MonoBehaviour
{
    Button _myButton;
    [SerializeField] Text _text;

    private void Start()
    {
        _myButton = GetComponent<Button>();
        if  ( _myButton != null )
        {
            _myButton.onClick.AddListener(OnClick);
        }

        if (_text == null)
        {
            _text = GetComponentInChildren<Text>();
        }
    }

    private void OnClick()
    {
        if (_text != null && !string.IsNullOrEmpty(_text.text) )
        {
            Calculator.Instance.OnInputRecieved(_text.text[0]);
        }
    }
    
}
