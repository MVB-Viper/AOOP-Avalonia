
## 🎯 **Strategy Design Pattern - Summary**

### 📌 **Purpose**

- Allows you to **define a family of algorithms**, put each in a separate class, and make them **interchangeable**.
    
- The strategy pattern lets the algorithm vary independently from the clients that use it.
    

---

## 🧱 **Problem**

- When you have **multiple versions of an algorithm**, and you embed conditional logic (e.g. `if/else` or `switch`) all over your code to handle them.
    
- This makes your code **hard to maintain**, extend, or reuse.
    

---

## 💡 **Solution**

- Extract the algorithm logic into **separate classes** (strategies).
    
- Let the client choose or inject the appropriate **strategy** at runtime.
    
- The client works with a **common interface**, unaware of the specific implementation.
    

---

## ✅ **Benefits**

- Cleaner, more maintainable code.
    
- Avoids duplication of control logic (e.g., `if` trees).
    
- Makes algorithms **easier to test and extend**.
    
- Promotes **Open/Closed Principle**: Add new strategies without modifying existing code.
    

---

## ⚠️ **Trade-offs**

- Adds more classes and complexity.
    
- The client must be aware of different strategies and manage them.
    

---

## 🧰 **When to Use**

- When you have a set of related algorithms and need to switch between them dynamically.
    
- When you want to avoid a bloated class with many conditional statements.
    
- Common in **sorting**, **payment processing**, **discount policies**, **validation strategies**, **compression formats**, etc.
    

---

## 🧩 **Structure**

1. **Context** – Uses a strategy.
    
2. **Strategy Interface** – Common interface for all algorithms.
    
3. **Concrete Strategies** – Different implementations of the algorithm.
    

---

## 💻 **C# Real-World Example: Payment Processing**

Imagine an e-commerce platform where a customer can pay with **Credit Card**, **PayPal**, or **Crypto**.

---

### Step 1: Strategy Interface

```csharp
public interface IPaymentStrategy {
    void Pay(decimal amount);
}
```

---

### Step 2: Concrete Strategies

```csharp
public class CreditCardPayment : IPaymentStrategy {
    public void Pay(decimal amount) {
        Console.WriteLine($"Paid {amount:C} using Credit Card.");
    }
}

public class PayPalPayment : IPaymentStrategy {
    public void Pay(decimal amount) {
        Console.WriteLine($"Paid {amount:C} using PayPal.");
    }
}

public class CryptoPayment : IPaymentStrategy {
    public void Pay(decimal amount) {
        Console.WriteLine($"Paid {amount:C} using Cryptocurrency.");
    }
}
```

---

### Step 3: Context Class

```csharp
public class Checkout {
    private IPaymentStrategy _paymentStrategy;

    public Checkout(IPaymentStrategy paymentStrategy) {
        _paymentStrategy = paymentStrategy;
    }

    public void SetPaymentStrategy(IPaymentStrategy strategy) {
        _paymentStrategy = strategy;
    }

    public void ProcessOrder(decimal amount) {
        Console.WriteLine("Processing order...");
        _paymentStrategy.Pay(amount);
    }
}
```

---

### Step 4: Client Code

```csharp
class Program {
    static void Main() {
        decimal amount = 125.50m;

        // Pay using Credit Card
        var checkout = new Checkout(new CreditCardPayment());
        checkout.ProcessOrder(amount);

        Console.WriteLine();

        // Switch to PayPal
        checkout.SetPaymentStrategy(new PayPalPayment());
        checkout.ProcessOrder(amount);

        Console.WriteLine();

        // Switch to Crypto
        checkout.SetPaymentStrategy(new CryptoPayment());
        checkout.ProcessOrder(amount);
    }
}
```

---

### 🧪 Output

```
Processing order...
Paid $125.50 using Credit Card.

Processing order...
Paid $125.50 using PayPal.

Processing order...
Paid $125.50 using Cryptocurrency.
```

---

## 🧠 **Key Takeaway**

> The Strategy Pattern helps you **swap logic dynamically** without modifying the classes that use it—keeping code clean, extensible, and adhering to solid design principles.

---