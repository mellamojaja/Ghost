# Ghost game

This project contains several sub-project related to the [game Ghost](https://en.wikipedia.org/wiki/Ghost_(game)):

* **Ghost.Library** is a C# DLL with the logic to run the game. It has  quite a complete ontology of classes to be able to define and run almost any kind of game. It does a heavy use of inheritance and abstractions, as well as use some design patterns such as **singleton**, **factory** and **publisher-subscriber**
*  **Ghost.AI.API** is a REST API that exposes the 2 resources needed to simulate a computer player y be able to play the game: 
     * **NextMove**, returns the best move given the current state
     * **Analyse**, evaluates the current state and returns useful information like the most probably winner, the best moves list and the highest number of moves until one player looses.
* **Ghost.AI.API.Tests** and **Ghost.AI.API.SystemTests** are both solutions with some tests for the API. The first one has only **unit tests** while the other has **system test**
* **Ghost.Console** is a C# console application to play the game. It allows to create 3 types of player: human, not so intelligent AI and optimal AI. It access the logic of the game through the "Ghost.Library"
* **Ghost.MVC** is a ASP.NET MVC application to play the game against the optimal AI. It access the logic of the game through the "Ghost.AI.API". 

## TODO list
There are many thing pending, but these are on the top of the list now: 
1. Add asynchronous request to the client side of the MVC application.
2. Add GUI tests
3. Deploy the API and the web application to the cloud
4. Mask the input box in the web application
5. Implement the "pass" functionality: if is the user's turn and he thinks the previous letter inserted by his opponent was incorrect because the word doesn't not exist, then he could click the "pass" button. This way, the other player would have to complete the word, or will loose the game
6. Record the score of the players of the web application in a database. This way I would be able to implement some form of persistence.

## Getting Started

Clone/download the whole repository.

### Prerequisites

The solution has been created in VS 2017 and has not been tested in any other version. So please, for better compatibility, use that version.

Also, when running the projects, don't change the port of the web API.

## Running the tests

The tests are in "Ghost.AI.API.Tests" and "Ghost.AI.API.SystemTests" projects. Note that, for the tests of  the latter, you must have "Ghost.AI.API" up and running 

## Deployment

No additional help for deployment has been added so far. It is definitely high in my TODO list