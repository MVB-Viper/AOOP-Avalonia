﻿using BatchProcess3.Data;

namespace BatchProcess3.ViewModels;

public partial class MacrosPageViewModel() : PageViewModel(ApplicationPageNames.Macros)
{
    public string Test { get; set; } = "Macros";
}