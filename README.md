A game created to test concurrency and objects count.

![image](https://github.com/pjmatuck/exploding-elves/assets/3275775/0c287bb3-48a2-4557-8342-1042e6499e2f)

Exploding Elves
-
Instructions to Play using Unity Editor
- 
1. Go to Assets/Scenes folder
2. Open the GameStartUIScene
3. Click on play

This project was planned to be developed in a kind of waterfall mode. Divided into "Runs", the strategy was create the main mechanics first and than iterate to improve them. The improvements complies since computing resources management until experience regarding the game in general.

RUN #1
-
In the first run the focus was create a basic maze, an spawner (aka Player) and make it spawn objects over the maze. How the objects would move and collide with maze was also considered in this moment. Unity Physics was adopted to set some fluidity and reduce the determinism of the movements.

RUN #2
-
After create the first spawner, other Players was configured and the interactions rules among them were implemented. In this point, the rule "Time to Spawn" was implemented to avoid successive uncontrolled collisions.
This new property lead the Player to different states, like "Spawning" and "Running", so a Finite State Machine was adopted inside Player Controller (file: ElfController.cs).
In order to set some archtecture pattern on the project, all objects were built in a adaptation of MVC pattern.
- Views: Visual objects like FBX and prefabs.
- Controllers: Scripts responsible to apply mechanics and bind models to views
- Models: ScriptableObjects which holds the object state.

RUN #3
-
To reduce the amount of memory allocated for spawners and replace the expensive Instantiate/Destroy mechanic, an Object Pool was created to manage objects scene lifecycle. All spawners mechanic were replaced by Pooling mechanic.
The Object Pool was created using Queue and in a non-lazy mode. Instead of instantiate all objects when requested, it is created under demand, as long as there is no more available objects on the pool.

RUN #4
-
In the fourth run the UI was included to control the objects spawn speed. Also following the adaptation of MVC pattern, in this case a View script was adopted to expose Views properties and allows controllers bind model data in UI elements. As we can see between UIPlayerControlView.cs, AbstractSpawnerController.cs and Spawners models.
The UI actions were developed using a Command pattern, allowing the encapsulation of these actions inside objects.
At this moment the project had two systems: Spawners and UI. To create a relationship among them a Service Locator pattern was implemented.

RUN #5
-
To improve the game experience an intro screen was implemented to allow "Number of Player" options. This new option leads to adopt a dynamic instantiaton of the players, i.e., depending on how many players was choose, the number of spawners and UI controllers should be instantiated accordinly.
A general GameController was created to attend this task. It is responsible to read from PlayerPrefs and instantiate the Players. This scripts is also responsible to hold the Players configurations. The Player entity is a match among maze spot, spawner and elf object.

RUN #6
-
A Particle System was included to handle the VFX for Elves destruction collision. To optimize the instantiation of particles two major changes were implemented:
- Create the AbstractSpawner controller. Allowing the creation of different spawners.
- Generic Object Pool. From now on the object pool supports any UnityEngine.Object.

RUN #7
-
An Audio System was implemented to include some SFXs for Elves collisions.

RUN #8
-
An background image and some camera adjustements were applied for visual improvements.

RUN #9
-
Tests and setup improvements.

![image](https://github.com/pjmatuck/exploding-elves/assets/3275775/deb7ff81-93c4-43b3-bf87-aad77bb5578e)

