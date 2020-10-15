using System;

public interface IQueueInterface<T>
{
    /// <summary>
    /// Push an element onto the back of list
    /// </summary>
    /// <param name="element">Any data that is not null</param>
    /// <returns>Element that was pushed onto list</returns>
    T Push(T element);

    /// <summary>
    /// Pop and item off the list if not empty
    /// </summary>
    /// <returns>The first node from the list</returns>
    T Pop();

    /// <summary>
    /// Returns data from the node that is next, otherwise throw an error
    /// </summary>
    /// <returns>Data from the first node</returns>
    T Peek();

    /// <summary>
    /// Returns if the linkedlist has any nodes
    /// </summary>
    /// <returns></returns>
    Boolean IsEmpty();
}