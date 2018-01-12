using MPDex.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPDex.Repository
{
    public interface IRepositoryFactory
    {
        /// <summary>
        /// Gets the specified repository for the <typeparamref name="EM"/>.
        /// </summary>
        /// <typeparam name="EM">The type of the entity.</typeparam>
        /// <returns>An instance of type inherited from <see cref="IRepository{EntityModel}"/> interface.</returns>
        IRepository<EM> GetRepository<EM>() where EM : Entity;
    }
}
