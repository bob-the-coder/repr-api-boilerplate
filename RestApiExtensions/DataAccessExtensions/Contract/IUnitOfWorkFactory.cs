using Microsoft.EntityFrameworkCore;

namespace DataAccessExtensions.Contract;

public interface IUnitOfWorkFactory
{
    IUnitOfWork Create();   
}