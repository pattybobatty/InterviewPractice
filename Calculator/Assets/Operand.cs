using UnityEngine;

public class Operand
{
    bool _isReady = false;
    private float _operandData;
    public float OperandData
    {
        get { return _operandData; }
        set
        {
            _operandData = value;
            _isReady = true;
        }
    }
    public void Clear()
    {
        _operandData = 0.0f;
        _isReady = false;
    }
}
