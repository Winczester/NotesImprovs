README

NotesImprovs

Overview
This is an ASP.NET Core Web API project that uses:
	•	PostgreSQL as the database.
	•	Redis for caching.
	•	Docker Compose to orchestrate the multi-container setup.
	•	Entity Framework Core for data access with automated migrations.
	•	JWT authentication for secure API access.
 
Prerequisites
Before you begin, ensure you have the following installed:
	•	Docker (v20.10+ recommended)
	•	Docker Compose (v2.0+ recommended)
 
Getting Started

Running the Application with Docker
Clone the repository: git clone https://github.com/Winczester/NotesImprovs.git
cd your-repository

Build and run the Docker containers:  docker-compose up —build
 This command will:
Build the Docker image for your ASP.NET Core Web API.
Start the PostgreSQL, Redis, and your project containers.
Automatically apply any pending database migrations.

Access the API: Once the containers are up and running, you can access the API at: http://localhost:5050/swagger

Stopping the Application
To stop the running containers:
docker-compose down

Accessing Swagger UI
	•	Swagger URL: http://localhost:5000/swagger
	•	This page provides a web interface to explore and interact with the API endpoints.

 
Authenticating in Swagger
To interact with secure endpoints in Swagger, you need to authenticate using a JWT token. Follow these steps:

Register a User: Use the /api/auth/register endpoint in Swagger to register a new user. You'll need to provide:  
{
  "email": “your email,
  "password": "your password”,
  "userName": "your username”
}

Log in to Get a JWT Token: Use the /api/auth/login endpoint to log in with your registered credentials. This will return a JSON response containing an accessToken and a refreshToken: 
{
  "accessToken": "your_jwt_access_token",
  "refreshToken": "your_jwt_refresh_token"
} 

Authorize in Swagger:
Copy the accessToken value.
Click the Authorize button (a padlock icon) in the top-right corner of the Swagger UI.
Enter {your_jwt_access_token} in the authorization dialog (replace {your_jwt_access_token} with the actual token value). Example: 

eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9... 

Using the API: After authorization, you can interact with the secure API endpoints. The authorization will be valid for the duration of the token's lifetime.


