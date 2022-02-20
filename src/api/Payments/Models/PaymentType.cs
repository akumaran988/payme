using System.Runtime.Serialization;

public enum PaymentType
{
    [EnumMember(Value = "None")]
    None,

    [EnumMember(Value = "Credit")]
    Credit,

    [EnumMember(Value = "Debit")]
    Debit
}