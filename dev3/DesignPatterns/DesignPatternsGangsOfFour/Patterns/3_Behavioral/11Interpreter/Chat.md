Excellent. The **Interpreter Pattern** is the final Gang of Four (GoF) design pattern.

Unlike most other patterns, Interpreter is quite specialized and is typically used when:

* Building rule engines
* Evaluating expressions
* Parsing simple languages
* Filtering data
* Query systems
* Mathematical expression evaluators

> **Key idea:**
>
> Represent a grammar as classes and use those classes to interpret sentences or expressions.

***

# Interpreter Pattern

## Definition

The Interpreter Pattern defines a representation for a language's grammar and provides an interpreter that can evaluate sentences in that language.

In simple terms:

> Convert expressions into objects and let those objects evaluate themselves.

***

# Real-World Analogy

Consider a calculator.

Expression:

```text
5 + 3 - 2
```

The calculator interprets:

```text
5
+
3
-
2
```

and produces:

```text
6
```

The Interpreter pattern models each part of the expression as an object.

***

# Problem Statement

Suppose we want to evaluate expressions like:

```text
10 + 5
20 - 3
```

Without Interpreter:

```csharp
if(operator == "+")
{
    return left + right;
}
else if(operator == "-")
{
    return left - right;
}
```

As the grammar grows:

```text
+
-
*
/
AND
OR
NOT
```

the code becomes increasingly complex.

***

# Solution

Represent grammar elements as classes.

Example:

```text
Expression
    |
    +-- NumberExpression
    +-- AddExpression
    +-- SubtractExpression
```

Each expression knows how to interpret itself.

***

# Structure

```text
             IExpression
                   ▲
          ┌────────┼────────┐
          │                 │
 NumberExpression    AddExpression
                     SubtractExpression
```

***

# Console Application Example

Let's build a simple calculator language.

***

## Step 1: Abstract Expression

### IExpression.cs

```csharp
namespace InterpreterPatternDemo
{
    public interface IExpression
    {
        int Interpret();
    }
}
```

***

## Step 2: Terminal Expression

Represents a number.

### NumberExpression.cs

```csharp
namespace InterpreterPatternDemo
{
    public class NumberExpression : IExpression
    {
        private readonly int _number;

        public NumberExpression(int number)
        {
            _number = number;
        }

        public int Interpret()
        {
            return _number;
        }
    }
}
```

***

## Step 3: Non-Terminal Expressions

### AddExpression.cs

```csharp
namespace InterpreterPatternDemo
{
    public class AddExpression : IExpression
    {
        private readonly IExpression _left;
        private readonly IExpression _right;

        public AddExpression(
            IExpression left,
            IExpression right)
        {
            _left = left;
            _right = right;
        }

        public int Interpret()
        {
            return _left.Interpret()
                + _right.Interpret();
        }
    }
}
```

***

### SubtractExpression.cs

```csharp
namespace InterpreterPatternDemo
{
    public class SubtractExpression : IExpression
    {
        private readonly IExpression _left;
        private readonly IExpression _right;

        public SubtractExpression(
            IExpression left,
            IExpression right)
        {
            _left = left;
            _right = right;
        }

        public int Interpret()
        {
            return _left.Interpret()
                - _right.Interpret();
        }
    }
}
```

***

## Step 4: Client

### Program.cs

```csharp
using System;

namespace InterpreterPatternDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            IExpression five =
                new NumberExpression(5);

            IExpression three =
                new NumberExpression(3);

            IExpression two =
                new NumberExpression(2);

            IExpression expression =
                new SubtractExpression(
                    new AddExpression(
                        five,
                        three),
                    two);

            Console.WriteLine(
                expression.Interpret());

            Console.ReadKey();
        }
    }
}
```

***

# Sample Output

```text
6
```

Because:

```text
(5 + 3) - 2

= 8 - 2

= 6
```

***

# Expression Tree Visualization

The expression:

```text
(5 + 3) - 2
```

becomes:

```text
        -
      /   \
     +     2
   /   \
  5     3
```

Evaluation occurs recursively.

***

# Terminal vs Non-Terminal Expressions

A common interview topic.

## Terminal Expression

Cannot be broken down further.

Example:

```csharp
new NumberExpression(5)
```

Represents:

```text
5
```

***

## Non-Terminal Expression

Contains other expressions.

Example:

```csharp
new AddExpression(
    left,
    right);
```

Represents:

```text
5 + 3
```

***

# More Realistic Example: User Access Rules

Suppose we have rules:

```text
Admin AND Active
```

***

## Context

```csharp
public class User
{
    public bool IsAdmin { get; set; }
    public bool IsActive { get; set; }
}
```

