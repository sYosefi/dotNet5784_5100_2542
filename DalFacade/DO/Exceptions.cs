
namespace DO;

[Serializable]
public class DalDoesNotExistException:Exception
{
    public DalDoesNotExistException(string? message) : base(message) { }
}

public class DalAlreayExistException : Exception
{
    public DalAlreayExistException(string? message) : base(message) { }
}

public class DalDeletionImpossible:Exception
{
    public DalDeletionImpossible(string? message) : base(message) { }
}

public class DalXMLFileLoadCreateException:Exception
{
    public DalXMLFileLoadCreateException(string? message) : base(message) { }
}