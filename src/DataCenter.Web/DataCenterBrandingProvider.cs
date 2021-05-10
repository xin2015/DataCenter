using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace DataCenter.Web
{
    [Dependency(ReplaceServices = true)]
    public class DataCenterBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "DataCenter";
    }
}
