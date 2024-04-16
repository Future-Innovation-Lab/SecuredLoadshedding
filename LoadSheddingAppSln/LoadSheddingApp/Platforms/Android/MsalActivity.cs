using Android.App;
using Android.Content;
using Microsoft.Identity.Client;

namespace LoadSheddingApp.Platforms.Android
{
    [Activity(Exported = true)]
    [IntentFilter(new[] { Intent.ActionView },
     Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault },
     DataHost = "auth",
     DataScheme = "msalab1be50a-3053-4c62-bb05-87dc0d63a3b8")]
    public class MsalActivity : BrowserTabActivity
    {
    }
}
