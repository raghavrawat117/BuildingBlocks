Excellent. The **Command Pattern** is one of the most practical Behavioral Patterns and is heavily used in:

* CQRS (Command Query Responsibility Segregation)
* Undo/Redo functionality
* Menu actions
* Background jobs
* Task queues
* Button click handlers
* ASP.NET applications

> **Key idea:**
>
> Encapsulate a request as an object.

This allows requests to be passed around, queued, logged, stored, retried, and executed later.

***

# Command Pattern

## Definition

The Command Pattern converts a request into a standalone object that contains all information needed to perform an action.

Instead of calling methods directly:

```csharp
light.TurnOn();
```

you create a command object:

```csharp
new TurnOnCommand(light);
```

and execute it:

```csharp
command.Execute();
```

***

# Real-World Analogy

Think of a restaurant.

```text
Customer
    ↓
Waiter
    ↓
Chef
```

The customer doesn't directly tell the chef.

Instead:

```text
Order Slip
```

is created and passed around.

That order slip is the **Command**.

***

# Problem Statement

Without Command:

```csharp
button.Click()
{
    light.TurnOn();
}
```

The button is tightly coupled to the light.

What if tomorrow the button should:

```text
Open Garage
Start Fan
Send Email
Generate Report
```

You'd need to change the button each time.

***

# Solution Structure

```text
Client
   |
   v
Command
   |
   v
Receiver
```

The client creates commands, and the invoker executes them.

***

# Components of Command Pattern

### Command

Defines execution contract.

```csharp
ICommand
```

### Concrete Command

Implements the request.

```csharp
TurnOnCommand
```

### Receiver

Performs actual work.

```csharp
Light
```

### Invoker

Triggers command execution.

```csharp
RemoteControl
```

### Client

Creates and wires everything together.

***

# Console Application Example

## Step 1: Command Interface

### ICommand.cs

```csharp
namespace CommandPatternDemo
{
    public interface ICommand
    {
        void Execute();
    }
}
```

***

## Step 2: Receiver

The actual business logic lives here.

### Light.cs

```csharp
using System;

namespace CommandPatternDemo
{
    public class Light
    {
        public void TurnOn()
        {
            Console.WriteLine("Light turned ON");
        }

        public void TurnOff()
        {
            Console.WriteLine("Light turned OFF");
        }
    }
}
```

***

## Step 3: Concrete Commands

### TurnOnCommand.cs

```csharp
namespace CommandPatternDemo
{
    public class TurnOnCommand : ICommand
    {
        private readonly Light _light;

        public TurnOnCommand(Light light)
        {
            _light = light;
        }

        public void Execute()
        {
            _light.TurnOn();
        }
    }
}
```

***

### TurnOffCommand.cs

```csharp
namespace CommandPatternDemo
{
    public class TurnOffCommand : ICommand
    {
        private readonly Light _light;

        public TurnOffCommand(Light light)
        {
            _light = light;
        }

        public void Execute()
        {
            _light.TurnOff();
        }
    }
}
```

***

## Step 4: Invoker

### RemoteControl.cs

```csharp
namespace CommandPatternDemo
{
    public class RemoteControl
    {
        private ICommand _command;

        public void SetCommand(ICommand command)
        {
            _command = command;
        }

        public void PressButton()
        {
            _command.Execute();
        }
    }
}
```

***

## Step 5: Client

### Program.cs

```csharp
using System;

namespace CommandPatternDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Light light = new Light();

            ICommand turnOn =
                new TurnOnCommand(light);

            ICommand turnOff =
                new TurnOffCommand(light);

            RemoteControl remote =
                new RemoteControl();

            remote.SetCommand(turnOn);
            remote.PressButton();

            remote.SetCommand(turnOff);
            remote.PressButton();

            Console.ReadKey();
        }
    }
}
```

***

# Sample Output

```text
Light turned ON

Light turned OFF
```

***

# Request Flow

When:

```csharp
remote.PressButton();
```

is executed:

```text
RemoteControl
      ↓
 TurnOnCommand
      ↓
     Light
```

The invoker knows nothing about the actual receiver.

***

# Why Not Direct Method Calls?

Without Command:

```csharp
light.TurnOn();
light.TurnOff();
```

This works for simple cases but you cannot:

* Queue actions
* Log actions
* Store actions
* Undo actions
* Retry actions

***

With Command:

```csharp
ICommand command =
    new TurnOnCommand(light);
```

The action becomes an object.

Objects can be:

```text
Stored
Queued
Serialized
Retried
Logged
```

***

# Real-World Example: Bank Transactions

## Receiver

