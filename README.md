# ✈️ FlightBooking System

**FlightBooking** is a modern flight reservation and management system built with **ASP.NET Core MVC and .NET 8**.

The system provides a complete flight booking workflow together with passenger management, multi-step check-in, seat selection, additional services, payment handling, weather and local-time information, an AI-powered travel assistant, and machine-learning-based no-show and overbooking analysis.

## ✨ Features

### 👤 Customer / Passenger Portal

The customer portal provides a complete flight booking and travel experience.

* Search and filter available flights
* View flight information and schedules
* Create flight reservations
* Generate PNR codes
* Manage passenger information
* Multi-step check-in process
* Interactive seat selection
* Select baggage options
* Select meal preferences
* Add additional services
* Dynamic price calculation
* View reservation and check-in information

### 🤖 AI Travel Assistant

FlightBooking includes an AI-powered travel assistant using **Llama 3.1**.

The assistant can provide personalized recommendations for:

* 🏙️ Cities
* 🍽️ Restaurants
* 🏨 Hotels
* 🗺️ Tourist attractions
* 🚇 Public transportation

The application also keeps AI interaction history through the `AIChatHistory` entity.

### 🌦️ Weather & Local Time

The system integrates destination-based weather and local-time information.

Users can view information such as:

* Current temperature
* Humidity
* Wind speed
* Local time
* Destination weather conditions

### 🛫 Flight Management

Administrators can manage flights through the administration dashboard.

Flight management includes:

* Create flights
* Edit flights
* View flight details
* Filter flights
* Monitor flight status
* Manage airlines
* Manage routes
* Manage departure and arrival information
* Configure aircraft capacity
* Manage base pricing

Supported flight statuses include:

* Scheduled
* Completed
* Delayed
* Cancelled

### 👥 Passenger & Reservation Management

Administrators can manage passenger and reservation information.

Features include:

* Passenger roster
* Reservation management
* PNR management
* Check-in status monitoring
* Payment status monitoring
* Passenger information editing
* Passenger data export to Excel

### 🧳 Multi-Step Check-In

FlightBooking provides an interactive **5-step check-in workflow**.

The process includes:

1. Baggage selection
2. Seat selection
3. Meal selection
4. Additional services
5. Final confirmation

The system calculates additional service costs dynamically and updates the final reservation price.

### 💳 Payment & Pricing

The booking workflow supports dynamic pricing based on selected services.

Additional costs can be calculated for:

* Baggage
* Meals
* Additional services

The payment architecture is designed around **Factory and Strategy patterns**, allowing payment providers and strategies to be extended without changing the main application logic.

## 🧠 AI & Machine Learning

One of the main features of FlightBooking is its AI-based risk and forecasting system.

### 📊 No-Show Analysis

Historical flight information is used to analyze passenger no-show behavior.

The system can evaluate:

* No-show rates
* Flight demand
* Passenger density
* Time-slot patterns
* Historical booking behavior

### 📈 Overbooking Forecast

The application uses **ML.NET** to generate predictive overbooking and demand analytics.

The system provides:

* Demand forecasting
* Passenger density forecasting
* No-show prediction
* Overbooking recommendations
* Risk classification
* Slot-based analysis
* Interactive risk visualization

Forecasting can be analyzed across different time slots such as **Morning** and **Evening**.

### 🔥 Risk Intelligence

The administration dashboard provides an AI Risk Intelligence Center with:

* Overbooking forecasts
* No-show analysis
* Dynamic overbooking recommendations
* Risk classification
* Slot × Week risk heatmaps
* Future demand forecasts

## 🏗️ Design Patterns

The project uses several software design patterns to keep the system modular and maintainable.

### 🗂️ Repository Pattern

Database operations are encapsulated inside dedicated repositories for entities such as flights, passengers, reservations, and check-ins.

This separates data access from business logic and improves maintainability and testability.

### 🔄 Unit of Work

The Unit of Work pattern coordinates operations that must be completed together during reservation and check-in workflows.

For example:

* Creating reservation records
* Updating selected seats
* Calculating additional service costs
* Updating payment information

### 🏭 Factory & Strategy

Factory and Strategy patterns are used for payment processing and AI forecasting.

This allows different strategies or providers to be selected dynamically without modifying the main business logic.

### 🔗 Chain of Responsibility

Reservation and check-in requests pass through a sequence of validation handlers.

Typical validation includes:

1. Flight status validation
2. Capacity and overbooking validation
3. Passenger validation
4. PNR verification

Each handler performs its own responsibility before passing the request to the next handler.

### 👁️ Observer Pattern

The Observer pattern is used for important flight and reservation events.

Events can trigger:

