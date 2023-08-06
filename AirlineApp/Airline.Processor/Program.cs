// See https://aka.ms/new-console-template for more information
using Airline.Processor;

Console.WriteLine("Welcome to the ticketing service");


var messageProcessor = new MessageProcessor();
messageProcessor.Consume();