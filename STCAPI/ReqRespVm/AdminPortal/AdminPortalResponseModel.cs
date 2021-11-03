using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STCAPI.ReqRespVm.AdminPortal
{
    public class AdminPortalResponseModel
    {
        public string userName { get; set; }
        public string directory { get; set; }
        public List<Stage> stages { get; set; }
    }

    public class Form
    {
        public bool accessLevel { get; set; }
        public object name { get; set; }
    }

    public class Dashboard
    {
        public bool accessLevel { get; set; }
        public string name { get; set; }
    }

    public class Report
    {
        public bool accessLevel { get; set; }
        public string name { get; set; }
    }

    public class SubStream
    {
        public string subStreamName { get; set; }
        public bool accessLevel { get; set; }
        public List<Form> form { get; set; }
        public List<Dashboard> dashboard { get; set; }
        public List<Report> report { get; set; }
    }

    public class MainStream
    {
        public string streamName { get; set; }
        public bool accessLevel { get; set; }
        public List<SubStream> subStream { get; set; }
    }

    public class Stage
    {
        public string stageName { get; set; }
        public bool accessLevel { get; set; }
        public List<MainStream> mainStream { get; set; }
    }
}
