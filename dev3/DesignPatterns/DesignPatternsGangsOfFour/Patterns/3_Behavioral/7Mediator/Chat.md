Excellent. The **Mediator Pattern** is designed to solve a common problem:

> When many objects communicate with each other directly, the system becomes difficult to maintain.

The Mediator Pattern centralizes communication through a single object called the **Mediator**.

***

# Mediator Pattern

## Definition

The Mediator Pattern defines an object that encapsulates how a set of objects interact.

Instead of objects talking directly to each other, they communicate through the mediator.

***

# Real-World Analogy

Think of an airport control tower.

Without a control tower:

```text
Plane A ↔ Plane B
Plane A ↔ Plane C
Plane B ↔ Plane C
...
```

Every plane must communicate with every other plane.

This becomes chaotic.

With a control tower:

```text
Plane A
    \
Plane B ---> Control Tower <--- Plane C
    /
Plane D
```

Planes communicate only with the tower.

The tower coordinates everything.

***

# Problem Statement

Suppose you have a chat application.

Without Mediator:

```text
User1 ↔ User2
User1 ↔ User3
User2 ↔ User3
```

As users increase:

```text
n * (n-1) relationships
```

The system becomes tightly coupled.

***

# Solution

Introduce a mediator.

```text
User1
   \
User2 ---> ChatRoom <--- User3
   /
User4
```

Users communicate only with the chat room.

***

# Structure

```text
             IMediator
                 ▲
                 |
             ChatRoom

                 ▲
        -------------------
        |        |        |
      User1    User2    User3
```

***

# Console Application Example

## Step 1: Mediator Interface

### IChatMediator.cs

```csharp
namespace MediatorPatternDemo
{
    public interface IChatMediator
    {
        void SendMessage(
            string message,
            ChatUser user);

        void RegisterUser(ChatUser user);
    }
}
```

***

## Step 2: Concrete Mediator

### ChatRoom.cs

```csharp
using System.Collections.Generic;

namespace MediatorPatternDemo
{
    public class ChatRoom : IChatMediator
    {
        private readonly List<ChatUser> _users =
            new();

        public void RegisterUser(ChatUser user)
        {
            _users.Add(user);
        }

        public void SendMessage(
            string message,
            ChatUser sender)
        {
            foreach (var user in _users)
            {
                if (user != sender)
                {
                    user.Receive(message);
                }
            }
        }
    }
}
```

***

## Step 3: Colleague

### ChatUser.cs

```csharp
using System;

namespace MediatorPatternDemo
{
    public class ChatUser
    {
        private readonly IChatMediator _mediator;

        public string Name { get; }

        public ChatUser(
            string name,
            IChatMediator mediator)
        {
            Name = name;
            _mediator = mediator;
        }

        public void Send(string message)
        {
            Console.WriteLine(
                $"{Name} sends: {message}");

            _mediator.SendMessage(
                message,
                this);
        }

        public void Receive(string message)
        {
            Console.WriteLine(
                $"{Name} received: {message}");
        }
    }
}
```

***

## Step 4: Client

### Program.cs

```csharp
using System;

namespace MediatorPatternDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            ChatRoom chatRoom = new ChatRoom();

            ChatUser raghav =
                new ChatUser("Raghav", chatRoom);

            ChatUser amit =
                new ChatUser("Amit", chatRoom);

            ChatUser priya =
                new ChatUser("Priya", chatRoom);

            chatRoom.RegisterUser(raghav);
            chatRoom.RegisterUser(amit);
            chatRoom.RegisterUser(priya);

            raghav.Send("Hello Everyone!");

            Console.ReadKey();
        }
    }
}
```

***

# Sample Output

```text
Raghav sends: Hello Everyone!

Amit received: Hello Everyone!

Priya received: Hello Everyone!
```

***

# Request Flow

When:

```csharp
raghav.Send("Hello Everyone!");
```

Flow becomes:

```text
Raghav
   ↓
ChatRoom
   ↓
Amit
   ↓
Priya
```

Users never communicate directly.

***

# More Realistic Example: Air Traffic Control

## Mediator

```csharp
public interface IAirTrafficControl
{
    void Broadcast(
        string message,
        Aircraft sender);
}
```

***

## Aircraft

```csharp
public class Aircraft
{
    private readonly IAirTrafficControl _tower;

    public string FlightNumber { get; }

    public Aircraft(
        string flightNumber,
        IAirTrafficControl tower)
    {
        FlightNumber = flightNumber;
        _tower = tower;
    }
}
```

