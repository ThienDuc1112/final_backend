# final_backend
The overall picture of implementations on my project using microservices.
![image](https://github.com/ThienDuc1112/final_backend/assets/95234772/56c96287-57a4-44ae-9f62-64770538b5a3)

**Microservice communication**
+ Sync inter-service gRPC Communication
+ Async Microservices Communication with RabbitMQ Message-Broker Service
+ Using RabbitMQ Publish/Subscribe Direct Exchange Model
+ Using MassTransit for abstraction over the RabbitMQ Message-Broker system

**Authentication**
+ Use IdentityServer4 to authenticate and authorize
+ Single Sign-On of IdentitySever4 allows to access multiple microservices without the need to re-enter credentials
+ Integrate with external identity providers (Google)

**API Gateway Microservice**
+ Route different port numbers of microservices into a single one
+ Use Rate-Limiting to prevent DDOS attacks
+ Use Caching to enhance the performance of the system

  

