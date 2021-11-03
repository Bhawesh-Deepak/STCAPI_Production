using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STCAPI.Helpers
{
    public class SubsidryErrorDetail
    {
        public string PropertyName { get; set; }
        public string ErrorDetail { get; set; }

        public List<SubsidryErrorDetail> GetSubsidryErrorDetails(IDictionary<int, (string, string)> error)
        {
            var models = new List<SubsidryErrorDetail>();
            foreach (var data in error)
            {
                var model = new SubsidryErrorDetail();
                model.PropertyName = data.Value.Item1;
                model.ErrorDetail = data.Value.Item2;

                models.Add(model);
            }
            return models;
        }
    }
}
