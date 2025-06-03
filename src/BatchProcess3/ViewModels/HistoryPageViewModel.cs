using BatchProcess3.Data;

namespace BatchProcess3.ViewModels;

public partial class HistoryPageViewModel() : PageViewModel(ApplicationPageNames.History)
{
    public string Test { get; set; } = "History";
    
}