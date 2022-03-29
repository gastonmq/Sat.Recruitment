using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    public class BaseController<T> : ControllerBase where T: BaseController<T>
    {
        public readonly ILogger<T> _logger;

        public BaseController(ILogger<T> logger)
        {
            _logger = logger;
        }

        protected string GetExceptionInsights(Exception ex)
        {
            return string.Format("{0}\n{1}\n{2}", ex.Message, ex.StackTrace, DumpInnerExceptions(ex));
        }

        private string DumpInnerExceptions(Exception ex)
        {
            Exception innerException;
            StringBuilder sb = new StringBuilder();

            if (!(ex.InnerException is null))
            {
                sb.Append("\n\n Inner exception(s):\n");
                sb.Append(ex.Message);

                innerException = ex.InnerException;
                while (!(innerException is null))
                {
                    sb.Append(string.Format("{0}\n{1}\n\n", innerException.Message, innerException.StackTrace));
                    innerException = innerException.InnerException;
                }
            }

            return sb.ToString();
        }
    }
}
