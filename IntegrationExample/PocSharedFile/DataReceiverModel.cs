using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocSharedFile
{
    public class DataReceiverModel
    {
        public string? FvIntentActionName { get; set; }
        public string? Body { get; set; }
        public string? Response { get; set; }
        public string? Token { get; set; }
        public string? FvPackage { get; set; }
        public string? FvClassName { get; set; }
        public string? RequestInfo { get; set; }
        public string? Code { get; set; }
        public string? ServiceName { get; set; }
        public int? RedirectService { get; set; }
        public int? StatusCode { get; set; }
        public int? ErrorCode { get; set; }
        public string? ExceptionMessage { get; set; }
        public string[]? IncludeOptions { get; set; }
        public string? ErrorCodeDescription { get; set; }
        public string? ReasonPhrase { get; set; }
    }
}
