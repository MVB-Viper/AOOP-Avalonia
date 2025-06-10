## 🎯 Command Design Pattern – Summary

### 📌 Purpose

The Command pattern is a behavioral design pattern that encapsulates a request as an object, thereby allowing for parameterization of clients with different requests, queuing or logging of requests, and support for undoable operations.([dofactory.com](https://www.dofactory.com/net/command-design-pattern?utm_source=chatgpt.com "C# Command Design Pattern - Dofactory.com"))

---

## 🧱 Problem

In GUI applications, such as a text editor, various user actions (like clicking buttons or selecting menu items) trigger different operations. Initially, you might handle these actions directly within the UI components. However, as the application grows, this approach leads to:

- Tight coupling between UI elements and business logic.
    
- Difficulty in reusing or extending functionalities.
    
- Challenges in implementing features like undo/redo.
    

---

## 💡 Solution

The Command pattern addresses these issues by:([reddit.com](https://www.reddit.com/r/csharp/comments/vgrzv1/help_me_understand_command_pattern/?utm_source=chatgpt.com "Help me understand Command Pattern : r/csharp - Reddit"))

- Encapsulating each request as a command object.
    
- Decoupling the object that invokes the operation from the one that knows how to perform it.
    
- Allowing for dynamic assignment, queuing, and logging of requests.([dotnettutorials.net](https://dotnettutorials.net/lesson/command-design-pattern/?utm_source=chatgpt.com "Command Design Pattern in C# - Dot Net Tutorials"), [medium.com](https://medium.com/elevate-salesforce/command-design-pattern-part-5-4bf2f2a60e5a?utm_source=chatgpt.com "Command: Design Pattern[Part-5] - Medium"), [dev.to](https://dev.to/hbolajraf/c-command-design-pattern-2c4i?utm_source=chatgpt.com "C# | Command Design Pattern - DEV Community"))
    

This approach promotes a cleaner separation of concerns and enhances the flexibility and maintainability of the codebase.

---

## ✅ Benefits

- **Decoupling**: Separates the sender of a request from its receiver.
    
- **Extensibility**: Easily add new commands without changing existing code.
    
- **Undo/Redo Support**: Implement reversible operations by storing command history.
    
- **Macro Commands**: Combine multiple commands into a single operation.([linkedin.com](https://www.linkedin.com/pulse/command-design-pattern-elevate-code-control-simplify-user-santilli?utm_source=chatgpt.com "\"Command Design Pattern\": Elevate Code Control and Flexibility ..."), [refactoring.guru](https://refactoring.guru/design-patterns/command/csharp/example?utm_source=chatgpt.com "Command in C# / Design Patterns - Refactoring.Guru"))
    

---

## ⚠️ Trade-offs

- **Increased Complexity**: Introduces additional classes for each command.
    
- **Overhead**: May be overkill for simple operations.
    

---

## 🧰 When to Use

- Implementing features like undo/redo.
    
- Queuing or logging requests.
    
- Parameterizing objects with operations.
    
- Decoupling UI components from business logic.([code-maze.com](https://code-maze.com/command/?utm_source=chatgpt.com "C# Design Patterns – Command - Code Maze"), [medium.com](https://medium.com/%40lexitrainerph/command-pattern-in-c-from-basics-to-advanced-29d954cafb92?utm_source=chatgpt.com "Command Pattern in C#: From Basics to Advanced - Medium"))
    

---

## 🧩 Structure

- **Command**: Declares an interface for executing operations.
    
- **ConcreteCommand**: Implements the Command interface and defines a binding between a Receiver and an action.
    
- **Receiver**: Knows how to perform the operation associated with the command.
    
- **Invoker**: Asks the command to carry out the request.
    
- **Client**: Creates a ConcreteCommand object and sets its receiver.([syncfusion.com](https://www.syncfusion.com/blogs/post/command-design-pattern-tutorial-with-csharp-examples?utm_source=chatgpt.com "Command Design Pattern Explained with C# Examples - Syncfusion"), [codeproject.com](https://www.codeproject.com/Articles/5377622/Command-Pattern-in-Csharp-What-You-Need-to-Impleme?utm_source=chatgpt.com "Command Pattern in C# – What You Need to Implement It"))
    

---

## 💻 C# Example: Text Editor Commands

Let's consider a simple text editor where users can perform operations like copy, paste, and undo.

### 1. **Command Interface**

```csharp
public interface ICommand
{
    void Execute();
    void Undo();
}
```

### 2. **Receiver**

```csharp
public class TextEditor
{
    public string Text { get; set; } = "";
    private string clipboard = "";

    public void Copy(string selectedText)
    {
        clipboard = selectedText;
        Console.WriteLine($"Copied '{clipboard}' to clipboard.");
    }

    public void Paste()
    {
        Text += clipboard;
        Console.WriteLine($"Pasted '{clipboard}' to text.");
    }

    public void UndoPaste()
    {
        if (Text.EndsWith(clipboard))
        {
            Text = Text.Substring(0, Text.Length - clipboard.Length);
            Console.WriteLine($"Undo paste. Current text: '{Text}'");
        }
    }
}
```

### 3. **Concrete Commands**

```csharp
public class CopyCommand : ICommand
{
    private TextEditor editor;
    private string selectedText;

    public CopyCommand(TextEditor editor, string selectedText)
    {
        this.editor = editor;
        this.selectedText = selectedText;
    }

    public void Execute()
    {
        editor.Copy(selectedText);
    }

    public void Undo()
    {
        // Copy operation might not need undo
    }
}

public class PasteCommand : ICommand
{
    private TextEditor editor;

    public PasteCommand(TextEditor editor)
    {
        this.editor = editor;
    }

    public void Execute()
    {
        editor.Paste();
    }

    public void Undo()
    {
        editor.UndoPaste();
    }
}
```

### 4. **Invoker**

```csharp
public class CommandInvoker
{
    private Stack<ICommand> commandHistory = new Stack<ICommand>();

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        commandHistory.Push(command);
    }

    public void Undo()
    {
        if (commandHistory.Count > 0)
        {
            ICommand command = commandHistory.Pop();
            command.Undo();
        }
    }
}
```

### 5. **Client**

```csharp
class Program
{
    static void Main(string[] args)
    {
        TextEditor editor = new TextEditor();
        CommandInvoker invoker = new CommandInvoker();

        ICommand copy = new CopyCommand(editor, "Hello");
        ICommand paste = new PasteCommand(editor);

        invoker.ExecuteCommand(copy);
        invoker.ExecuteCommand(paste);

        Console.WriteLine($"Current Text: {editor.Text}");

        invoker.Undo();
        Console.WriteLine($"After Undo: {editor.Text}");
    }
}
```

### 🧪 Output

```
Copied 'Hello' to clipboard.
Pasted 'Hello' to text.
Current Text: Hello
Undo paste. Current text: ''
After Undo: 
```

---

## 🧠 Key Takeaway

The Command pattern is instrumental in scenarios where you need to decouple the sender and receiver of requests, support undoable operations, or queue and log requests. By encapsulating requests as objects, it promotes flexibility and extensibility in software design.([linkedin.com](https://www.linkedin.com/pulse/command-design-pattern-elevate-code-control-simplify-user-santilli?utm_source=chatgpt.com "\"Command Design Pattern\": Elevate Code Control and Flexibility ..."))

---

If you're interested in seeing how this pattern integrates with frameworks like Avalonia or implementing features like macro commands and command history, feel free to ask!





# Avalonia Setup
Absolutely! Let's walk through how the **Command design pattern** applies in an **Avalonia MVVM** application — using **`ICommand`** to handle UI actions like button clicks in a decoupled, testable way.

---

## 🧭 Context: Avalonia MVVM and the Command Pattern

Avalonia (like WPF and other XAML frameworks) uses the **Command pattern** in UI binding via the `ICommand` interface. It allows UI elements (like `Button`) to trigger logic in the ViewModel without directly handling code in code-behind.

---

## 🧩 Use Case

Imagine a simple **To-Do app** in Avalonia:

- User types a task name.
    
- Clicks **Add Task**.
    
- The task is added to a list.
    

---

## ✅ Benefits of Using Command Pattern in Avalonia

- Keeps business logic in the **ViewModel**
    
- Keeps XAML clean and **code-behind empty**
    
- Makes features testable and **easy to reuse**
    

---

## 🧱 Structure in Avalonia

- **View (XAML)**: Binds to `AddCommand`
    
- **ViewModel**: Implements `ICommand` (often via `RelayCommand` or `ReactiveCommand`)
    
- **Model**: Represents data (e.g., a `TaskItem`)
    

---

## 💻 Code Example — Avalonia MVVM with Command Pattern

### 1. **Model**

```csharp
public class TaskItem
{
    public string Description { get; set; }
}
```

---

### 2. **RelayCommand** (Simple ICommand Helper)

```csharp
using System;
using System.Windows.Input;

public class RelayCommand : ICommand
{
    private readonly Action<object?> _execute;
    private readonly Predicate<object?>? _canExecute;

    public RelayCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;

    public void Execute(object? parameter) => _execute(parameter);

    public event EventHandler? CanExecuteChanged;
    public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}
```

---

### 3. **MainViewModel**

```csharp
using System.Collections.ObjectModel;
using System.ComponentModel;

public class MainViewModel : INotifyPropertyChanged
{
    private string _newTaskDescription;

    public string NewTaskDescription
    {
        get => _newTaskDescription;
        set
        {
            _newTaskDescription = value;
            OnPropertyChanged(nameof(NewTaskDescription));
        }
    }

    public ObservableCollection<TaskItem> Tasks { get; set; } = new();

    public RelayCommand AddTaskCommand { get; }

    public MainViewModel()
    {
        AddTaskCommand = new RelayCommand(
            execute: _ =>
            {
                if (!string.IsNullOrWhiteSpace(NewTaskDescription))
                {
                    Tasks.Add(new TaskItem { Description = NewTaskDescription });
                    NewTaskDescription = string.Empty;
                }
            },
            canExecute: _ => !string.IsNullOrWhiteSpace(NewTaskDescription)
        );
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
```

---

### 4. **View (MainWindow.axaml)**

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="https://github.com/avaloniaui"
        xmlns:vm="clr-namespace:YourApp"
        x:Class="YourApp.MainWindow"
        Width="400" Height="300"
        Title="Command Pattern Example">

    <Window.DataContext>
        <vm:MainViewModel />
    </Window.DataContext>

    <StackPanel Margin="20">
        <TextBox Text="{Binding NewTaskDescription, Mode=TwoWay}" Watermark="Enter a task..." />
        <Button Content="Add Task" Command="{Binding AddTaskCommand}" Margin="0,10,0,0"/>
        <ItemsControl Items="{Binding Tasks}">
            <ItemsControl.ItemTemplate>
                <DataTemplate>
                    <TextBlock Text="{Binding Description}" />
                </DataTemplate>
            </ItemsControl.ItemTemplate>
        </ItemsControl>
    </StackPanel>
</Window>
```

---

## 🧪 Output

1. User types a task → textbox updates `NewTaskDescription`.
    
2. User clicks “Add Task” → `AddTaskCommand.Execute()` runs.
    
3. Task is added to the list and displayed via `ItemsControl`.
    

---

## 🧠 Summary

|Concept|Avalonia Example|
|---|---|
|**Command**|`RelayCommand` implementing `ICommand`|
|**Receiver**|`MainViewModel` executes the logic|
|**Invoker**|The `Button` in XAML|
|**Client**|The ViewModel sets up the command binding|

---

Would you like to add **undo**, **remove command**, or integrate with **ReactiveUI's `ReactiveCommand`** for async tasks? I can help expand on that too!