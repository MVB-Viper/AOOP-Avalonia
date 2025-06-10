
### 📌 **Purpose**

- Defines an **interface for creating an object**, but lets subclasses decide **which class to instantiate**.
    
- Delays the instantiation to subclasses, promoting **loose coupling** and **extensibility**.
    

---

## 🧱 **Problem**

- You have code that creates objects directly using `new`, and this makes the code **hard to change or extend** (e.g. switching to different subclasses).
    
- Every time a new type is needed, the code must be modified—**violating the Open/Closed Principle**.
    

---

## 💡 **Solution**

- Create a **factory method** in a base class.
    
- Let **subclasses override** this method to return different concrete products.
    
- The core logic uses the factory method without knowing the specific subclass of the object being created.
    

---

## ✅ **Benefits**

- Adheres to **Open/Closed Principle**: you can add new products without changing the creator code.
    
- Reduces coupling between product creator and product types.
    
- Centralizes object creation logic.
    

---

## ⚠️ **Trade-offs**

- Can introduce many classes if each product type needs a separate subclass of the creator.
    
- Adds complexity due to indirection.
    

---

## 🧰 **When to Use**

- When you don’t know the exact type of object your code should create.
    
- When a class wants its subclasses to specify the object it creates.
    
- When object creation should be independent of the system that uses the object.
    

---

## 🧩 **Structure**

1. **Creator (abstract class)** – declares the factory method.
    
2. **Concrete Creators** – override the factory method to create specific products.
    
3. **Product Interface** – common interface for all products.
    
4. **Concrete Products** – different implementations of the product interface.
    
5. **Client** – works with products through their interface and uses creators to obtain them.
    

---

## 💻 **C# Example**

### 🎬 Scenario: Logistics Application

Let’s say you’re building an app that manages deliveries via different transport types (Truck, Ship, etc.).

```csharp
// Product Interface
interface ITransport {
    void Deliver();
}

// Concrete Products
class Truck : ITransport {
    public void Deliver() {
        Console.WriteLine("Delivering by land in a box.");
    }
}

class Ship : ITransport {
    public void Deliver() {
        Console.WriteLine("Delivering by sea in a container.");
    }
}

// Creator
abstract class Logistics {
    public void PlanDelivery() {
        // Calls the factory method
        ITransport transport = CreateTransport();
        transport.Deliver();
    }

    // Factory Method
    public abstract ITransport CreateTransport();
}

// Concrete Creators
class RoadLogistics : Logistics {
    public override ITransport CreateTransport() {
        return new Truck();
    }
}

class SeaLogistics : Logistics {
    public override ITransport CreateTransport() {
        return new Ship();
    }
}

// Client Code
class Program {
    static void Main() {
        Logistics logistics;

        Console.WriteLine("Using road logistics:");
        logistics = new RoadLogistics();
        logistics.PlanDelivery();

        Console.WriteLine();

        Console.WriteLine("Using sea logistics:");
        logistics = new SeaLogistics();
        logistics.PlanDelivery();
    }
}
```

---

## 🧠 **Key Takeaway**

> The Factory Method pattern helps you **defer object creation to subclasses**, allowing the system to be extended more easily and maintain loose coupling between the creator and the product types.

---