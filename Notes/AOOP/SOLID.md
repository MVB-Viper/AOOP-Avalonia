
# SOLID Principles with C# Examples

The **SOLID** principles are five design principles intended to make software designs more understandable, flexible, and maintainable.

## 1. **Single Responsibility Principle (SRP)**

**Definition**: A class should have one, and only one, reason to change. This means a class should have only one job or responsibility.

**Example**:

```csharp
// BAD: One class handling multiple responsibilities
public class Report
{
    public string Title { get; set; }

    public void GenerateReport()
    {
        // Generate report logic
    }

    public void SaveToFile(string path)
    {
        // Saving logic
    }
}
```

```csharp
// GOOD: Each class has a single responsibility
public class Report
{
    public string Title { get; set; }

    public string GenerateReport()
    {
        return $"Report: {Title}";
    }
}

public class ReportSaver
{
    public void SaveToFile(string reportContent, string path)
    {
        // File saving logic
    }
}
```

---

## 2. **Open/Closed Principle (OCP)**

**Definition**: Software entities (classes, modules, functions, etc.) should be open for extension but closed for modification.

**Example**:

```csharp
// BAD: Modifying class to support new types
public class DiscountCalculator
{
    public double CalculateDiscount(string customerType)
    {
        if (customerType == "Regular")
            return 0.1;
        else if (customerType == "Premium")
            return 0.2;
        else
            return 0;
    }
}
```

```csharp
// GOOD: Extendable via inheritance
public interface IDiscountStrategy
{
    double GetDiscount();
}

public class RegularCustomerDiscount : IDiscountStrategy
{
    public double GetDiscount() => 0.1;
}

public class PremiumCustomerDiscount : IDiscountStrategy
{
    public double GetDiscount() => 0.2;
}

public class DiscountCalculator
{
    public double CalculateDiscount(IDiscountStrategy strategy)
    {
        return strategy.GetDiscount();
    }
}
```

---

## 3. **Liskov Substitution Principle (LSP)**

**Definition**: Objects of a superclass should be replaceable with objects of its subclasses without altering the correctness of the program.

**Example**:

```csharp
// BAD: Subclass breaks behavior of base class
public class Bird
{
    public virtual void Fly() => Console.WriteLine("Flying");
}

public class Ostrich : Bird
{
    public override void Fly()
    {
        throw new InvalidOperationException("Ostriches can't fly");
    }
}
```

```csharp
// GOOD: Use interfaces to model capabilities
public interface IBird
{
    void Eat();
}

public interface IFlyingBird : IBird
{
    void Fly();
}

public class Sparrow : IFlyingBird
{
    public void Eat() => Console.WriteLine("Sparrow eating");
    public void Fly() => Console.WriteLine("Sparrow flying");
}

public class Ostrich : IBird
{
    public void Eat() => Console.WriteLine("Ostrich eating");
}
```

---

## 4. **Interface Segregation Principle (ISP)**

**Definition**: Clients should not be forced to depend on interfaces they do not use.

**Example**:

```csharp
// BAD: One interface for everything
public interface IWorker
{
    void Work();
    void Eat();
}

public class Robot : IWorker
{
    public void Work() => Console.WriteLine("Robot working");
    public void Eat() => throw new NotImplementedException();
}
```

```csharp
// GOOD: Split interfaces
public interface IWorkable
{
    void Work();
}

public interface IEatable
{
    void Eat();
}

public class Human : IWorkable, IEatable
{
    public void Work() => Console.WriteLine("Human working");
    public void Eat() => Console.WriteLine("Human eating");
}

public class Robot : IWorkable
{
    public void Work() => Console.WriteLine("Robot working");
}
```

---

## 5. **Dependency Inversion Principle (DIP)**

**Definition**: High-level modules should not depend on low-level modules. Both should depend on abstractions.

**Example**:

```csharp
// BAD: High-level module depends on low-level module
public class FileLogger
{
    public void Log(string message)
    {
        // Write to file
    }
}

public class Application
{
    private FileLogger _logger = new FileLogger();

    public void Run()
    {
        _logger.Log("Running application...");
    }
}
```

```csharp
// GOOD: Depend on abstraction
public interface ILogger
{
    void Log(string message);
}

public class FileLogger : ILogger
{
    public void Log(string message)
    {
        // Write to file
    }
}

public class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine(message);
    }
}

public class Application
{
    private readonly ILogger _logger;

    public Application(ILogger logger)
    {
        _logger = logger;
    }

    public void Run()
    {
        _logger.Log("Running application...");
    }
}
```
