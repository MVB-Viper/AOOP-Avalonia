C# is a powerful object-oriented programming (OOP) language that is built on four key principles: **Abstraction**, **Encapsulation**, **Inheritance**, and **Polymorphism**. These principles help you design software that is modular, reusable, and easier to maintain.
## 1. **Abstraction**

**Microsoft Definition**
Modeling the relevant attributes and interactions of entities as classes to define an abstract representation of a system.

**Definition**:  
Abstraction is the process of modeling relevant attributes and behaviors of an object while hiding unnecessary details. In C#, this is often achieved using **abstract classes** or **interfaces**.

### ✅ Example:

```csharp
// Abstract class defines the abstraction of a shape
public abstract class Shape
{
    public string Color { get; set; }

    public abstract double GetArea();
}

// Concrete class implements the abstraction
public class Circle : Shape
{
    public double Radius { get; set; }

    public override double GetArea()
    {
        return Math.PI * Radius * Radius;
    }
}
```

🧠 **Key Point**: The `Shape` class provides an abstract definition, and concrete classes like `Circle` implement the relevant details.

---

## 2. **Encapsulation**

**Microsoft Definition**
Hiding the internal state and functionality of an object and only allowing access through a public set of functions.

**Definition**:  
Encapsulation is about **hiding internal data** and logic from external access. This is done using **private fields** and **public properties or methods** to control access.

**How does it work**
You can add an access modifier to the declaration:
- **public**: From anywhere
- **Internal**: only inside assembly
- **private**: only within class (or struct)
- **protected**: only within subclasses (and containing classes)
### ✅ Example:

```csharp
public class BankAccount
{
    private decimal _balance;

    public decimal Balance 
    {
        get { return _balance; }
        private set { _balance = value; }
    }

    public BankAccount(decimal initialBalance)
    {
        _balance = initialBalance;
    }

    public void Deposit(decimal amount)
    {
        if (amount > 0)
            _balance += amount;
    }

    public bool Withdraw(decimal amount)
    {
        if (amount <= _balance)
        {
            _balance -= amount;
            return true;
        }

        return false;
    }
}
```

🧠 **Key Point**: Direct access to the `_balance` field is restricted. Changes are controlled via public methods.

---

## 3. **Inheritance**

**Microsoft Definition**
Ability to create new abstractions based on existing abstractions

**Definition**:  
Inheritance allows a class to acquire the properties and behaviors of another class, enabling **code reuse and hierarchy creation**. It is used as "is-a" relation in C# (A Dog *is a* Animal)

### ✅ Example:

```csharp
public class Animal
{
    public string Name { get; set; }

    public void Eat()
    {
        Console.WriteLine($"{Name} is eating.");
    }
}

// Dog inherits from Animal
public class Dog : Animal
{
    public void Bark()
    {
        Console.WriteLine($"{Name} is barking.");
    }
}
```

```csharp
// Usage
var dog = new Dog { Name = "Buddy" };
dog.Eat();  // From base class
dog.Bark(); // From derived class
```

🧠 **Key Point**: The `Dog` class inherits common behavior (`Eat`) from `Animal`, and adds specific behavior (`Bark`).

---

## 4. **Polymorphism**

**Microsoft Definition**
Ability to implement inherited properties or methods in different ways across multiple abstractions

**Definition**:  
Polymorphism allows objects to be treated as instances of their base class while exhibiting **different behavior** based on the actual object type. It can be achieved through **method overriding** (runtime polymorphism) or **method overloading** (compile-time polymorphism).

### ✅ Example: Runtime Polymorphism (via method overriding)

```csharp
public class Animal
{
    public virtual void Speak()
    {
        Console.WriteLine("Animal sound");
    }
}

public class Dog : Animal
{
    public override void Speak()
    {
        Console.WriteLine("Woof");
    }
}

public class Cat : Animal
{
    public override void Speak()
    {
        Console.WriteLine("Meow");
    }
}
```

```csharp
// Usage
List<Animal> animals = new List<Animal>
{
    new Dog(),
    new Cat()
};

foreach (var animal in animals)
{
    animal.Speak(); // Output depends on the actual type (Dog or Cat)
}
```

🧠 **Key Point**: The `Speak()` method behaves differently depending on the actual object instance.

---