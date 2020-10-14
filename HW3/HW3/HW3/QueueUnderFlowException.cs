using System;

public class QueueUnderFlowException : Exception
{
    public QueueUnderFlowException() : base()
    {
    }

    public QueueUnderFlowException(string message) : base(message)
    {
    }
}