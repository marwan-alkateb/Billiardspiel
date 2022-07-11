# To the successors

## Advices

In the following sections there are some tips that may help you and show you what problems we had during our sprints. 

### General

1. Prepare sprint reviews in your team so when you have to present it you don't need to ask your team members who can show what. 
2. Respect each other and keep up the communication. Especially after the christmas holidays the communication went not as well as before. 
3. Push and pull regularly and merge small branches frequently to avoid merge conflicts. 
4. The CI/CD pipelines have to be re-established at the beginning of the project. 

### Unity

1. Unity merges are a bit more difficult than the others. Keep that in mind and remember doing merges frequently.

2. The game physics are not as easy to improve as it may look like. 

## Ideas to realize

### Documentation

* Add doxygen documentation.
* Modify docker for mkdocs so you don't have to rebuild the docker image after modifying some documentation files. 
* Keep the documentation up to date.

### Game

* Improve multi screen functionality
* Add game-mode for the real table (tracking)
* Customize the project physics for more realistic effects
    * Analyze static and dynamic friction coefficients with real life tracking
    * Determine the best friction values by applying artificial intelligence techniques
* Organising the project by
    * Utilizing the multi-scene workflow concept
    * Using more prefabs instead of extending the objects tree of the project
* Extend 9 ball rules

### Tracking

* Client integration into the Unity project
* Cue tracking system
    * Accuracy improvements
    * Touch detection -> GUI interaction
* Algorithmic improvements
    * Parameter fine-tuning
    * Hand / arm rejection

### UI

* Power bar indicator
    * Visualisation of how much power is used for the cue
* New background(s)
    * Bar / beach / ... scene
* Different color schemes for variety
    * Colorblind mode on / off
* Overlay for the main menu to prevent movement of cue and balls
* Transitions between the menus to have a more dynamic look / approach

