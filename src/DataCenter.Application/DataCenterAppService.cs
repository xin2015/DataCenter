using System;
using System.Collections.Generic;
using System.Text;
using DataCenter.Localization;
using Volo.Abp.Application.Services;

namespace DataCenter
{
    /* Inherit your application services from this class.
     */
    public abstract class DataCenterAppService : ApplicationService
    {
        protected DataCenterAppService()
        {
            LocalizationResource = typeof(DataCenterResource);
        }
    }
}
