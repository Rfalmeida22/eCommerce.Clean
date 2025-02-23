using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Domain.Exceptions;
using eCommerce.Domain.ValueObjects;

public class HistoryAction : ValueObject
{
    public const string INSERT = "I";
    public const string UPDATE = "U";
    public const string DELETE = "D";

    public string Value { get; }

    private static readonly HashSet<string> ValidActions = new() 
    { 
        INSERT, 
        UPDATE, 
        DELETE 
    };

    private HistoryAction(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("Ação é obrigatória");

        if (!ValidActions.Contains(value.ToUpper()))
            throw new DomainException("Ação inválida. Use I, U ou D");

        Value = value.ToUpper();
    }

    public static HistoryAction Create(string value) => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
} 