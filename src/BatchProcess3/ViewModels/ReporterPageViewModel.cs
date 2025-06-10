using BatchProcess3.Data;

namespace BatchProcess3.ViewModels;

public partial class ReporterPageViewModel() : PageViewModel(ApplicationPageNames.Reporter)
{
    public string Test { get; set; } = "Reporter";
    
}