***

## Expression Interface

```csharp
public interface IRule
{
    bool Interpret(User user);
}
```

***

## Terminal Expressions

```csharp
public class IsAdminRule : IRule
{
    public bool Interpret(User user)
    {
        return user.IsAdmin;
    }
}
```

```csharp
public class IsActiveRule : IRule
{
    public bool Interpret(User user)
    {
        return user.IsActive;
    }
}
```

***

## Non-Terminal Expression

```csharp
public class AndRule : IRule
{
    private readonly IRule _left;
    private readonly IRule _right;

    public AndRule(IRule left, IRule right)
    {
        _left = left;
        _right = right;
    }

    public bool Interpret(User user)
    {
        return _left.Interpret(user)
            && _right.Interpret(user);
    }
}
```

***

## Usage

```csharp
IRule rule =
    new AndRule(
        new IsAdminRule(),
        new IsActiveRule());

bool result =
    rule.Interpret(user);
```

Expression:

```text
Admin AND Active
```

is represented as objects.

***

# UML (Simplified)

```text
              IExpression
                   ▲
          --------------------
          |                  |
 NumberExpression     AddExpression
                      SubtractExpression
```

***

# Real-World Examples

### Rule Engines

```text
Country == "India"
AND
Age > 18
```

### Search Filters

```text
Category = Electronics
AND
Price < 1000
```

### Mathematical Expressions

```text
(10 + 5) * 2
```

### SQL-like Languages

```text
Name = 'Raghav'
AND Active = true
```

### Compilers

Parsing source code into syntax trees.

***

# Interpreter vs Strategy

## Strategy

Chooses one algorithm.

```text
UPIPayment
CreditCardPayment
PayPalPayment
```

Focus:

✅ Algorithm selection

***

## Interpreter

Represents a language or grammar.

```text
5 + 3 - 2
```

Focus:

✅ Expression evaluation

***

# Interpreter vs Command

## Command

Represents an action.

```csharp
CreateOrderCommand
```

Focus:

✅ Execute a request

***

## Interpreter

Represents a sentence in a language.

```csharp
AddExpression
```

Focus:

✅ Evaluate an expression

***

# Interpreter vs Composite

A very common observation.

## Composite

```text
Folder
 ├── File
 └── File
```

Focus:

✅ Tree structures

***

## Interpreter

Often uses Composite internally.

```text
Expression
 ├── Expression
 └── Expression
```

Focus:

✅ Language evaluation

***

# Advantages

✅ Easy to extend grammar

✅ Represents expressions cleanly

✅ Great for rule engines

✅ Supports recursive evaluation

✅ Grammar maps naturally to object structures

***

# Disadvantages

❌ Many classes for large grammars

❌ Performance can suffer for complex languages

❌ Difficult to maintain for full programming languages

❌ Better parsing tools usually exist for advanced scenarios

***

# When to Use

Use Interpreter when:

* The grammar is simple.
* Expressions are evaluated repeatedly.
* Rules need to be represented as objects.
* Building a DSL (Domain-Specific Language).

Examples:

* Rule engines
* Search filters
* Validation rules
* Simple calculators
* Access control systems
* Workflow conditions

***

# Interview Question

### Q: Why is the Interpreter Pattern rarely used in enterprise applications?

**Answer:**

Because large grammars create many classes and quickly become difficult to maintain.

For complex languages, developers usually prefer:

* ANTLR
* Roslyn
* Expression Trees
* Parsing Libraries

The Interpreter Pattern is most suitable for **simple grammars and rule engines**.

***

# Interpreter Pattern in One Line

> **Represent a grammar as a set of classes and interpret expressions by evaluating those objects.**

✅ **Behavioral Pattern #11 Complete**

***

# 🎉 All 23 GoF Design Patterns Completed

### Creational (5)

✅ Singleton  
✅ Factory Method  
✅ Abstract Factory  
✅ Builder  
✅ Prototype

### Structural (7)

✅ Adapter  
✅ Bridge  
✅ Composite  
✅ Decorator  
✅ Facade  
✅ Flyweight  
✅ Proxy

### Behavioral (11)

✅ Chain of Responsibility  
✅ Observer  
✅ Strategy  
✅ Command  
✅ State  
✅ Template Method  
✅ Mediator  
✅ Iterator  
✅ Memento  
✅ Visitor  
✅ Interpreter

## Final takeaway for juniors

If they remember just one line for each category:

* **Creational:** How objects are created.
* **Structural:** How objects are composed.
* **Behavioral:** How objects communicate and behave.

That mental model alone helps make sense of all 23 patterns.
