using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace DataCenter.FileCrawlers
{
    public class FileCrawlerAlreadyExistsException : BusinessException
    {
        public FileCrawlerAlreadyExistsException(string code) : base(DataCenterDomainErrorCodes.FileCrawlerAlreadyExists)
        {
            WithData("code", code);
        }
    }
}
