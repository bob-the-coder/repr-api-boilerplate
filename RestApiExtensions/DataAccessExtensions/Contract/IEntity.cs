namespace DataAccessExtensions.Contract;

public interface IEntity
{
}

public interface IPkEntity<out TPk> : IEntity where TPk : IComparable
{
    TPk Id { get; }
}