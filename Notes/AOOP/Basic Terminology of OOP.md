
**Object**
- Small manageable program pieces which can change their State (Attributes) according to their Behaviors (Methods)

**Class**
- A template for an object (Example: Class could be a ball, and the objects would be red ball, blue ball etc..)

**Attribute**
- Things that describe an object (Color, Size, Id)

**Behaviors (Methods)**
- Actions or Operations that the object can perform (Get Item, Jump, Roll)

**Constructors**
- The function called when an instance is made of the class
- A class can have multiple, that require different inputs

```cs

class User {
	// Attributes
	private string userName; // A Field

	private string email;

	private string role;

	
	
	public User() {} // A Constructor (if missing would be default)


	// Methods (Behaviors)
	
	public void SetUserName(string username) { // A method, specifically a setter
		this.userName = username;
	}


	public string GetUserName() { // A method, specifically a getter
		return this.Speed;
	}

	public void Login()
	{
		// Logic to login
	}
	
}

```


**Properties**
- Fields with a connected getter and setter function.


```cs

public class Note {
	// Properties (ways to declare)
	public int Pitch { get; set; }
	// Short hand expression
	public decimal Worth
	{
		get => currentPrice * sharesOwned;
		get => sharesOwned = value / currentPrice;
	}

	// Read only properties
	public int Pitch { get ; } = 20;
	public int Duration { get ; } = 100;

	// Init-only properties (Can only be set only when initialized in constructor)
	public int Pitch { get ; init } = 20;
	public int Duration { get ; init } = 100;

	// only internally mutable
	public int Pitch { get ; 
					private set; } = 20;
	public int Duration { get ; 
					private set; } = 100;


```


**Abstract Class**
- An Abstract Class cannot be instantiated, only its subclasses can be. Any abstract methods have to be implemented by the sub classes