* Audit and operational logs
* Email notifications
* SMS notifications
* AI risk dashboard updates

## 🗄️ Database

FlightBooking uses **MongoDB** as its database.

The main entities include:

| Entity                  | Description                                            |
| ----------------------- | ------------------------------------------------------ |
| **Flight**              | Flight routes, schedules, capacity, status and pricing |
| **Passenger**           | Passenger information and contact details              |
| **Reservation**         | Flight reservations and generated PNR codes            |
| **CheckIn**             | Check-in status, seats, baggage and meal preferences   |
| **SeatMap / Slot**      | Aircraft seating layouts and slot occupancy            |
| **OverbookingForecast** | ML.NET forecasting and risk metrics                    |
| **AIChatHistory**       | AI travel assistant interaction history                |

DTOs are used throughout the application to separate API/application data from domain entities.

## 🛠️ Technologies

The project is built using:

* **C#**
* **ASP.NET Core MVC**
* **.NET 8**
* **MongoDB**
* **ML.NET**
* **Llama 3.1**
* **Razor Views**
* **HTML / CSS / JavaScript**
* **AutoMapper**
* **FluentValidation**

### NuGet Packages

* `AutoMapper`
* `FluentValidation`
* `Microsoft.ML`
* `Microsoft.ML.FastTree`
* `MongoDB.Bson`
* `MongoDB.Driver`
* `MongoDB.Driver.Core`

## 📁 Project Structure

```text
FlightBooking/
│
├── AgentServices/
├── AgentSettings/
├── Areas/
│   └── Admin/
│
├── Controllers/
├── Dtos/
├── Entities/
├── MachineLearningModels/
├── MachineLearningRegressionModels/
├── Mapping/
├── Models/
├── Services/
├── Settings/
├── Tools/
│   └── WeatherTool/
│
├── ViewComponents/
├── Views/
├── wwwroot/
│
├── Program.cs
├── appsettings.json
├── appsettings.Development.json
├── FlightBooking.csproj
├── FlightBooking.sln
└── README.md
```

## 🚀 Getting Started

### Requirements

To build and run FlightBooking, you need:

* .NET 8 SDK
* Visual Studio 2022 or newer
* MongoDB
* Internet connection for external integrations
* Configuration for the required AI, weather, notification, and other external services

### Clone the Repository

```bash
git clone https://github.com/fazilmemmedzade/FlightBooking.git
```

Navigate to the project:

```bash
cd FlightBooking
```

Run the application:

```bash
dotnet run
```

Alternatively, open `FlightBooking.sln` in Visual Studio and run the project.

> Configure the required application settings and MongoDB connection before running the application.

## 🎯 Project Purpose

FlightBooking was created as a full-stack flight reservation project that combines traditional booking functionality with **AI and machine learning**.

The project demonstrates how different technologies and architectural patterns can work together in a real-world aviation scenario.

The main workflow can be summarized as:

**Search Flight → Book → Check-In → Select Seat & Services → Confirm → Analyze**

In addition to the customer-facing booking experience, the system provides an administrative environment for managing flights, passengers, reservations, check-ins, and AI-powered operational analytics.

## 👨‍💻 Author

**Fazil Məmmədzadə**

<a href="https://github.com/fazilmemmedzade">Github</a>
<a href="https://fazilmemmedzade.github.io/Portfolio">Portfolio</a>


## 📸 Screenshots

|                                            User - Flight Search                                            |                                          User - Flight Search (2)                                          |
| :--------------------------------------------------------------------------------------------------------: | :--------------------------------------------------------------------------------------------------------: |
| <img width="460" alt="User 1" src="https://github.com/fazilmmmdzad/My-Files/blob/main/PageForUser1.png" /> | <img width="460" alt="User 2" src="https://github.com/fazilmmmdzad/My-Files/blob/main/PageForUser2.png" /> |

|                                            User - Flight Booking                                           |
| :--------------------------------------------------------------------------------------------------------: |
| <img width="920" alt="User 3" src="https://github.com/fazilmmmdzad/My-Files/blob/main/PageForUser3.png" /> |

|                                            Admin - Flight List                                            |                                            Admin - Flight Detail                                            |
| :-------------------------------------------------------------------------------------------------------: | :---------------------------------------------------------------------------------------------------------: |
| <img width="460" alt="Admin 1" src="https://github.com/fazilmmmdzad/My-Files/blob/main/FlightList.png" /> | <img width="460" alt="Admin 2" src="https://github.com/fazilmmmdzad/My-Files/blob/main/FlightDetail.png" /> |

