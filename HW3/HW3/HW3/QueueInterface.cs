using System;

public interface IQueueInterface<T>
{
    T Push(T element);

    T Pop();

    T Peek();

    Boolean IsEmpty();
}