# NET 6 - Message Broker with RabbitMQ

RabbitMQ is a powerful message broker that can help you create resilient and scalable applications. In this project, we'll study the basics of message brokers, study how RabbitMQ can be used with C# applications.


## Airline App

- packages to install in **Airline.API**
```
RabbitMQ.Client
```


## App

### Rabbit MQ

- run docker command to trigger message brocker
```
docker run -d --hostname rmq --name rabbit-server -p 8080:15672 -p 5672:5672 rabbitmq:3-management
```



<img src="/pictures/devops.png" title="general picture of devops"  width="900">
