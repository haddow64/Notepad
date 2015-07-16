using System;
using System.Configuration;
using System.Drawing.Printing;

namespace Notepad.Properties
{
    internal partial class Settings
    {
        [UserScopedSetting]
        [SettingsSerializeAs(SettingsSerializeAs.Binary)]
        public MoreSettings MoreSettings
        {
            get
            {
                var moreSettings = ((MoreSettings) this["MoreSettings"]);

                if (moreSettings == null)
                {
                    this["MoreSettings"] = moreSettings = new MoreSettings();
                }

                return moreSettings;
            }
        }
    }
}

namespace Notepad
{
    [Serializable]
    public class MoreSettings
    {
        public PrinterSettings PrinterSettings { get; set; }
        public PageSettings PageSettings { get; set; }
    }
}