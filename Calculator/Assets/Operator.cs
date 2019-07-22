public class Operator
{
    private char _operator;

    public Operator()
    {
        Clear();
    }

    public char CurrentOperator
    {
        get { return _operator; }
        set { _operator = value; }
    }

    public void Clear()
    {
        _operator = '0';
    }

    public bool IsReady
    {
        get { return _operator != '0'; }
    }
}
