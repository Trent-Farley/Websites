using System;

public class LinkedQueue<T> : IQueue<T>
{
    private Node<T> front;
    private Node<T> rear;

    /// <summary>
    /// Optional valued constructor to set up pointer for the front and back of list
    /// </summary>
    /// <param name="front">Front of list, if not provided is null</param>
    /// <param name="rear">last pointer, if not provided is null</param>
    public LinkedQueue(Node<T> front = null, Node<T> rear = null)
    {
        this.front = front;
        this.rear = rear;
    }

    public bool IsEmpty()
    {
        return front == null && rear == null;
    }

    public T Peek()
    {
        if (IsEmpty())
        {
            throw new QueueUnderFlowException("The queue was empty when peek invoked");
        }
        return front.Data;
    }

    public T Pop()
    {
        T tmp;
        if (IsEmpty())
        {
            throw new QueueUnderFlowException("The queue was empty when pop was invoked");
        }
        else if (front == rear)
        {
            tmp = front.Data;
            front = null;
            rear = null;
        }
        else
        {
            tmp = front.Data;
            front = front.Next;
        }
        return tmp;
    }

    public T Push(T element)
    {
        if (element == null)
        {
            throw new NullReferenceException();
        }
        if (IsEmpty())
        {
            Node<T> tmp = new Node<T>(element, null);
            rear = front = tmp;
        }
        else
        {
            Node<T> tmp = new Node<T>(element, null);
            rear.Next = tmp;
            rear = tmp;
        }
        return element;
    }
}