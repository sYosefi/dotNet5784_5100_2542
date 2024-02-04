namespace BO;

[Serializable]
public class BlDoesNotExistException:Exception
{
    public BlDoesNotExistException(string? message) : base(message) { }
    public BlDoesNotExistException(string message,Exception innerException) : base(message, innerException) { }
}

public class BlNullPropertyException:Exception
{
    public BlNullPropertyException(string? message) : base(message) { }
}

public class BlAlreadyExistException:Exception
{
    public BlAlreadyExistException(string? message):base(message) { }
}

public class BlDeletionImpossible : Exception
{
    public BlDeletionImpossible(string? message) : base(message) { }
}

public class BlInvalidValueException:Exception
{
    public BlInvalidValueException(string? message) : base(message) { }
}

