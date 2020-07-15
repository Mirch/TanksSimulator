# Tanks Simulator

![API CI Pipeline](https://github.com/Mirch/TanksSimulator/workflows/API%20CI%20Pipeline/badge.svg)

This repository contains my solution for the tanks simulator code challenge. It's developed in `ASP.NET Core 3.1`, with a `MongoDB` database, and runs using `docker` and `docker-compose`.

## Solution structure

The solution contains 4 projects:
- **TanksSimulator.WebApi** - the API that allows you to get information from the database and start new game simulations;
- **TanksSimulator.Game** - a class library that contains all the game logic;
- **TanksSimulator.Shared** - a class library that contains classes that are shared by the API and Game projects; 
- **TanksSimulator.WebApi.Tests** - contains (some) unit tests for the Web API, in order to use the testing step in the CI pipeline; 

## API

The format of the endpoints is e.g. `api/v1/Maps/{mapId}`. All the endpoints can be seen in Swagger, by running the project and navigating to the index page:

![API](https://i.imgur.com/4ttuUZa.png)

### Entities and maps

#### Tanks
Tanks are made up of 3 components: a main body, a barrel and road wheels. Any of them can be hit durring a battle, resulting in different events.
You can choose between 3 tanks:
Tank Name | Tank ID | Description
------- | --------- | ------------
Swifty | 5f0c62691d218f63246c9e9b | High range, low damage
Chunky | 5f0f5d792ad06286095c9b2b | Low range, high damage
Simply | 5f0f5e0a2ad06286095c9b2c | Midrange, mid damage

#### Maps
Maps are simple and only composed of two types of tiles: plain and rocks - tanks can not travers rock tiles.
You can also choose between the following maps:
Map ID | Description
-------|---------------
5f0e051a40e3fe550cc58679 | 50x50, griddy map, full of obstacles
5f0f60409af9bdf5cb6f9779 | 50x50, more open space, but still contains good chunks of solid blocks

### Battles
To simulate a battle, you need to provide 2 tank IDs and a map ID:
```
{
  "tanks": [
    "5f0c62691d218f63246c9e9b", "5f0f5d792ad06286095c9b2b"
  ],
  "mapId": "5f0e051a40e3fe550cc58679"
}
```

## The actual game

The game simulates a battle between two given tanks, on a given map. The map is saved in the database in text form, sent to the `GameSimulator` and converted into a list of `Tile` objects at runtime. Tanks are randomly placed on the map at the beginning of each battle; one of them is randomly chosen to act first, and then they take turns making their moves. The following moves are possible:
- Moving one tile in any direction (either towards the opponent using an A* pathfinding algorithm, or somewhere where the opponent can't shoot);
- Shooting the opponent (randomly between the main tank body, the barrel and the roadwheels);

Each tank's action generates an **event** - which tells the game whether the action was finished, or if there is something else that needs to happen. Events can chain other events, which will be triggered at the beginning of the next turn. This is an example of an event flow:
- `Tank1` shoots `Tank2`, generating a `TankShootEvent`;
- The `TankShootEvent` calculates the shot to be in `Tank2`'s barrel for 15 damage;
- `Tank2`'s barrel is now destroyed;
- The `TankShootEvent` sees that `Tank2`'s barrel is destroyed, and chains a `TankBarrelRepairingEvent` for next turn - this lets `Tank2` repair its barrel during the next 2 turns;
- At the beginning of the next turn, the `TankBarrelRepairingEvent` is trigerred, which chains another `TankBarrelRepairingEvent` for next turn;
- Once the barrel is repaired, a `Succeeded` event is returned, and the game continues normally.

The game ends when one of the tank's main body is destroyed.   
