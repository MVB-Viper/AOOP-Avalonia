using BatchProcess3.Data;

namespace BatchProcess3.ViewModels;

public partial class HistoryPageViewModel : PageViewModel
{
    public string Test { get; set; } = "History";

    public HistoryPageViewModel()
    {
        PageName = ApplicationPageNames.History;
    }
}