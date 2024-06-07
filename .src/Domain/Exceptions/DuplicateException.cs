namespace Domain.Exceptions;

public class DuplicateException(string message) : Exception(message);