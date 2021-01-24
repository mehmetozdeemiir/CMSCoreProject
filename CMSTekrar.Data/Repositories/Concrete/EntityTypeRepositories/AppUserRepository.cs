using CMSTekrar.Data.Context;
using CMSTekrar.Data.Repositories.Concrete.Base;
using CMSTekrar.Data.Repositories.Interfaces.EntityTypeRepositories;
using CMSTekrar.Entity.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMSTekrar.Data.Repositories.Concrete.EntityTypeRepositories
{
    public class AppUserRepository:KernelRepository<AppUser>,IAppUserRepository
    {
        //DIP Pattern  a gore repositoryler birbirinden bağımsız olması için "IAppUserRepository" den kalıtım aldık ve contructor method ile 
        //"ApplicationDbContext" inject edildi.Buradan =>Startup=>ConfigureServise
        public AppUserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }
       
    }
}
