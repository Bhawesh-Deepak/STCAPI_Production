using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using STCAPI.DataLayer;
using STCAPI.ReqRespVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace STCAPI.ServiceLayer
{
    public class GenericImplementation<TEntity, T> : IGenericRepository<TEntity, T> where TEntity : class
    {
        private STCContext context;
        private DbSet<TEntity> TEntities;

        public GenericImplementation(IConfiguration configuration)
        {
            context = new STCContext(configuration);
            TEntities = context.Set<TEntity>();

        }
        public async Task<ResponseModel<TEntity>> CheckIsExists(Func<TEntity, bool> where)
        {
            try
            {
                TEntity item = null;
                IQueryable<TEntity> dbQuery = context.Set<TEntity>();
                item = dbQuery.AsNoTracking().FirstOrDefault(where);
                return await Task.Run(() => new ResponseModel<TEntity>(item, null,
                    "success", HttpStatusCode.OK));
            }
            catch (Exception ex)
            {
                return await Task.Run(() => new ResponseModel<TEntity>(null, null,
                    ex.Message, HttpStatusCode.InternalServerError));
            }

        }

        public async Task<ResponseModel<TEntity>> CreateEntities(TEntity[] model)
        {
            try
            {
                await TEntities.AddRangeAsync(model);
                await context.SaveChangesAsync();
                return new ResponseModel<TEntity>(null, null, "Created", HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("The duplicate key "))
                {
                    return new ResponseModel<TEntity>(null, null, "Already Exists", HttpStatusCode.BadRequest);
                }
                return new ResponseModel<TEntity>(null, null, ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ResponseModel<TEntity>> CreateEntity(TEntity model)
        {
            try
            {
                await TEntities.AddAsync(model);
                await context.SaveChangesAsync();
                return new ResponseModel<TEntity>(null, null, "Created", HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("The duplicate key "))
                {
                    return new ResponseModel<TEntity>(null, null, "Already Exists", HttpStatusCode.BadRequest);
                }
                return new ResponseModel<TEntity>(null, null, ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ResponseModel<TEntity>> DeleteEntities(params TEntity[] items)
        {
            try
            {
                context.UpdateRange(items);
                await context.SaveChangesAsync();
                return new ResponseModel<TEntity>(null, null, "Deleted Successfully..", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new ResponseModel<TEntity>(null, null, ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ResponseModel<TEntity>> DeleteEntity(TEntity items)
        {
            try
            {
                context.Update(items);
                await context.SaveChangesAsync();
                return new ResponseModel<TEntity>(null, null, "Deleted Successfully..", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new ResponseModel<TEntity>(null, null, ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ResponseModel<TEntity>> GetAllEntities(Func<TEntity, bool> where)
        {
            try
            {
                IQueryable<TEntity> dbQuery = context.Set<TEntity>();
                var tList = dbQuery.AsNoTracking().Where(where).ToList<TEntity>();
                return await Task.Run(() => new ResponseModel<TEntity>(null, tList, "success", HttpStatusCode.OK));
            }
            catch (Exception ex)
            {
                return new ResponseModel<TEntity>(null, null, ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ResponseModel<TEntity>> UpdateEntities(TEntity[] model)
        {
            try
            {
                context.UpdateRange(model);
                await context.SaveChangesAsync();
                return new ResponseModel<TEntity>(null, null, "Updated", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new ResponseModel<TEntity>(null, null, ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ResponseModel<TEntity>> UpdateEntity(TEntity model)
        {
            try
            {
                context.Update(model);
                await context.SaveChangesAsync();
                return new ResponseModel<TEntity>(null, null, "Updated", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new ResponseModel<TEntity>(null, null, ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
