using UnityEngine;

public class WaitingForOperand2 : State
{
    int _currentOperand;
    int _decimalPlaces = 0;
   

    public delegate void CommitOperator(char input);
    public CommitOperator _commitOperator;

    public delegate void UpdateOperand(float current);
    public UpdateOperand _updateOperand;

    public WaitingForOperand2()
    {
        OnEnter();
    }

    // Use this for initialization
    public override void OnEnter()
    {
        _currentOperand = 0;
        _decimalPlaces = 0;
    }

    public override void OnInputReceived(char input)
    {
        ProcessInput(input);

        base.OnInputReceived(input);
    }

    private void ProcessInput(char input)
    {
        if (IsValidOperandData(input))
        {
            UpdateOperandData(input);
        }
        else if (IsValidOperator(input))
        {
            ProcessOperator(input);
        }
    }
    private void UpdateOperandData(char input)
    {
        if ((input >= '0' && input <= '9'))
        {
            _currentOperand *= 10;
            _currentOperand += int.Parse(new string(input, 1));

            float temp = _currentOperand / Mathf.Pow(10, _decimalPlaces);

            if (_updateOperand != null)
            {
                _updateOperand(temp);
            }

            if (_decimalPlaces > 0)
            {
                _decimalPlaces++;
            }
        }
        else if (input == '.')
        {
            if (_decimalPlaces == 0)
            {
                _decimalPlaces = 1;
            }
        }
    }

    bool IsValidOperandData(char input)
    {
        return ((input >= '0' && input <= '9') || input == '.');
    }

    bool IsValidOperator(char input)
    {
        return (input == '+' || input == '-' || input == '*' || input == '/' || input == '%' || input == '=' );
    }

    void ProcessOperator(char input)
    {
        if (input == '+' || input == '-' || input == '*' || input == '/' || input == '%' || input == '=')
        {
            if (_commitOperator != null)
            {
                _commitOperator(input);
            }
        }
    }
}
