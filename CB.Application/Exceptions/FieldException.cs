namespace CB.Application.Exceptions;

public class FieldException
{
    public string FieldName { get; }
    public string Message { get; }

    public FieldException(string fieldName, string message)
    {
        FieldName = fieldName;
        Message = message;
    }
}