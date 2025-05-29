using BatchProcess3.Data;

namespace BatchProcess3.ViewModels;

public partial class MacrosPageViewModel : PageViewModel
{
    public string Test { get; set; } = "Macros";

    public MacrosPageViewModel()
    {
        PageName = ApplicationPageNames.Macros;
    }
}