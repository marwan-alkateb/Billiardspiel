# Goals for WiSe 2021/2022

## Team Documentation
* Scrum documentation ✅
    * general workflow and explanations 
    * retrospective 
    * review  
* modify folder structur, sort files and remove unused / not needed files ✅
* project documentation  ✅
    * how to open, start and execute each project 
    * general explanations 
* mkdocs ✅
    * installed and initialized 
    * documentation for how to install and use mkdocs 
    * docker container for php, pip and mkdocs which are needed to run mkdocs 
* update doxygen code documentation ❌

## Team Game
* Provide the game / rule logic for all components: ✅
    * Simulator 
    * Real Table 
        * provide the interface / use the real tracking input in order to check all game rules 
* Each mode should run correctly without any bugs and show fouls on the UI ✅
    * 8 Ball  
    * 9 Ball 
    * Trickshot 
* Redeploy the physics for the simulator ⚠️
    * Adding new component to unity for calculating the rolling friction 
        * in order to have a correct game physic 

## Team Tracking
* new tracking algorithm ✅
    * Automatic calibration on the table 
    * Stable ball detection 
    * Stable ball classifier (color and type) 
    * Touch detection with cue 
* Tracking server ✅
    * Own process for tracking, independent of Unity 
    * Networking API for Inter Process Communication 
        * Clients such as Unity receive tracking results from the server 
* Full compatibility with the real table ⚠️

## Team UI
* Include and display all necessary elements of the menu ⚠️
    * Transitions between menus ❌
    * Rules for game variants ✅
    * Guideline from Cue ✅
    * Revise in-game player information ✅
        * Icon for full and half balls 
        * Number of balls holed per player 
    * Installation of buttons ✅
        * (Back button, button for further game variants)  ✅
* Revision and embellishment of the UI elements (texts, buttons) ⚠️
    * Sound settings, depending on the strength of the shock ✅
    * Power indicator for impact ❌
    * Uniform design everywhere ✅
        * Nicer buttons
        * With icons and text
        * Hover effect on buttons
        * For recruitment and info
        * The name input is hard to recognize
    * Animations ✅
        * Prize animation 
    * GUI should be on top of everything else ❌
        * (Remove the play utensils in the initial menu so that you can see the texts)
    * Display prefab (projection of elements onto the real table) ⚠️