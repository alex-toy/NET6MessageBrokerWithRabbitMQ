# NET 6 - Message Broker with RabbitMQ

RabbitMQ is a powerful message broker that can help you create resilient and scalable applications. In this project, we'll study the basics of message brokers, study how RabbitMQ can be used with C# applications.


## Airline App

- packages to install in **Airline.API**
```
RabbitMQ.Client
```

- packages to install in **Airline.Processor**
```
RabbitMQ.Client
```

- run docker compose and reach 
```
http://localhost:15672/#/
```


## Rabbit App

### Rabbit MQ

- packages to install in **RabbitSender**
```
RabbitMQ.Client
```

- run docker command to trigger message brocker
```
docker run -d --hostname rmq --name rabbit-server -p 8080:15672 -p 5672:5672 rabbitmq:3-management
```

- go to http://localhost:8080 and see the message waiting in the queue
<img src="/pictures/message.png" title="message in queue"  width="900">



