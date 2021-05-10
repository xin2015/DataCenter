using DataCenter.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace DataCenter.Permissions
{
    public class DataCenterPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(DataCenterPermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(DataCenterPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<DataCenterResource>(name);
        }
    }
}
