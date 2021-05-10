using Volo.Abp.Settings;

namespace DataCenter.Settings
{
    public class DataCenterSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(DataCenterSettings.MySetting1));
        }
    }
}
