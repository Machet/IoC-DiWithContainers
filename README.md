## About project ##
This is standard ASP.MVC app that simulate cinema. User is able to book seat for a seance. This project was created to exercise with using Dependency Injection (DI) with Inversion of Control (IoC) Containers. Currently application is composed using manual class wiring, choose preferred container and replace it.
##Setup
Before tearing project apart play with it a little. Check what is happening when you try to book seat for movie. For logging in you can use accounts **user1 - user9** with **password 123**. 

When you are ready delete everything from `IoCCinema.CompositionRoot` except `ContextUserProvider` class, then remove lines containing errors. You are ready to introduce chosen container - download it from nuget along with Mvc supporting library if exist.

## Composing application
#### Task 1 - Displaying list of movies
First task is just to display list of all movies. To perform this HomeController needs be created along with MovieViewRepository. When classes are properly wired make sure that CinemaContext is properly disposed after each request.

#### Task 2 - Ability to login
When you choose movie and time application forces you to login. This is handled by LoginController and proper CommandHandler. Make sure that you apply TransactionalCommandHandler as decorator, otherwise changes made by command won't be persisted to database.

#### Task 3 - Displaying all screens
There is two more screens that display data to user: Notification and Audit. Its tedious to register repositories every time we add new screen to application. Add convention to wire repositories

#### Task 4 - Ability to perform all commands
Simmilary to repositories there should be convention for registering all command handlers at once. Additionally to support next command it is required to provide custom WinChanceCalculatorFactory.

#### Task 5 - Auditing
Add AuditingCommandHandler decorators for all commands. As bonus task make sure that passwords won't be logged by using LoggingAuditingCommandHandler decorator instead default one.

#### Task 6 - Event Handling
Provide custom implementation of DomainEventBus. Add auditing for events using AuditingEventHandler decorator and standalone AuditEventOccurrenceHandler class. Examine Audit & Notification view to check if all audits and all events are fired up correctly. Ensure that if you win a lottery ticket then only one notification is send.
