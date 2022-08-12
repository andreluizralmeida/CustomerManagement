
<h1 align="center"> 
	Customer Management
</h1>

<p align="center">
 <a href="#-about-the-project">About</a> •
 <a href="#-functionalities">Functionalities</a> •
 <a href="#-how-to-execute">How to execute</a> • 
 <a href="#-technologies">Technologies</a> • 
 <a href="#-deployment-documentation">Deployment documentation</a>
</p>


### 💻 About the project

This is an API to manage the customers

---

### ⚙️ Functionalities

- [x] Save a new customer
- [x] Retrieve customer's information by Id and by filter
- [x] Update customer's information

---

### 💡 How to execute

## before starting

Download Docker and install it.
https://desktop.docker.com/win/main/amd64/Docker%20Desktop%20Installer.exe

## Running

Clone the project

```bash
git clone https://github.com/andreluizralmeida/CustomerManagement.git
```

through CMD, enter in the folder of the cloned project (CustomerManagement) and run the docker compose command

```bash
docker compose up
```

Open the browser and type this URL: http://localhost/swagger/index.html

![image](https://user-images.githubusercontent.com/6225842/184368543-a03c0c98-94ea-4366-8977-70af3b109ac0.png)

✅ That's it! Now you are ready to use the customer management project!

---

### 👨‍💻 How to use it

You can use the API through Swagger page or any API Client like Postman or Insomnia

<h4>Swagger</h4>

> Choose which operation you want to do.
<h3>Creating a new Customer</h3>

<details>

![image](https://user-images.githubusercontent.com/6225842/184047149-224fbaec-75c7-41db-b021-da4f0b5a852e.png)

![image](https://user-images.githubusercontent.com/6225842/184047337-4e9c9bdb-9467-4fff-bb91-459c33fd935c.png)

![image](https://user-images.githubusercontent.com/6225842/184047502-5bd46268-5ec2-4ac6-943e-8aa37bd4563e.png)
	
![image](https://user-images.githubusercontent.com/6225842/184093034-67db0317-8af2-4e45-96d9-b541f1503320.png)

</details>

<h3>Searching a customer by Id </h3>

<details>

![image](https://user-images.githubusercontent.com/6225842/184047162-3d2ef385-06b3-474d-add5-c1d4294e3c64.png)

![image](https://user-images.githubusercontent.com/6225842/184047617-77d92fd4-fe3f-4da8-9bdb-96d5a3d32bfb.png)

![image](https://user-images.githubusercontent.com/6225842/184047654-443e0b04-7a9f-4549-85a6-082073af1b2d.png)

</details>

<h3>Searching a customer by filter</h3>

<details>

![image](https://user-images.githubusercontent.com/6225842/184047182-2c26c75b-e931-42e3-b6f2-2cb954c593bc.png)

![image](https://user-images.githubusercontent.com/6225842/184047779-9e920fe3-90fd-4cbf-a552-5460ae44e70b.png)

![image](https://user-images.githubusercontent.com/6225842/184048376-8fb0fbba-0bb1-46d1-a9d0-ce8aec7b4913.png)

* First Name includes
* Surname includes

</details>

<h3>Updating customer information</h3>

<details>

![image](https://user-images.githubusercontent.com/6225842/184047198-8a3128b1-9b68-4b0f-87ac-c981f9833757.png)

![image](https://user-images.githubusercontent.com/6225842/184048497-10a30369-4ac3-4c20-bef3-ecf7144ac1fe.png)

![image](https://user-images.githubusercontent.com/6225842/184048562-078f61a8-fe9a-4530-8cf6-eee727c08b38.png)

</details>

---

### 🛠 Technologies

The following Technologies were used to build this project

- .NET 6
- DDD
- Docker
- Xunit
- Entity Framework Core 6
- [NetArchTest](https://github.com/BenMorris/NetArchTest)
- [AutoMapper](https://github.com/AutoMapper/AutoMapper)
- [FluentResults](https://github.com/altmann/FluentResults)
- [MediatR](https://github.com/jbogard/MediatR)
- [Serilog](https://github.com/serilog/serilog-aspnetcore)
- [BCrypt.net](https://github.com/BcryptNet/bcrypt.net)
- [Flunt](https://github.com/andrebaltieri/Flunt.Extensions.AspNet)
- [IQueryable](https://github.com/brunobritodev/AspNetCore.IQueryable.Extensions)
- [AutoFixture](https://github.com/AutoFixture/AutoFixture)

---

### 🚀 Deployment documentation

> Deploy to a live environment;

Write a .yml file with deployment instructions through Kubernete or Jenkins and etc

> Handle large volumes of requests, including concurrent creation and update operations

Using some load balancer, like Nginx and transaction or implements Semaphore

> Continue operating in the event of problems reading and writing from the database

Implement Resilience Techniques using Hangfire to create a job to retry a fail command. Or Implement some message-broker like Kafka or rabbitMQ to process on demand

> Ensure the security of the user information

Add authentication, add SSL, Encrypt sensitive data and not logging de sensitive data

