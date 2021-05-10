using DataCenter.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace DataCenter.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class DataCenterController : AbpController
    {
        protected DataCenterController()
        {
            LocalizationResource = typeof(DataCenterResource);
        }
    }
}