using Volo.Abp.Settings;

namespace Tank.Financing.Settings;

public class FinancingSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(FinancingSettings.MySetting1));
    }
}
