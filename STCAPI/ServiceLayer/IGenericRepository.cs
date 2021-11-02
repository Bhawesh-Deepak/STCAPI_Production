using STCAPI.ReqRespVm;
using System;
using System.Threading.Tasks;

namespace STCAPI.ServiceLayer
{
    public interface IGenericRepository<TEntity,T> where TEntity:class
    {
        Task<ResponseModel<TEntity>> GetAllEntities(Func<TEntity, bool> where);
        Task<ResponseModel<TEntity>> CreateEntity(TEntity model);
        Task<ResponseModel<TEntity>> CreateEntities(TEntity[] model);
        Task<ResponseModel<TEntity>> UpdateEntity(TEntity model);
        Task<ResponseModel<TEntity>> UpdateEntities(TEntity[] model);
        Task<ResponseModel<TEntity>> DeleteEntity(TEntity items);
        Task<ResponseModel<TEntity>> DeleteEntities(params TEntity[] items);
        Task<ResponseModel<TEntity>> CheckIsExists(Func<TEntity, bool> where);
    }
}