|                                            Admin - Create Flight                                            |                                            Admin - Booking List                                            |
| :---------------------------------------------------------------------------------------------------------: | :--------------------------------------------------------------------------------------------------------: |
| <img width="460" alt="Admin 3" src="https://github.com/fazilmmmdzad/My-Files/blob/main/CreateFlight.png" /> | <img width="460" alt="Admin 4" src="https://github.com/fazilmmmdzad/My-Files/blob/main/BookingList.png" /> |

|                                            Admin - Create Booking                                            |                                          Admin - Create Check-In (1)                                          |
| :----------------------------------------------------------------------------------------------------------: | :-----------------------------------------------------------------------------------------------------------: |
| <img width="460" alt="Admin 5" src="https://github.com/fazilmmmdzad/My-Files/blob/main/CreateBooking.png" /> | <img width="460" alt="Admin 6" src="https://github.com/fazilmmmdzad/My-Files/blob/main/CreateCheckIn1.png" /> |

|                                          Admin - Create Check-In (2)                                          |                                          Admin - Create Check-In (3)                                          |
| :-----------------------------------------------------------------------------------------------------------: | :-----------------------------------------------------------------------------------------------------------: |
| <img width="460" alt="Admin 7" src="https://github.com/fazilmmmdzad/My-Files/blob/main/CreateCheckIn2.png" /> | <img width="460" alt="Admin 8" src="https://github.com/fazilmmmdzad/My-Files/blob/main/CreateCheckIn3.png" /> |

|                                          Admin - Create Check-In (4)                                          |                                           Admin - Create Check-In (5)                                          |
| :-----------------------------------------------------------------------------------------------------------: | :------------------------------------------------------------------------------------------------------------: |
| <img width="460" alt="Admin 9" src="https://github.com/fazilmmmdzad/My-Files/blob/main/CreateCheckIn4.png" /> | <img width="460" alt="Admin 10" src="https://github.com/fazilmmmdzad/My-Files/blob/main/CreateCheckIn5.png" /> |

|                                           Admin - Over Booking Forecast (1)                                          |                                           Admin - Over Booking Forecast (2)                                          |
| :------------------------------------------------------------------------------------------------------------------: | :------------------------------------------------------------------------------------------------------------------: |
| <img width="460" alt="Admin 11" src="https://github.com/fazilmmmdzad/My-Files/blob/main/OverBookingForecast1.png" /> | <img width="460" alt="Admin 12" src="https://github.com/fazilmmmdzad/My-Files/blob/main/OverBookingForecast2.png" /> |

|                                           Admin - Over Booking (1)                                           |                                           Admin - Over Booking (2)                                           |
| :----------------------------------------------------------------------------------------------------------: | :----------------------------------------------------------------------------------------------------------: |
| <img width="460" alt="Admin 13" src="https://github.com/fazilmmmdzad/My-Files/blob/main/OverBooking1.png" /> | <img width="460" alt="Admin 14" src="https://github.com/fazilmmmdzad/My-Files/blob/main/OverBooking2.png" /> |

|                                           Admin - Over Booking (3)                                           |                                             Admin - Predict                                             |
| :----------------------------------------------------------------------------------------------------------: | :-----------------------------------------------------------------------------------------------------: |
| <img width="460" alt="Admin 15" src="https://github.com/fazilmmmdzad/My-Files/blob/main/OverBooking3.png" /> | <img width="460" alt="Admin 16" src="https://github.com/fazilmmmdzad/My-Files/blob/main/Predict.png" /> |

|                                            Admin - No Show Analysis                                            |                                            Admin - January 2027 Forecast                                            |
| :------------------------------------------------------------------------------------------------------------: | :-----------------------------------------------------------------------------------------------------------------: |
| <img width="460" alt="Admin 17" src="https://github.com/fazilmmmdzad/My-Files/blob/main/NoShowAnalysis.png" /> | <img width="460" alt="Admin 18" src="https://github.com/fazilmmmdzad/My-Files/blob/main/January2027Forecast.png" /> |

|                                              Admin - Agent (1)                                             |                                              Admin - Agent (2)                                             |
| :--------------------------------------------------------------------------------------------------------: | :--------------------------------------------------------------------------------------------------------: |
| <img width="460" alt="Admin 19" src="https://github.com/fazilmmmdzad/My-Files/blob/main/AgentPage1.png" /> | <img width="460" alt="Admin 20" src="https://github.com/fazilmmmdzad/My-Files/blob/main/AgentPage2.png" /> |

|                                              Admin - Agent (3)                                             |
| :--------------------------------------------------------------------------------------------------------: |
| <img width="920" alt="Admin 21" src="https://github.com/fazilmmmdzad/My-Files/blob/main/AgentPage3.png" /> |
