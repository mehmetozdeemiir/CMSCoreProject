using CMSTekrar.Data.Context;
using CMSTekrar.Data.Repositories.Concrete.Base;
using CMSTekrar.Data.Repositories.Interfaces.EntityTypeRepositories;
using CMSTekrar.Entity.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMSTekrar.Data.Repositories.Concrete.EntityTypeRepositories
{
    public class CategoryRepository:KernelRepository<Category>,ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }
    }
}
