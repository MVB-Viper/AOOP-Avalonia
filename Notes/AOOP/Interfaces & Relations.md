
# *Interface Definition*
An interface is used to define a *can-do* relationship. It is similar to an abstract class, but only specifies behavior and cannot include a implementation. A class can implement any number of interfaces. The implementing class must include all properties and methods defined by the interface.

## Interface Notes
- You cannot instantiate an interface, but you can reference an interface
- Interfaces can inherit from other interfaces
![[Interfaces.png]]


# *Aggregation Definition*
Aggregation is a *has-a* relationship between two classes where one class references the other instead of inheriting or implementing parts of the other class.

The "part" class can exist independently of the "whole class".

![[Aggregation.png]]


# *Composition Definition*
Composition is a *part-of* relationship between two classes where both classes are much more strongly linked. The "part" class cannot exist without the "whole class".

![[Composition.png]]