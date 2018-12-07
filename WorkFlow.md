# _Word Counter Project_
#### C# ASP.NET Core MVC Independent Project at Epicodus, 12.5.2018

### Created By
* _Tanvi Garg and Sheila Stephen_

### Description
* _Add Project Description._


### MVC Specifications
* _**DATABASE DESIGN:**_

![Image description](/ToDoList/wwwroot/images/Database.png)
1. _Database Name: todolist_
2. _Test Database Name: todolist_test_
3. _Database Schema is One To Many_
4. _Two Tables - items and categories_
5. _items has 3 columns id, description and cateory id_
6. _categories has 2 columns id and name_
7. _An item can belong to a category. To do this, we will need to save the item's category ID into our item table. Category ID will be the foreign key that binds a category to several items.

* _**PROJECT STRUCTURE:**_


* _**PROJECT FLOW:**_
Parent Class: Category
Properties:
1. id
2. name
3. List of items

Child Class: Item
Properties:
1. description
2. id
3. category id (foreign key in DB)

Methods For Child:
1. _**Get Description**_: gets value stored in the description property.
2. _**Set Description**_: assigns a new value into a ItemDescription property.
3. _**Save Method**_: Uses SQL command via C# MySQL inbuilt classes and methods to take user input description and saves to the database.
4. Every Save creates a new entry into the todolist database---> item table and auto increments an id for that item.
5. _**Equal Method**_: Modify the Object.Equals inbuilt C# method to check whether the object is of the datatype Item. If not, perform explicit type conversion and compare all the property values. Return true or false.
6. _**Edit**_:Takes in a new-description argument and uses C# MySQL inbuilt classes and methods to save it to an existing entry into the database. Also, update in the existing item object (constructor) description property.
7. _**GetAll**_: Uses SQL command via C# MySQL inbuilt classes and methods to query all items from the database and extracts all property values. These values are used to create item objects using the Item constructor. Returns all item objects in a list.
8. _**Find**_: Generate Item Id using GetID method on the item. Use this as an argument for Find method. Using C# MySQL commands pull the description and category ID and create item using the Item constructor. Return the found item object.
9. _**Clear All**_

Controller Routes and Views:
![Image description](/ToDoList/wwwroot/images/todolist_routes.png)


### Complete setup/installation instructions
1. _Go to GitHub profile and project @ [TanviCodeLife](https://github.com/TanviCodeLife/word-counter-csharp-proj)_
2. _Use #git clone <project url> command to pull it to a local repository in your Home directory using a bash terminal._
3. _Go to project folder and cd into word-counter-csharp-proj/WordCounter directory from your bash terminal_
4. _run #dotnet run_
5. _Wait till you see this message display in you bash terminal - "Now listening on: http://localhost:5000
Application started. Press Ctrl+C to shut down."_
6. _Copy the local host link you see in the message for eg http://localhost:5000 and paste it into your web browser address bar._
7. _Browse through the project._

### Technologies used
1. **CSharp**
2. **.Net Core MVC Framework**
3. **MSTest BDD Workflow(Unit Testing for Model and Controller)**
3. **Atom**
4. **Command Line**
5. **GitHub**

### Known Bugs
_No known bugs._

### Contact information
_tanvi.garg23@gmail.com_

### Support
* _For issues, please contact authors at the provided contact information above._

MIT License
