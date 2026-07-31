Excellent. After Factory Method, **Abstract Factory** is a natural next step because it solves a bigger problem:

> Factory Method creates **one product**.
>
> Abstract Factory creates **families of related products**.

***

# Abstract Factory Pattern

## Definition

The Abstract Factory Pattern provides an interface for creating families of related or dependent objects without specifying their concrete classes.

***

# Real-World Example

Imagine you're building a cross-platform UI framework.

For **Windows**:

* Windows Button
* Windows Checkbox

For **Mac**:

* Mac Button
* Mac Checkbox

You must ensure:

✅ Windows Button works with Windows Checkbox

✅ Mac Button works with Mac Checkbox

❌ Avoid mixing Windows Button with Mac Checkbox

This is where Abstract Factory shines.

***

# Structure

```text
                 GUI Factory
                       ▲
          ┌────────────┴────────────┐
          │                         │
    WindowsFactory            MacFactory

       ▲      ▲                 ▲      ▲
       │      │                 │      │
WindowsButton WindowsCheckbox MacButton MacCheckbox
```

***

# Console Application Example

We'll build a UI Component Factory.

***

## Step 1: Abstract Products

### IButton.cs

```csharp
namespace AbstractFactoryDemo
{
    public interface IButton
    {
        void Render();
    }
}
```

### ICheckbox.cs

```csharp
namespace AbstractFactoryDemo
{
    public interface ICheckbox
    {
        void Render();
    }
}
```

***

## Step 2: Concrete Products

### WindowsButton.cs

```csharp
using System;

namespace AbstractFactoryDemo
{
    public class WindowsButton : IButton
    {
        public void Render()
        {
            Console.WriteLine("Rendering Windows Button");
        }
    }
}
```

### MacButton.cs

```csharp
using System;

namespace AbstractFactoryDemo
{
    public class MacButton : IButton
    {
        public void Render()
        {
            Console.WriteLine("Rendering Mac Button");
        }
    }
}
```

### WindowsCheckbox.cs

```csharp
using System;

namespace AbstractFactoryDemo
{
    public class WindowsCheckbox : ICheckbox
    {
        public void Render()
        {
            Console.WriteLine("Rendering Windows Checkbox");
        }
    }
}
```

### MacCheckbox.cs

```csharp
using System;

namespace AbstractFactoryDemo
{
    public class MacCheckbox : ICheckbox
    {
        public void Render()
        {
            Console.WriteLine("Rendering Mac Checkbox");
        }
    }
}
```

***

## Step 3: Abstract Factory

### IUIFactory.cs

```csharp
namespace AbstractFactoryDemo
{
    public interface IUIFactory
    {
        IButton CreateButton();
        ICheckbox CreateCheckbox();
    }
}
```

***

## Step 4: Concrete Factories

### WindowsFactory.cs

```csharp
namespace AbstractFactoryDemo
{
    public class WindowsFactory : IUIFactory
    {
        public IButton CreateButton()
        {
            return new WindowsButton();
        }

        public ICheckbox CreateCheckbox()
        {
            return new WindowsCheckbox();
        }
    }
}
```

### MacFactory.cs

```csharp
namespace AbstractFactoryDemo
{
    public class MacFactory : IUIFactory
    {
        public IButton CreateButton()
        {
            return new MacButton();
        }

        public ICheckbox CreateCheckbox()
        {
            return new MacCheckbox();
        }
    }
}
```

***

## Step 5: Client

### Application.cs

```csharp
namespace AbstractFactoryDemo
{
    public class Application
    {
        private readonly IButton _button;
        private readonly ICheckbox _checkbox;

        public Application(IUIFactory factory)
        {
            _button = factory.CreateButton();
            _checkbox = factory.CreateCheckbox();
        }

        public void RenderUI()
        {
            _button.Render();
            _checkbox.Render();
        }
    }
}
```

***

## Step 6: Program.cs

```csharp
using System;

namespace AbstractFactoryDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Select UI Style");
            Console.WriteLine("1 - Windows");
            Console.WriteLine("2 - Mac");

            string choice = Console.ReadLine();

            IUIFactory factory = choice switch
            {
                "1" => new WindowsFactory(),
                "2" => new MacFactory(),
                _ => throw new Exception("Invalid choice")
            };

            Application app = new Application(factory);

            app.RenderUI();

            Console.ReadKey();
        }
    }
}
```

***

# Sample Output

### Windows

```text
Select UI Style
1 - Windows
2 - Mac

1

Rendering Windows Button
Rendering Windows Checkbox
```

### Mac

```text
Select UI Style
1 - Windows
2 - Mac

2

Rendering Mac Button
Rendering Mac Checkbox
```

***

# Why Factory Method Is Not Enough

Suppose we only use Factory Method.

```csharp
ButtonFactory.CreateButton();
CheckboxFactory.CreateCheckbox();
```

Nothing prevents this:

```csharp
WindowsButton
MacCheckbox
```

Now your UI is inconsistent.

Abstract Factory guarantees that related objects come from the same family.

```csharp
WindowsFactory
    -> WindowsButton
    -> WindowsCheckbox
```

or

```csharp
MacFactory
    -> MacButton
    -> MacCheckbox
```

***

# Real-World Example: Database Providers

Imagine supporting multiple databases.

### SQL Server Family

```text
SqlConnection
SqlCommand
SqlTransaction
```

### Oracle Family

```text
OracleConnection
OracleCommand
OracleTransaction
```

Factory Method can create a connection.

Abstract Factory can create the entire database family together.

***

# Advantages

✅ Ensures object compatibility

✅ Keeps related products together

✅ Removes direct dependencies on concrete classes

✅ Supports Open/Closed Principle

✅ Easy to switch entire product families

***

# Disadvantages

❌ More interfaces and classes

❌ Can become complex for small applications

❌ Adding a new product type requires updating every factory

Example:

Adding:

```csharp
ITextBox
```

means modifying:

```csharp
IUIFactory
WindowsFactory
MacFactory
LinuxFactory
```

***

# Factory Method vs Abstract Factory

| Factory Method              | Abstract Factory                            |
| --------------------------- | ------------------------------------------- |
| Creates one product         | Creates families of products                |
| Focuses on object creation  | Focuses on product families                 |
| Usually one creation method | Multiple creation methods                   |
| Simpler                     | More complex                                |
| Example: Create Button      | Example: Create Button + Checkbox + TextBox |

***

# Interview Question

### Q: What is the relationship between Factory Method and Abstract Factory?

**Answer:**

An Abstract Factory is often implemented using multiple Factory Methods.

Example:

```csharp
public interface IUIFactory
{
    IButton CreateButton();
    ICheckbox CreateCheckbox();
}
```

Each method:

```csharp
CreateButton()
CreateCheckbox()
```

is effectively a Factory Method.

So:

> **Abstract Factory = A factory of related factories (or a collection of Factory Methods) used to create an entire product family.**

***

# Key Takeaway for Juniors

Think of it like ordering a meal.

**Factory Method**

```text
Give me a Burger
```

Creates one product.

**Abstract Factory**

```text
Give me the McDonald's Combo
```

Creates a family of related products:

```text
Burger
Fries
Drink
```

that are designed to go together.

✅ **Design Pattern #3 Complete: Abstract Factory Pattern**

**Rule of thumb:**

> Use Factory Method when you need one object.  
> Use Abstract Factory when you need multiple related objects that must work together.
