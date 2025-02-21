# Projectflow

ProjectFlow is a web application that lets users create public or private projects. Others can request to join a project, and the project owner has the ability to accept or decline these requests. Once accepted, the user becomes a project member.

## Domain

This is a high level overview of the current domain:

![Domain Overview](/docs/Images/Domain-Overview.png)


## Features

### Project Management
- Users can create new projects.
- Project owners can update or delete their projects.

### Membership & Collaboration
- Users can request to join a project.
- Project owners can accept or decline join requests.
- Project members can leave a project at any time.
- Project owners can remove members from their projects.


## Examples

### Generate a token

```yaml
POST {{host}}/api/identity/token
Content-Type: application/json
```
> Note: The application uses its own token generator endpoint to make the authentication process simple,
but still act as the token was provided from an external identity provider.
 Provided userId needs to exists in the in memory database.

#### Requestbody needs to contain:

```json
{
  "userId": "2c05da8e-47ab-4260-a7ec-5ec2342fd547",
  "email": "testuser@test.de"
}
```

#### Response

The request should return a JSON Web Token.

```
"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiJlOGI5MWQyMy05NzA4LTRkNDktYjE1MS0yYTIzY2Q0NWMxODQiLCJzdWIiOiJ0ZXN0dXNlckB0ZXN0LmRlIiwiZW1haWwiOiJ0ZXN0dXNlckB0ZXN0LmRlIiwidXNlcmlkIjoiMmMwNWRhOGUtNDdhYi00MjYwLWE3ZWMtNWVjMjM0MmZkNTQ3IiwibmJmIjoxNzQwMTExNTcyLCJleHAiOjE3NDAxNDAzNzIsImlhdCI6MTc0MDExMTU3MiwiaXNzIjoiaHR0cHM6Ly9pZC5ldGhlbXNhaGJhei5jb20iLCJhdWQiOiJodHRwczovL3Byb2plY3RmbG93LmV0aGVtc2FoYmF6LmNvbSJ9.b0WowQo-I4msSyeZFcjeYjdpFyogHC3UhhRK1paUFQw"
```

### Create a project

Projects can be created on following endpoint
```yaml
POST {{host}}/api/projects
Content-Type: application/json
```

#### Requestbody needs to contain

```json
{
    "Name": "Projectflow",
    "Description": "Building a projectmanagement application.",
    "IsPublic" : true
}
```

A valid request should return a `201 Created` Response.

```json
{
    "id": "1bd5ebec-1e56-49a4-96d1-02f1cac24962",
    "name": "Projectflow",
    "description": "Buidling a projectmanagement application",
    "isPublic": true
}
```

#### Validation
There is some validation for specific endpoints. Trying to create project without a name will result in a validation error with a `400 Bad Request`.

```json
{
    "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
    "title": "One or more validation errors occurred.",
    "status": 400,
    "errors": {
        "Name": [
            "Project name is required"
        ]
    }
}
```