```csharp
public class BankAccount
{
    public void Deposit(decimal amount)
    {
        Console.WriteLine(
            $"Deposited ₹{amount}");
    }
}
```

***

## Command

```csharp
public class DepositCommand : ICommand
{
    private readonly BankAccount _account;
    private readonly decimal _amount;

    public DepositCommand(
        BankAccount account,
        decimal amount)
    {
        _account = account;
        _amount = amount;
    }

    public void Execute()
    {
        _account.Deposit(_amount);
    }
}
```

***

## Client

```csharp
var command =
    new DepositCommand(account, 5000);

command.Execute();
```

The transaction is now encapsulated as an object.

***

# Undo/Redo Example

This is one of the most famous uses of Command.

### Enhanced Interface

```csharp
public interface ICommand
{
    void Execute();
    void Undo();
}
```

***

### Command

```csharp
public class DepositCommand : ICommand
{
    public void Execute()
    {
        account.Deposit(amount);
    }

    public void Undo()
    {
        account.Withdraw(amount);
    }
}
```

***

### Usage

```csharp
command.Execute();

command.Undo();
```

Output:

```text
Deposited ₹5000
Withdrew ₹5000
```

***

# CQRS Example

A modern .NET example.

### Command

```csharp
public class CreateOrderCommand
{
    public string CustomerName { get; set; }
}
```

***

### Handler

```csharp
public class CreateOrderCommandHandler
{
    public void Handle(
        CreateOrderCommand command)
    {
        Console.WriteLine(
            $"Creating order for {command.CustomerName}");
    }
}
```

This is the basis of frameworks like MediatR.

***

# Real-World .NET Examples

## Menu Actions

```csharp
SaveCommand
PrintCommand
OpenCommand
```

Each menu item triggers a command.

***

## Background Jobs

```csharp
SendEmailCommand
GenerateReportCommand
ProcessInvoiceCommand
```

Jobs can be queued and executed later.

***

## ASP.NET CQRS

```csharp
CreateEmployeeCommand
UpdateEmployeeCommand
DeleteEmployeeCommand
```

Commands represent user intentions.

***

# UML (Simplified)

```text
             ICommand
                 ▲
        ------------------
        |                |
 TurnOnCommand   TurnOffCommand

                 |
                 v
               Light

                 ^
                 |
           RemoteControl
```

***

# Command vs Strategy

This is a very common interview question.

## Strategy

Represents different algorithms.

```text
UPI Payment
Card Payment
PayPal Payment
```

Focus:

✅ How something is done

***

## Command

Represents an action/request.

```text
Create Order
Delete Order
Send Email
```

Focus:

✅ What should be done

***

# Command vs Chain of Responsibility

### Chain of Responsibility

Routes a request.

```text
Handler1
   ↓
Handler2
   ↓
Handler3
```

***

### Command

Encapsulates a request.

```text
Command
   ↓
Receiver
```

***

# Advantages

✅ Decouples sender from receiver

✅ Supports undo/redo

✅ Supports queuing

✅ Supports logging

✅ Supports retries

✅ Follows Open/Closed Principle

✅ Works well with CQRS

***

# Disadvantages

❌ More classes

❌ Can increase complexity

❌ Overkill for very simple actions

***

# When to Use

Use Command when:

* Actions need to be represented as objects.
* Undo/Redo is required.
* Requests should be queued.
* Requests should be logged.
* Multiple receivers may execute commands.

Examples:

* CQRS
* Task Queues
* Bank Transactions
* UI Buttons
* Workflow Engines
* Background Processing

***

# Interview Question

### Q: What problem does the Command Pattern solve?

**Answer:**

The Command Pattern decouples the object that requests an action from the object that performs it by encapsulating the request into a command object.

Instead of:

```csharp
light.TurnOn();
```

you use:

```csharp
ICommand command =
    new TurnOnCommand(light);

command.Execute();
```

This allows commands to be stored, logged, queued, retried, and undone.

***

# Command Pattern in One Line

> **Encapsulate a request as an object, allowing it to be parameterized, queued, logged, and executed independently.**

✅ **Behavioral Pattern #4 Complete: Command Pattern**

### Behavioral Patterns Progress

1. ✅ Chain of Responsibility
2. ✅ Observer
3. ✅ Strategy
4. ✅ Command
5. ⏭️ Iterator
6. ⏭️ Mediator
7. ⏭️ Memento
8. ⏭️ State
9. ⏭️ Template Method
10. ⏭️ Visitor
11. ⏭️ Interpreter

A practical next pattern is **State Pattern**, because it is frequently confused with Strategy and is commonly asked in .NET interviews.
