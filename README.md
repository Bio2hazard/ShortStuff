ShortStuff
==========

A Twitter-clone based on google accounts, created as a learning exercise project. Uses a MEAN stack for frontend and ASP.NET Web Api 2 + Entity Framework for a api backend.

**This is a work in progress - The Api is nearing completion, the front-end is woefully underdeveloped.**


----------


Api Structure
-------------
###Projects:

 - **ShortStuff.Data (Data Layer):** Contains the Data Entities, Fluent Api Mappings and the Entity Framework context.
 - **ShortStuff.Repository (Repository Layer):** Interacts with Entity Framework's context, fetches Data Entities and converts them to Business Models.
 - **ShortStuff.Domain (Domain/Service Layer):** Represents the one and only access point to fetch data from the Database via service classes.
 - **ShortStuff.Web (Web Layer):** A ASP.NET Web Api 2 that provides RESTful data access via http/https. Also handles DI/IOC via Ninject.

###References:

**ShortStuff.Data** <= **ShortStuff.Repository** => **ShortStuff.Domain**

**ShortStuff.Web** References all other projects to provide constructor injection. 

**ShortStuff.Data** and **ShortStuff.Domain** have no references.

**ShortStuff.Repository** references **both ShortStuff.Data and ShortStuff.Domain**, and acts as a intermediary.

Methodology
-----------

####Domain Layer
The **Domain Layer** provides the IRepository Interface that outlines all methods required by the services - it's up to the **Repository Layer** to fulfill these requirements. This decouples the **Domain Layer** from the underlying data architecture - all that matters is that the **Repository Layer** implements the methods requested through the Interface. 

Also provided by the **Domain Layer** are the *Business Models* - they contain smiliar properties to the *Data Entities*, but also provide some business logic, such as validation.

Lastly, the **Domain Layer** provides Services. Services provide the internal access point to the Data, as well as business logic. Every service method returns a universal *ActionResult* model that contains information pertaining the action as well as data. It's up to the service consumers to mold the *ActionResult* in a way that makes sense for the platform.

**Example:** The **Web Layer** acts as a consumer of the services provided by the **Domain Layer**. It requests *ActionResults* and turns them into Http Response Codes, encoding any data it needs to transmit via JSON.

Closing Thoughts
----------------
I am aware that this level of decoupling is unnecessary for a project of this size. Nevertheless, I went ahead with it to *learn* - this whole project represents the journey of a single developer trying to learn and apply enterprise-level design patterns and philosophy.

There are many patterns and anti-patterns around, and each has a lot of proponents as well as detractors:

I know that Entity Framework supports *Rich Domain Models*, and that my POCOs are anemic and considered pointless, or even a bad practice by some. I also know that some people dislike the idea of a Repository Layer on top of Entity Framework, as it's context can be considered a repository, thus leading to a pointless abstraction.

In the end, there are many different views and coding styles, and I made my choice regarding what techniques & patterns I want to employ when I started the project. Perhaps I will work on another learning project in the future, employing Rich Domain Models, or accessing Entity Framework directly.


> Written with [StackEdit](https://stackedit.io/).