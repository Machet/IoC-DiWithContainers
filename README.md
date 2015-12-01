## About project ##
This is standard ASP.MVC app that simulate cinema. User is able to book seat for a seance. This project was created to exercise with using Dependency Injection (DI) with Inversion of Control (IoC) Containers. Currently application is composed using manual class wiring, choose preferred container and replace it.
##Setup
Before tearing project apart play with it a little. Check what is happening when you try to book seat for movie. For logging in you can use accounts **user1 - user9** with **password 123**. 

When you are ready delete everything from `IoCCinema.CompositionRoot` except `ContextUserProvider` class, then remove lines containing errors. You are ready to introduce chosen container - download it from nuget along with Mvc supporting library if exist.

## Composing application
#### Task 1 - Displaying list of movies
First task is just to display list of all movies. To perform this HomeController needs be created along with MovieViewRepository. When classes are properly wired make sure that CinemaContext is properly disposed after each request.

#### Task 2 - Ability to login

#### Task 3 - Displaying all screens
#### Task 4 - Ability to perform all commands
#### Task 5 - Auditing
#### Task 6 - Event Handling
