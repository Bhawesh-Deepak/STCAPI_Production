using System.Collections.Generic;
using System.Net;

namespace STCAPI.ReqRespVm
{
    public class ResponseModel<TEntity> where TEntity:class
    {
        public IEnumerable<TEntity> TEntities { get; set; }
        public TEntity Entity { get; set; }
        public string Message { get; set; }
        public HttpStatusCode ResponseStatus { get; set; }

        public ResponseModel(TEntity entity, List<TEntity> entities, string message, HttpStatusCode responseStatus)
        {
            TEntities = entities;
            Entity = entity;
            Message = message;
            ResponseStatus = responseStatus;
        }
        public ResponseModel() { }
    }
}