***

## Communication

```csharp
aircraft.Send(
    "Requesting landing clearance");
```

Instead of contacting every aircraft directly, the request goes through the control tower.

***

# UI Control Example

Imagine a form with:

```text
Country Dropdown
State Dropdown
City Dropdown
```

Without Mediator:

```text
Country knows State
Country knows City
State knows Country
State knows City
City knows Country
```

Many dependencies.

***

With Mediator:

```text
Country
   \
State ---> FormMediator
   /
City
```

When country changes:

```csharp
_formMediator.CountryChanged();
```

The mediator updates the other controls.

***

# Real-World .NET Example: MediatR

One of the most popular .NET libraries is **MediatR**.

### Request

```csharp
public record CreateOrderCommand(
    string CustomerName);
```

***

### Handler

```csharp
public class CreateOrderHandler :
    IRequestHandler<CreateOrderCommand>
{
    public Task Handle(
        CreateOrderCommand request,
        CancellationToken token)
    {
        Console.WriteLine(
            $"Creating order for {request.CustomerName}");

        return Task.CompletedTask;
    }
}
```

***

### Client

```csharp
await mediator.Send(
    new CreateOrderCommand("Raghav"));
```

Instead of calling handlers directly:

```csharp
handler.Handle(command);
```

the request flows through the mediator.

This is one of the most common uses of the Mediator pattern in modern .NET applications.

***

# UML (Simplified)

```text
            IMediator
                ▲
                |
            ChatRoom

                ▲
      --------------------
      |        |         |
    User1    User2    User3
```

***

# Mediator vs Observer

A very common interview question.

## Observer

```text
Subject
  ├── Observer1
  ├── Observer2
  └── Observer3
```

One-to-many notification.

Example:

```text
Stock Price Changed
```

All subscribers get notified.

***

## Mediator

```text
User1
User2
User3
   ↓
Mediator
```

Coordinates communication between many objects.

Focus:

✅ Centralized communication

***

# Mediator vs Facade

## Facade

Simplifies access to a subsystem.

```text
Client
   ↓
Facade
   ↓
Subsystem
```

Focus:

✅ Simplicity

***

## Mediator

Controls communication between objects.

```text
Object A
Object B
Object C
    ↓
Mediator
```

Focus:

✅ Collaboration

***

# Mediator vs Chain of Responsibility

## Chain of Responsibility

```text
Handler1
   ↓
Handler2
   ↓
Handler3
```

A request travels through a chain.

***

## Mediator

```text
ObjectA
ObjectB
ObjectC
    ↓
Mediator
```

A mediator coordinates interactions.

***

# Advantages

✅ Reduces coupling between objects

✅ Centralizes communication logic

✅ Easier maintenance

✅ Follows Single Responsibility Principle

✅ Easier to add new colleagues

***

# Disadvantages

❌ Mediator can become very large

❌ Too much logic may accumulate in one class

❌ Can become a "god object" if not managed carefully

***

# When to Use

Use Mediator when:

* Many objects interact with each other.
* Object-to-object dependencies are becoming complex.
* Communication rules should be centralized.

Examples:

* Chat applications
* UI forms
* Workflow engines
* Air traffic systems
* Message dispatchers
* CQRS with MediatR

***

# Interview Question

### Q: What problem does the Mediator Pattern solve?

**Answer:**

The Mediator Pattern reduces coupling between objects by centralizing communication in a mediator object.

Instead of:

```text
User1 ↔ User2 ↔ User3
```

you use:

```text
User1
User2 ---> Mediator
User3
```

which makes the system easier to maintain and extend.

***

# Mediator Pattern in One Line

> **Encapsulate how a group of objects interact by centralizing communication through a mediator.**

✅ **Behavioral Pattern #7 Complete: Mediator Pattern**

### Behavioral Patterns Progress

1. ✅ Chain of Responsibility
2. ✅ Observer
3. ✅ Strategy
4. ✅ Command
5. ✅ State
6. ✅ Template Method
7. ✅ Mediator
8. ⏭️ Iterator
9. ⏭️ Memento
10. ⏭️ Visitor
11. ⏭️ Interpreter

**Next Pattern: Iterator Pattern**, which provides a standard way to traverse a collection without exposing its internal structure.
