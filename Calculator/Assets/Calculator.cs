using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calculator : MonoBehaviour
{
    private static Calculator _instance;
    public static Calculator Instance
    {
        get { return _instance; }
    }
    Operator _currentOperator;
    Operand _op1;
    Operand _op2;

    State _currentState;
    WaitingForOperand1 _waitingForOperand1;
    WaitingForOperand2 _waitingForOperand2;

    [SerializeField] Text _display;

    private void Awake()
    {
        _instance = this;
    }

    private void Init()
    {
        _currentOperator = new Operator();
        _op1 = new Operand();
        _op2 = new Operand();
        InitStateMachine();
    }

    private void InitStateMachine()
    {
        _waitingForOperand1 = new WaitingForOperand1();
        _waitingForOperand1._onClear += OnClearRecieved;
        _waitingForOperand1._commitOperator += OnOperatorRecieved;
        _waitingForOperand1._updateOperand += OnOperand1Updated;

        _waitingForOperand2 = new WaitingForOperand2();
        _waitingForOperand2._onClear += OnClearRecieved;
        _waitingForOperand2._commitOperator += OnOperatorRecieved;
        _waitingForOperand2._updateOperand += OnOperand2Updated;

        _currentState = _waitingForOperand1;
        _currentState.OnEnter();
    }

    void OnOperand1Updated(float op)
    {
        _display.text = op.ToString();

        _op1.OperandData = op;
    }

    void OnOperand2Updated(float op)
    {
        _display.text = op.ToString();

        _op2.OperandData = op;
    }

    public void OnClearRecieved()
    {
        _currentOperator.Clear();
        _op1.Clear();
        _op2.Clear();

        _currentState = _waitingForOperand1;
        _currentState.OnEnter();
        _display.text = _op1.OperandData.ToString();
    }

    void OnOperatorRecieved(char op)
    {
        if ( op == '+' || op == '-' || op == '*' || op == '/' || op == '%' )
        {
            if (_currentState == _waitingForOperand1)
            {
                _currentOperator.CurrentOperator = op;
                _currentState = _waitingForOperand2;
                _currentState.OnEnter();
            }
            else if ( _currentState == _waitingForOperand2 )
            {
                _op1.OperandData = Calculate();
                _currentState = _waitingForOperand2;
                _currentState.OnEnter();
            }
        }

        if (op == '=')
        {
            _op1.OperandData = Calculate();
            _currentState = _waitingForOperand1;
            _currentState.OnEnter();
        }
    }

    public void OnInputRecieved(char input)
    {
        if (_currentState != null )
            _currentState.OnInputReceived(input);
    }

    private float Calculate()
    {
        float result;
        if ( _currentOperator.CurrentOperator == '+' )
        {
            result = (_op1.OperandData + _op2.OperandData);
            _display.text = result.ToString(); ;
        }
        else if (_currentOperator.CurrentOperator == '-')
        {
            result = (_op1.OperandData - _op2.OperandData);
            _display.text = result.ToString();
        }
        else if (_currentOperator.CurrentOperator == '*')
        {
            result = (_op1.OperandData * _op2.OperandData);
            _display.text = result.ToString();
        }
        else if (_currentOperator.CurrentOperator == '/')
        {
            if (_op2.OperandData != 0)
            {
                result = (_op1.OperandData / _op2.OperandData);
                _display.text = result.ToString();
            }
            else
            {
                result = 0.0f;
                _display.text = "Error Divide by Zero";
            }
        }
        else if (_currentOperator.CurrentOperator == '%')
        {
            if (_op2.OperandData != 0)
            {
                result = (_op1.OperandData % _op2.OperandData);
                _display.text = result.ToString();
            }
            else
            {
                result = 0.0f;
                _display.text = "Error Divide by Zero";
            }
        }
        else
        {
            result = 0.0f;
        }
        return result;
    }

    // Use this for initialization
    void Start ()
    {
        Init();

    }
	
	// Update is called once per frame
	void Update ()
    {

	}

}

