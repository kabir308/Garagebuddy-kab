# Garage Buddy Mobile App (Flutter)

This directory contains the source code for the Garage Buddy mobile application, built with Flutter.

## Project Structure

The project follows a standard feature-driven architecture to keep the code organized and scalable.

-   `lib/`: Contains all the Dart code for the application.
    -   `main.dart`: The entry point of the application.
    -   `screens/`: Contains the main pages or screens of the app.
    -   `widgets/`: Contains reusable UI components used across different screens.
    -   `models/`: Contains the data model classes (e.g., `Job`, `Customer`).
    -   `services/`: Contains business logic, such as services for API communication (`ApiService`).
    -   `providers/`: Contains state management classes using the Provider pattern.

## Getting Started

To get started with this project, you will need to have the Flutter SDK installed.

1.  **Get dependencies:**
    ```bash
    flutter pub get
    ```

2.  **Run the application:**
    ```bash
    flutter run
    ```

## Dependencies

-   `http`: For making network requests to the backend API.
-   `provider`: For simple and efficient state management.
