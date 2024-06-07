namespace Domain.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a null entity is encountered.
/// Inherits from the Exception class.
/// </summary>
public class NullEntityException(string message) : Exception(message);