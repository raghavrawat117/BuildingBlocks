Excellent. The **Prototype Pattern** completes the five Gang of Four (GoF) Creational Patterns and introduces a different approach to object creation:

> Instead of creating objects using `new`, create new objects by **cloning an existing object (prototype)**.

***

# Prototype Pattern

## Definition

The Prototype Pattern creates new objects by copying an existing object, known as the prototype.

This is useful when object creation is expensive or complex, and creating a copy is more efficient than building from scratch.

***

# Real-World Example

Imagine a character creation system in a game.

Creating a character might involve:

* Loading skills
* Loading equipment
* Setting attributes
* Applying configurations

Instead of building every character from the beginning:

```csharp
var warrior = new Character(...many configurations...);
```

you can create a base warrior and clone it:

```csharp
var warrior1 = warriorPrototype.Clone();
var warrior2 = warriorPrototype.Clone();
```

***

# Simple Console Application Example

## Step 1: Prototype Interface

### IPrototype.cs

```csharp
namespace PrototypePatternDemo
{
    public interface IPrototype<T>
    {
        T Clone();
    }
}
```

***

## Step 2: Concrete Prototype

### Employee.cs

```csharp
using System;

namespace PrototypePatternDemo
{
    public class Employee : IPrototype<Employee>
    {
        public string Name { get; set; }
        public string Department { get; set; }

        public Employee Clone()
        {
            return (Employee)this.MemberwiseClone();
        }

        public void Display()
        {
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Department: {Department}");
            Console.WriteLine();
        }
    }
}
```

***

## Step 3: Client

### Program.cs

```csharp
using System;

namespace PrototypePatternDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee original = new Employee
            {
                Name = "Raghav",
                Department = "Engineering"
            };

            Employee clone = original.Clone();

            clone.Name = "Amit";

            Console.WriteLine("Original Employee");
            original.Display();

            Console.WriteLine("Cloned Employee");
            clone.Display();

            Console.ReadKey();
        }
    }
}
```

***

# Sample Output

```text
Original Employee
Name: Raghav
Department: Engineering

Cloned Employee
Name: Amit
Department: Engineering
```

The cloned object starts with the same values as the original but can be modified independently.

***

# Understanding `MemberwiseClone()`

`MemberwiseClone()` creates a shallow copy of an object.

```csharp
Employee clone = (Employee)this.MemberwiseClone();
```

It copies:

✅ Value types

✅ References to reference-type objects

It does **not** recursively copy nested objects.

***

# Shallow Copy Problem

Consider this class:

```csharp
public class Address
{
    public string City { get; set; }
}
```

```csharp
public class Employee
{
    public string Name { get; set; }
    public Address Address { get; set; }

    public Employee Clone()
    {
        return (Employee)this.MemberwiseClone();
    }
}
```

Client:

```csharp
Employee emp1 = new Employee
{
    Name = "Raghav",
    Address = new Address
    {
        City = "Noida"
    }
};

Employee emp2 = emp1.Clone();

emp2.Address.City = "Mumbai";
```

Result:

```text
emp1.Address.City = Mumbai
emp2.Address.City = Mumbai
```

Why?

Because both employees reference the same `Address` object.

***

# Deep Copy Solution

To fully clone nested objects:

### Address.cs

```csharp
public class Address
{
    public string City { get; set; }

    public Address Clone()
    {
        return new Address
        {
            City = this.City
        };
    }
}
```

### Employee.cs

```csharp
public class Employee
{
    public string Name { get; set; }
    public Address Address { get; set; }

    public Employee Clone()
    {
        return new Employee
        {
            Name = this.Name,
            Address = this.Address.Clone()
        };
    }
}
```

Now:

```csharp
emp2.Address.City = "Mumbai";
```

Output:

```text
emp1.Address.City = Noida
emp2.Address.City = Mumbai
```

Each object has its own copy.

***

# More Realistic Example

Suppose generating reports is expensive.

### Report.cs

```csharp
using System;

namespace PrototypePatternDemo
{
    public class Report
    {
        public string Header { get; set; }
        public string Footer { get; set; }

        public Report Clone()
        {
            return (Report)MemberwiseClone();
        }

        public void Print()
        {
            Console.WriteLine($"Header : {Header}");
            Console.WriteLine($"Footer : {Footer}");
        }
    }
}
```

Client:

```csharp
Report template = new Report
{
    Header = "Monthly Sales Report",
    Footer = "Confidential"
};

Report report1 = template.Clone();
Report report2 = template.Clone();

report1.Print();
report2.Print();
```

Instead of recreating the report structure each time, we clone an existing template.

***

# Real-World Uses

Common scenarios:

* Document templates
* UI control templates
* Game character templates
* Configuration objects
* Workflow definitions
* Report templates

***

# Prototype vs Builder

### Builder

Creates objects step-by-step.

```csharp
var computer = new ComputerBuilder()
                    .SetCpu("i9")
                    .SetRam("32GB")
                    .Build();
```

### Prototype

Copies an existing object.

```csharp
var computer2 = computer1.Clone();
```

***

# Prototype vs Factory

### Factory

```csharp
var car = CarFactory.CreateSportsCar();
```

Creates a brand-new object.

### Prototype

```csharp
var car2 = car1.Clone();
```

Creates a copy of an existing object.

***

# UML (Simplified)

```text
        +----------------+
        |   Prototype    |
        +----------------+
        | + Clone()      |
        +----------------+
                 ▲
                 |
        +----------------+
        |   Employee     |
        +----------------+
        | Name           |
        | Department     |
        +----------------+
        | Clone()        |
        +----------------+
```

***

# Advantages

✅ Faster than complex object creation

✅ Reduces duplicate initialization code

✅ Easy creation of similar objects

✅ Can improve performance

✅ Hides object creation complexity

***

# Disadvantages

❌ Deep cloning can be difficult

❌ Circular references can complicate cloning

❌ Requires careful handling of reference types

❌ Maintenance becomes harder for large object graphs

***

# When to Use

Use Prototype when:

* Object creation is expensive.
* Many similar objects are needed.
* You want a template-based approach.
* Runtime object configuration should be copied and reused.

Examples:

* Game entities
* Document templates
* Report generation
* Dashboard widgets
* Configuration snapshots

***

# Interview Question

### Q: What is the difference between Shallow Copy and Deep Copy?

**Shallow Copy**

Copies primitive values and reference addresses.

```csharp
Employee clone =
    (Employee)this.MemberwiseClone();
```

Nested objects are shared.

***

**Deep Copy**

Creates entirely new nested objects.

```csharp
return new Employee
{
    Name = this.Name,
    Address = this.Address.Clone()
};
```

Nested objects are independent.

***

## Prototype Pattern in One Line

> **Create new objects by copying existing objects instead of constructing them from scratch.**

✅ **Design Pattern #5 Complete: Prototype Pattern**

### Creational Patterns Completed

1. ✅ Singleton
2. ✅ Factory Method
3. ✅ Abstract Factory
4. ✅ Builder
5. ✅ Prototype

The next category is **Structural Patterns**, and the usual starting point is **Adapter Pattern**, often called the "wrapper" pattern because it makes incompatible interfaces work together.
