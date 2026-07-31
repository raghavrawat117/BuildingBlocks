Excellent. The **State Pattern** is one of the most frequently confused patterns because it looks very similar to Strategy.

The easiest way to remember the difference is:

> **Strategy:** The client chooses the behavior.
>
> **State:** The object changes its own behavior when its internal state changes.

***

# State Pattern

## Definition

The State Pattern allows an object to alter its behavior when its internal state changes.

The object appears to change its class because different states provide different behavior.

***

# Real-World Analogy

Think about a vending machine.

Possible states:

```text
No Coin
Coin Inserted
Dispensing Item
Out Of Stock
```

The same action:

```csharp
SelectItem()
```

behaves differently depending on the machine's state.

***

# Problem Statement

Without the State Pattern:

```csharp
if(state == "Draft")
{
    Console.WriteLine("Editing document");
}
else if(state == "Published")
{
    Console.WriteLine("Cannot edit");
}
else if(state == "Archived")
{
    Console.WriteLine("Read only");
}
```

As states grow:

```text
Draft
Review
Approved
Published
Rejected
Archived
```

the code becomes full of conditional logic.

***

# Solution

Move state-specific behavior into separate classes.

```text
Document
    |
    +-- DraftState
    +-- PublishedState
    +-- ArchivedState
```

The document delegates behavior to its current state.

***

# Structure

```text
             IDocumentState
                    ▲
        ┌───────────┼───────────┐
        │           │           │
     Draft      Published    Archived

                    ▲
                    |
                Document
```

***

# Console Application Example

We'll create a document workflow.

***

## Step 1: State Interface

### IDocumentState.cs

```csharp
namespace StatePatternDemo
{
    public interface IDocumentState
    {
        void Publish(Document document);
    }
}
```

***

## Step 2: Context

### Document.cs

```csharp
using System;

namespace StatePatternDemo
{
    public class Document
    {
        private IDocumentState _state;

        public Document()
        {
            _state = new DraftState();
        }

        public void SetState(IDocumentState state)
        {
            _state = state;
        }

        public void Publish()
        {
            _state.Publish(this);
        }
    }
}
```

***

## Step 3: Concrete States

### DraftState.cs

```csharp
using System;

namespace StatePatternDemo
{
    public class DraftState : IDocumentState
    {
        public void Publish(Document document)
        {
            Console.WriteLine(
                "Document published.");

            document.SetState(
                new PublishedState());
        }
    }
}
```

***

### PublishedState.cs

```csharp
using System;

namespace StatePatternDemo
{
    public class PublishedState : IDocumentState
    {
        public void Publish(Document document)
        {
            Console.WriteLine(
                "Document already published.");
        }
    }
}
```

***

## Step 4: Client

### Program.cs

```csharp
using System;

namespace StatePatternDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Document document =
                new Document();

            document.Publish();

            document.Publish();

            Console.ReadKey();
        }
    }
}
```

***

# Sample Output

```text
Document published.

Document already published.
```

Notice:

The same method:

```csharp
document.Publish();
```

behaves differently depending on the current state.

***

# More Realistic Example: Order Processing

An online order may have states:

```text
New
Paid
Shipped
Delivered
```

***

## State Interface

```csharp
public interface IOrderState
{
    void Next(Order order);
}
```

***

## New State

```csharp
public class NewState : IOrderState
{
    public void Next(Order order)
    {
        Console.WriteLine("Payment received.");
        order.SetState(new PaidState());
    }
}
```

***

## Paid State

```csharp
public class PaidState : IOrderState
{
    public void Next(Order order)
    {
        Console.WriteLine("Order shipped.");
        order.SetState(new ShippedState());
    }
}
```

***

## Shipped State

```csharp
public class ShippedState : IOrderState
{
    public void Next(Order order)
    {
        Console.WriteLine("Order delivered.");
        order.SetState(new DeliveredState());
    }
}
```

***

## Delivered State

