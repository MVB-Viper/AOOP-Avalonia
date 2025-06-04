using BatchProcess3.ViewModels;

namespace BatchProcess3.Interface;

public interface IDialogProvider
{
    DialogViewModel Dialog { get; set; }
}