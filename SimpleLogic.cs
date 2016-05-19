using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODIF;

namespace SimpleLogic
{
    [PluginInfo(
        PluginName = "Simple Logic",
        PluginDescription = "",
        PluginID = 43,
        PluginAuthorName = "InputMapper",
        PluginAuthorEmail = "jhebbel@gmail.com",
        PluginAuthorURL = "http://inputmapper.com",
        PluginIconPath = @"pack://application:,,,/SimpleLogic;component/Resources/share-icon.png"
    )]
    [CompatibleTypes(
        InputTypes = mappingIOTypes.Double,
        OutputTypes = mappingIOTypes.Bool
    )]
    public class SimpleLogic : InputModificationPlugin
    {
        private Setting relOperator, compValue;

        public SimpleLogic(Guid guid) : base(guid)
        {
            this.Value = false;
            relOperator = new Setting("Operator", "Type of comparison to perform",
    SettingControl.Dropdown, SettingType.Text, "", true, true);
            relOperator.configuration.Add("options", new List<string>() { "=","<",">","<=",">=","!="});
            settings.settings.Add(relOperator);

            compValue = new Setting("Comparison Value", "", SettingControl.Numeric, SettingType.Decimal, 0d);
            compValue.configuration["interval"] = .1d;

            settings.settings.Add(compValue);
            this.DisplayData = "Open setting menu to setup";
            var test = this.settings;
        }
        public override void SetValue(dynamic inValue)
        {
            var test = this.settings;
            if (!String.IsNullOrWhiteSpace(relOperator.settingValue))
                this.DisplayData = inValue + " " + relOperator.settingValue + " " + compValue.settingValue;
            if (relOperator.settingValue == "=")
            {
                this.Value = (inValue == compValue.settingValue);
            }
            if (relOperator.settingValue == ">")
            {
                this.Value = (inValue > compValue.settingValue);
            }
            if (relOperator.settingValue == "<")
            {
                this.Value = (inValue < compValue.settingValue);
            }
            if (relOperator.settingValue == ">=")
            {
                this.Value = (inValue >= compValue.settingValue);
            }
            if (relOperator.settingValue == "<=")
            {
                this.Value = (inValue <= compValue.settingValue);
            }
            if (relOperator.settingValue == "!=")
            {
                this.Value = (inValue != compValue.settingValue);
            }
        }
    }
}
