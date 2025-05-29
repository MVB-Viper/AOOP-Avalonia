using BatchProcess3.Data;

namespace BatchProcess3.ViewModels;

public partial class ReporterPageViewModel : PageViewModel
{
    public string Test { get; set; } = "Reporter";

    public ReporterPageViewModel()
    {
        PageName = ApplicationPageNames.Reporter;
    }
}