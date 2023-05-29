using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface IUserService : IBaseService<User>
    {
        Task<User> FirstOrDefaultAsync(Expression<Func<User, bool>> filter, CancellationToken cancellationToken = default);

    }
}
