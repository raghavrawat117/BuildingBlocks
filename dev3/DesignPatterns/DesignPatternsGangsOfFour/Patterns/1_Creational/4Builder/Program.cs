using System;
using Abstractions.IEmployeeRepo;

namespace BuilderPatternDemo;

public class Computer
{
    public string CPU { get; set; }
    public string RAM { get; set; }
    public string Storage { get; set; }
    public string GraphicsCard { get; set; }

    public bool WifiEnabled { get; set; }
    public bool BluetoothEnabled { get; set; }

    public void ShowConfiguration()
    {
        Console.WriteLine("Computer Configuration");
        Console.WriteLine($"CPU: {CPU}");
        Console.WriteLine($"RAM: {RAM}");
        Console.WriteLine($"Storage: {Storage}");
        Console.WriteLine($"Graphics Card: {GraphicsCard}");
        Console.WriteLine($"WiFi: {WifiEnabled}");
        Console.WriteLine($"Bluetooth: {BluetoothEnabled}");
    }
}

public class ComputerBuilder
{
    private readonly Computer _computer;

    public ComputerBuilder()
    {
        _computer = new Computer();
    }

    public ComputerBuilder SetCpu(string cpu)
    {
        _computer.CPU = cpu;
        return this;
    }

    public ComputerBuilder SetRam(string ram)
    {
        _computer.RAM = ram;
        return this;
    }

    public ComputerBuilder SetStorage(string storage)
    {
        _computer.Storage = storage;
        return this;
    }

    public ComputerBuilder SetGraphicsCard(string card)
    {
        _computer.GraphicsCard = card;
        return this;
    }

    public ComputerBuilder EnableWifi()
    {
        _computer.WifiEnabled = true;
        return this;
    }

    public ComputerBuilder EnableBluetooth()
    {
        _computer.BluetoothEnabled = true;
        return this;
    }

    public Computer Build()
    {
        return _computer;
    }
}

public class Program : IProgramToRun
{
    public void Run()
    {
        Computer gamingPc = new ComputerBuilder()
                            .SetCpu("Intel i9")
                            .SetRam("32GB")
                            .SetStorage("1TB SSD")
                            .SetGraphicsCard("RTX 4080")
                            .EnableWifi()
                            .EnableBluetooth()
                            .Build();

        gamingPc.ShowConfiguration();

        Console.ReadKey();
    }
}