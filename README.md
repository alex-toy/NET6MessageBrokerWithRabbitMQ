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

- send messages through the API and receive them 
<img src="/pictures/api.png" title="send messages through API"  width="900">


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

- run the sender and go to http://localhost:8080/#/queues and see the message waiting in the queue
<img src="/pictures/message.png" title="message in queue"  width="900">

- run sender and receivers at the same time and see the messages processed
<img src="/pictures/messages.png" title="message in queue"  width="900">



