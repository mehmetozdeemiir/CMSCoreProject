using CMSTekrar.Data.Repositories.Interfaces.Base;
using CMSTekrar.Entity.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMSTekrar.Data.Repositories.Interfaces.EntityTypeRepositories
{
    public interface IProductRepository:IKernelRepository<Product>
    {
    }
}