```csharp
public class DeliveredState : IOrderState
{
    public void Next(Order order)
    {
        Console.WriteLine("Order already completed.");
    }
}
```

***

## Client

```csharp
Order order = new Order();

order.Next();
order.Next();
order.Next();
order.Next();
```

Output:

```text
Payment received.
Order shipped.
Order delivered.
Order already completed.
```

***

# Without State Pattern

Typical code:

```csharp
switch(orderStatus)
{
    case OrderStatus.New:
        ...
        break;

    case OrderStatus.Paid:
        ...
        break;

    case OrderStatus.Shipped:
        ...
        break;
}
```

As states grow, this switch becomes huge.

***

# With State Pattern

Each state owns its behavior.

```csharp
NewState
PaidState
ShippedState
DeliveredState
```

No giant switch statement.

***

# UML (Simplified)

```text
            IOrderState
                 ▲
      -------------------------
      |          |           |
    New       Paid      Shipped

                 ▲
                 |
               Order
```

***

# Real-World .NET Examples

## Workflow Engines

Status transitions:

```text
Draft
Review
Approved
Rejected
```

Each state allows different actions.

***

## Order Management Systems

```text
Pending
Paid
Shipped
Delivered
```

***

## Document Management

```text
Draft
Published
Archived
```

***

## Media Players

```text
Playing
Paused
Stopped
```

The same button behaves differently depending on the state.

***

# State vs Strategy

This is the most common interview question.

## Strategy

The client chooses the algorithm.

```csharp
var payment =
    new UpiPayment();
```

```csharp
cart.Checkout(payment);
```

Focus:

✅ Different ways to perform the same task.

***

## State

The object changes behavior itself.

```csharp
order.Next();
```

Internally:

```text
New -> Paid -> Shipped
```

Focus:

✅ Behavior depends on current state.

***

### Quick Rule

**Strategy**

```text
Client chooses behavior.
```

**State**

```text
Object changes behavior.
```

***

# State vs Chain of Responsibility

### Chain of Responsibility

```text
Handler1
   ↓
Handler2
   ↓
Handler3
```

The request travels through handlers.

***

### State

```text
Draft
  ↓
Published
  ↓
Archived
```

The object transitions between states.

***

# Advantages

✅ Eliminates large switch/if blocks

✅ Encapsulates state-specific behavior

✅ Supports Open/Closed Principle

✅ Easier to add new states

✅ Cleaner workflow implementations

***

# Disadvantages

❌ More classes

❌ Complex workflows can create many state objects

❌ Understanding state transitions may require more effort

***

# When to Use

Use State when:

* Behavior changes based on state.
* Large switch statements exist.
* State transitions are important.
* Workflow-driven systems are being built.

Examples:

* Order processing
* Document management
* Ticket systems
* Workflow engines
* Media players
* ATM machines
* Vending machines

***

# Interview Question

### Q: What problem does the State Pattern solve?

**Answer:**

The State Pattern removes large conditional logic by encapsulating state-specific behavior into separate classes and allowing the object to change behavior when its state changes.

Instead of:

```csharp
if(status == Draft)
{
}
else if(status == Published)
{
}
```

Use:

```csharp
document.Publish();
```

and let the current state decide what happens.

***

# State Pattern in One Line

> **Allow an object to change its behavior automatically when its internal state changes.**

✅ **Behavioral Pattern #5 Complete: State Pattern**

### Behavioral Patterns Progress

1. ✅ Chain of Responsibility
2. ✅ Observer
3. ✅ Strategy
4. ✅ Command
5. ✅ State
6. ⏭️ Iterator
7. ⏭️ Mediator
8. ⏭️ Memento
9. ⏭️ Template Method
10. ⏭️ Visitor
11. ⏭️ Interpreter

A natural next pattern is **Template Method**, because it contrasts nicely with Strategy:

* **Strategy** = composition + interchangeable algorithms
* **Template Method** = inheritance + fixed algorithm structure with customizable steps.
