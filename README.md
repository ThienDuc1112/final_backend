# final_backend
The overall picture of implementations on my project using microservices.
![image](https://github.com/ThienDuc1112/final_backend/assets/95234772/56c96287-57a4-44ae-9f62-64770538b5a3)
**Microservice communication**
+ Microservices Communication
+ Sync inter-service gRPC Communication
+ Async Microservices Communication with RabbitMQ Message-Broker Service
+ Using RabbitMQ Publish/Subscribe Topic Exchange Model
+ Using MassTransit for abstraction over RabbitMQ Message-Broker system

**Authentication**
+ Use IdentityServer4 to authenticate and authorize
+ Single Sign-On of IdentitySever4 allows to access multiple microservices without the need to re-enter credentials
+ Integrate with external identity providers (Google) 

