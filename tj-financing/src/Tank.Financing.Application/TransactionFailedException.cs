using System;

namespace Tank.Financing;

public class TransactionFailedException : Exception
{
    public TransactionFailedException(string message) : base(message)
    {

    }
}