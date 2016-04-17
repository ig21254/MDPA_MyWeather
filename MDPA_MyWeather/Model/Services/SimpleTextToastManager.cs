
namespace MDPA_MyWeather.Model.Services
{
    using Windows.Data.Xml.Dom;
    using Windows.UI.Notifications;

    public sealed class SimpleTextToastManager
    {
        public static SimpleTextToastManager Instance { get; }
            = new SimpleTextToastManager();
        private SimpleTextToastManager() { }

        public void ShowToast(string line1, string line2 = null)
        {
            var template = $@"
                <toast>
                <visual>
                <binding template=""ToastGeneric"">
                  <text>{line1}</text>
                  <text>{line2}</text>
                </binding>
                </visual>
                </toast>
                ";
            var xml = new XmlDocument();
            xml.LoadXml(template);
            var toast = new ToastNotification(xml);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
