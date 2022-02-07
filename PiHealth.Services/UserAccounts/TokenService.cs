using Microsoft.AspNetCore.Http;
using PiHealth.DataModel;
using PiHealth.DataModel.Entity;
using PiHealth.Service.UserAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace PiHealth.Services.UserAccounts
{
    public interface ITokenService
    {
        UserToken Add(UserToken entity);
        UserToken Get(long userId);

        void Delete(UserToken entity);

        IQueryable<UserToken> GetAll();
    }

    public class TokenService: ITokenService
    {
        public readonly IRepository<UserToken> _repository;     

        public TokenService(IRepository<UserToken> repository)
        {
            _repository = repository;            
        }

        public virtual UserToken Get(long userId)
        {
            return _repository.GetById(userId);
        }

        public virtual UserToken Add(UserToken entity)
        {
           return  _repository.Insert(entity);
        }

        public virtual void Delete(UserToken entity)
        {
            _repository.Delete(entity);
        }

        public virtual IQueryable<UserToken> GetAll()
        {
            return _repository.Table;
        }
       
       
    }
}
