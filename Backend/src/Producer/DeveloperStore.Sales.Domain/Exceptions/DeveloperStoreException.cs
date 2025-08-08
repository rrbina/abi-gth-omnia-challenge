using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Domain.Exceptions;

[ExcludeFromCodeCoverage]
public class DeveloperStoreException : Exception
{
    public DeveloperStoreException(string message) : base(message) { }
}