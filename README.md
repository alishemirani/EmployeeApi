# EmployeeApi
An open api to create and manage employees which can be accessed from [here](https://cuemployeeapi.azurewebsites.net/index.html)

## Attention
This api is deployed to Azure app services on a free tier for demo purposes and it might be stopped at the time of testing. Please reach out if you want me to turn it on.

## Concepts
* `User:` represent a user that can login and use apis. for this simple api there are 2 use role defined. "Admin" and "User" which former have access read/write access to all entities while the latter can only view associated resources. once a user is defined it can be associated to an employee.
* `Employee:` represent an employee in an organization. an employee does not nessesarily have access to the api. if we want to give an employee access to use the api, it must be associated with a user. 

## How to test the api
to test this api one need to have [postman](https://www.postman.com) software installed and then import `Postman.json` from the root folder. this file defines a sequence of call to the api that starts with logging in to the application with default admin user and create an invalid and then a valid employee. this script is parametrized so it can be run using postman Collection Runner.

This implementation uses in-memory states but is flexible to wire different implementation. this means if the app is restarted it loses all information except the hardcoded default admin account as bellow 

```
username: admin
password: password
```