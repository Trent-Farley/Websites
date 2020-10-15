public class Node<T>
{
    /// <summary>
    /// Data inside the node
    /// </summary>
    public T Data { get; set; }

    /// <summary>
    /// The next node
    /// </summary>
    public Node<T> Next { get; set; }

    /// <summary>
    /// Constructor for each individual node
    /// </summary>
    /// <param name="data">Information each node holds</param>
    /// <param name="next">Next node</param>
    public Node(T data, Node<T> next)
    {
        Data = data;
        Next = next;
    }
}