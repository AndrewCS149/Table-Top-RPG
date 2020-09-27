# Crypts and Coders
API hosted at: *https://table-top-rpg.azurewebsites.net/api*

## Vision
This app is designed to alleviate the burden of keeping track of DnD campaigns across an array of notebooks and scattered character sheets. Running a table top RPG can be very difficult work, and often requires a lot of note keeping to keep track of all the characters, enemies, locations, etc. This API brings that all to one place, where the Game Master can see and update any information in the game, and players can see and keep track of their own information. Finally, no more pain from forgotten character sheets and coffee-soiled notes, keep all the information needed in one easy and convenient API.

## Scope
### IN
- This API will allow a user to create and keep track of their information throughout a DnD campaign.
- The API will let Game Masters control everything in the database, adding and removing custom items to each table as needed. 
- The API will provide limited seeded data to start and then allow the Game Master user full control.
- The API will be configured to show all relevant information in a way that conforms to RESTful standards.

### OUT
- The API will not provide all of the classes, species, items, etc. available in DnD, nor all of their attributes.

---

### MVP
The MVP for this API is a simple set up using 6 tables to represent Characters, enemies, weapons, locations, items, and stats which will all be joined together and displayed following RESTful standards. It will also utilize basic authorization to allow players to only view their own character's informaiton but allow the GM full access to all routes.

---

### Stretch
- Event log table that will store events that are posted to the API with all relevant information.
- Sophisticated interactions to allow players to "Loot" slain enemies or "Trade" with other players.
- Pull in items, classes and other information from DnD API to have a more robust initial data seed.

---

### Functional Requirements
- A Game Master has full access to read, update, add and delete any and all objects in all tables.
- A Player can view all of their own information, including Weapon, Stats, Inventory and current locaiton, and can update some parts.
- An anonymous user can view assets not related to the ongoing game such as enemies but not their locations, items but not who has them, etc.

---

### Data Flow
A user will make any kind of request to the API, it will be processed if the information given is correct and they the user has appropriate authorization in the for of a JWT token, the data will be queried and either update or return the information supplied the user.
The various routes and their methods are listed below.

---

## Non-Functional Requirements 
- Security: Our app will utilize ASP.NET Identity framework in order to add a layer of both security and authorizaiton to the data. The information will be primarily overseen by one user, the Game Master, and they will be the only user with all authorization. Players will only be able to view their own information after entering in their username and passwords to receive authorization. All of the authorization is being handled via JWT tokens.
- Testability: All of the services on our app will have tests for every method to ensure that they can handle the data correctly. The services are used to handle what happens for each of the routes and the tests will ensure that any queries are being handled correctly as long as the inputs are correct. The testing is done using the XUnit testing framework.

---