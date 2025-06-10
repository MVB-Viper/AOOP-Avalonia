## 🧠 Observer Design Pattern (aka Publisher–Subscriber)

### 📌 Intent:

> Define a one-to-many dependency between objects so that when one object changes state, all its dependents are notified and updated automatically.

---

## 🧩 Problem:

You want to ensure that **multiple parts of an app stay in sync** when a core object’s state changes — **without tightly coupling** those parts.

Example problems:

- You have a `DataModel` that updates, and multiple UI components or services need to react to that change.
    
- You want components to **subscribe** to updates and **unsubscribe** when no longer needed.
    

---

## 🛠 Structure:

- **Subject (Observable)**: Holds state and notifies observers when it changes.
    
- **Observer**: Subscribes to the subject and gets notified of changes.
    
- **ConcreteSubject / ConcreteObserver**: Real implementations.
    

---

## 🔄 How it works:

- Observers **register** with the subject.
    
- When subject changes, it **notifies all observers** via a common interface.
    
- Observers decide how to react.
    

---

## ✅ C# Real-World Example: Avalonia MVVM

Suppose you’re building a notification system in Avalonia where:

- A `TimeService` tracks time and notifies UI views.
    
- Multiple `ClockViewModels` listen to the time service and update their clocks.
    

---

### 🔸 `ITimeObserver.cs`

```csharp
public interface ITimeObserver
{
    void OnTimeChanged(DateTime newTime);
}
```

---

### 🔸 `TimeService.cs` (Subject)

```csharp
public class TimeService
{
    private readonly List<ITimeObserver> _observers = new();

    private Timer? _timer;

    public void Start()
    {
        _timer = new Timer(_ =>
        {
            var currentTime = DateTime.Now;
            NotifyObservers(currentTime);
        }, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
    }

    public void Register(ITimeObserver observer) => _observers.Add(observer);
    public void Unregister(ITimeObserver observer) => _observers.Remove(observer);

    private void NotifyObservers(DateTime time)
    {
        foreach (var observer in _observers)
        {
            observer.OnTimeChanged(time);
        }
    }
}
```

---

### 🔸 `ClockViewModel.cs` (Observer)

```csharp
public class ClockViewModel : ITimeObserver, INotifyPropertyChanged
{
    private DateTime _currentTime;
    public DateTime CurrentTime
    {
        get => _currentTime;
        set
        {
            _currentTime = value;
            OnPropertyChanged(nameof(CurrentTime));
        }
    }

    public void OnTimeChanged(DateTime newTime)
    {
        CurrentTime = newTime;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
```

---

### 🔸 Usage in App Initialization

```csharp
var timeService = new TimeService();
var clock1 = new ClockViewModel();
var clock2 = new ClockViewModel();

timeService.Register(clock1);
timeService.Register(clock2);

timeService.Start();
```

---

## 🧠 Benefits:

- Promotes **loose coupling** between objects.
    
- Enables a **publish-subscribe** system that’s scalable and modular.
    
- UI updates or service reactions become **reactive**, not procedural.
    

---

## ⚠️ Downsides:

- Can lead to **memory leaks** if observers aren't unsubscribed (watch for this!).
    
- Complex dependency chains can become hard to debug.
    

---

## 🔁 Related Patterns:

- **Mediator**: Centralizes complex communication between observers.
    
- **Event Dispatcher**: A variation often used in UI frameworks.
    
- **Reactive Extensions (Rx)**: A modern take on the Observer pattern.
    
