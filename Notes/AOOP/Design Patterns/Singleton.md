
### 📌 **Purpose**

- Ensures that **only one instance** of a class exists throughout the application.
    
- Provides a **global access point** to that instance.
    

---

## 🧱 **Problem**

- In some situations (e.g., configuration, logging, database access), having multiple instances would cause issues like:
    
    - Conflicting data
        
    - Resource contention
        
    - Inconsistent behavior
        

You need to **control access** and ensure there’s only **one instance**, while still making it easily accessible.

---

## 💡 **Solution**

- Hide the constructor.
    
- Provide a **static method** or property that returns the single instance.
    
- Store that instance in a **static variable**.
    

---

## ✅ **Benefits**

- Controlled access to the sole instance.
    
- Reduces memory footprint in some use cases.
    
- Ensures consistency when a single shared resource is used.
    

---

## ⚠️ **Trade-offs**

- **Global state** can be an anti-pattern in large applications.
    
- Makes **unit testing harder** (tight coupling, can't easily mock).
    
- Breaks **Single Responsibility Principle** (handles both business logic and lifecycle).
    

---

## 🧰 **When to Use**

- When exactly **one instance** is needed (and enforced) across the application.
    
- When a class needs **centralized control** over a resource or service.
    

Examples:

- App configuration
    
- Logging services
    
- Caching
    
- Thread pools
    
- Window managers
    

---

## 🧩 **Structure**

1. **Private constructor** – prevents direct instantiation.
    
2. **Static field** – holds the one instance.
    
3. **Static method/property** – provides access to the instance.

## Code Example

```csharp
using System;
using System.Threading;

namespace Singleton
{
    // This Singleton implementation is called "double check lock". It is safe
    // in multithreaded environment and provides lazy initialization for the
    // Singleton object.
    class Singleton
    {
        private Singleton() { }

        private static Singleton _instance;

        // We now have a lock object that will be used to synchronize threads
        // during first access to the Singleton.
        private static readonly object _lock = new object();

        public static Singleton GetInstance(string value)
        {
            // This conditional is needed to prevent threads stumbling over the
            // lock once the instance is ready.
            if (_instance == null)
            {
                // Now, imagine that the program has just been launched. Since
                // there's no Singleton instance yet, multiple threads can
                // simultaneously pass the previous conditional and reach this
                // point almost at the same time. The first of them will acquire
                // lock and will proceed further, while the rest will wait here.
                lock (_lock)
                {
                    // The first thread to acquire the lock, reaches this
                    // conditional, goes inside and creates the Singleton
                    // instance. Once it leaves the lock block, a thread that
                    // might have been waiting for the lock release may then
                    // enter this section. But since the Singleton field is
                    // already initialized, the thread won't create a new
                    // object.
                    if (_instance == null)
                    {
                        _instance = new Singleton();
                        _instance.Value = value;
                    }
                }
            }
            return _instance;
        }

        // We'll use this property to prove that our Singleton really works.
        public string Value { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // The client code.
            
            Console.WriteLine(
                "{0}\n{1}\n\n{2}\n",
                "If you see the same value, then singleton was reused (yay!)",
                "If you see different values, then 2 singletons were created (booo!!)",
                "RESULT:"
            );
            
            Thread process1 = new Thread(() =>
            {
                TestSingleton("FOO");
            });
            Thread process2 = new Thread(() =>
            {
                TestSingleton("BAR");
            });
            
            process1.Start();
            process2.Start();
            
            process1.Join();
            process2.Join();
        }
        
        public static void TestSingleton(string value)
        {
            Singleton singleton = Singleton.GetInstance(value);
            Console.WriteLine(singleton.Value);
        } 
    }
}
```


## 🧠 **Key Takeaway**

> The Singleton pattern ensures a class has **one and only one instance**, while providing a **global point of access**—use it wisely to avoid unwanted global state.