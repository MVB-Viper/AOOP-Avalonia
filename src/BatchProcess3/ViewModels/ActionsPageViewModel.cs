using BatchProcess3.Data;

namespace BatchProcess3.ViewModels;

public partial class ActionsPageViewModel : PageViewModel
{
    public string Test { get; set; } = "Actions";

    public ActionsPageViewModel()
    {
        PageName = ApplicationPageNames.Actions;
    }
}