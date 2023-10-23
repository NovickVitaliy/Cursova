using System;
namespace CursovaProject
{
  internal class NegativeValueException : Exception
  {
    public NegativeValueException() : base() { }
    public NegativeValueException(string message) : base(message) { }
    public NegativeValueException(string message, Exception innerException) : base(message, innerException) { }
  }
}
