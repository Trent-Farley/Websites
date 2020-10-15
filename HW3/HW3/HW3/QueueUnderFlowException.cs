using System;

public class QueueUnderFlowException : Exception
{
    /// <summary>
    /// Creates a QueueUnderflowException from a general exception. No data is processed
    /// just the name is changed
    /// </summary>
    public QueueUnderFlowException() : base()
    {
    }

    /// <summary>
    /// Exception is created with the message provided
    /// </summary>
    /// <param name="message">Message to use in Exception</param>
    public QueueUnderFlowException(string message) : base(message)
    {
    }
}