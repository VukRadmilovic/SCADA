# SCADA

## Tech Stack

- Backend:
  - C# ASP.NET Core
  - Entity Framework Core

- Frontend:
  - Angular

## Description

This project is a SCADA (Supervisory Control and Data Acquisition) system implementation. It allows users to add and remove analog and digital tags, manage alarms, register users and perform real-time monitoring and control of the system. The focus of the application is on efficient control of the system.

The system architecture consists of the following components:

- Database Manager: Provides a user interface to add/remove tags, enable/disable tag scanning, write output tag values, and display current output tag values. It also handles user registration and authentication, with user data stored in a database. It allows the management of alarms for input tags.

- Trending App: Displays the current values of input tags in the system.

- SCADA Core: The core of the SCADA system, responsible for server-client communication with other components. It includes the Simulation Driver and Tag Processing components. The Simulation Driver generates predefined signals (e.g., sine, cosine, ramp) on predefined I/O addresses. The Real-Time Unit generates values which simulate the work of sensors in the real world.

- Alarm Display: A client application to display system alarms which can be temporarily snoozed and information about whose activation is logged to a file (`logfile.txt`) and database.

- Report Manager: Provides various types of reports, such as all alarms within a specific time period (sorted by priority and time), alarms of a specific priority (sorted by time), values received from services within a specific time period (sorted by time), and the latest values of AI (Analog Input) and DI (Digital Input) tags.

## How to Run

### Backend

1. Clone the repository:
```
git clone https://github.com/VukRadmilovic/SCADA.git
```

2. Open the solution in Visual Studio.

3. Restore NuGet packages if prompted.

4. Configure the database connection string in the `Utils/DatabaseContext.cs` file.

5. Build the solution.

6. Run the migration commands to create the database schema:

```
Add-Migration InitialMigration
Update-Database
```

7. Start the backend server.

### Frontend

1. Navigate to the frontend directory:

```
cd SCADA/Frontend/bata-scada
```

2. Install dependencies:
```
npm install
```

3. Start the frontend development server:
```
ng serve
```

4. Access the application in your browser at `http://localhost:4200`.

Note: Additional configuration steps may be required depending on your environment.
