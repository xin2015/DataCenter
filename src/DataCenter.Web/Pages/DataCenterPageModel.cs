using DataCenter.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace DataCenter.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class DataCenterPageModel : AbpPageModel
    {
        protected DataCenterPageModel()
        {
            LocalizationResourceType = typeof(DataCenterResource);
        }
    }
}