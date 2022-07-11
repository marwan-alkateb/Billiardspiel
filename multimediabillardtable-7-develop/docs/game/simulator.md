# Simulator

## Game Physics

The Unity game physics do not support rolling friction. Therefore it's currently not the best way to implement the physics. 

If you want to improve the ball physics you have to partially implement an own physics engine for the rolling friction.

**TIP**: The game physics are not as easy as it may look like!

## Simulator Usage
The main menu has multiple buttons, shown on an 3-dimensional billiard table with the usual balls in their starting positions along with cue and white ball on the left side. After choosing 'Play' the user can select the game mode he wants to play or select 'Rules' to get an overview over the rules of the different game modes. 

Once a game mode is chosen, you have to configure your game. Just enter your choices into the predetermined fields for each asked configuration. Click on 'Play' if you want to play with the made configurations or click 'Back' if you want to cancel and go back to the main menu.

For playing, the user can move around the mouse-cursor to move the cue around the white ball. At the beginning the white ball has a fixed position on the table and is not allowed to be changed. While moving around the cue you can see a dotted white direction line which shows the player in how the ball is going to roll if hitted from the cue from that direction.

To bump the white ball, simply click (and hold) Left-Mouse-Button (LMB) on the billiard table and an animation executes where the cue moves away backwards from the white ball. The longer the LMB is held, the further away the cue goes from the white ball. While LMB is pressed, the position at which the cue is going to push the white ball won't change even though cursor is moved around, the shot direction is locked to the position of LMB click. When LMB is released, the cue moves forward to bump the white ball. The force with which the white ball is pushed by the cue depends on how long LMB is held before release. 

Whenever a ball hits a pocket, it will then be removed from the playing area and is instead spawned to a collection box next to the billiard-table.  
When the balls are out of the table, they respawn on the billiard table.

If the user wants to quit the game, press [Esc] to get back to the main menu and press [Space] to reset the game.
Press [S] once to open the sound menu and press it again to close the sound menu.

## Tracking Support 
The Game-scene of the Simulator includes a separate camera that is set up to only capture the playing area of the billiard table (unlike the default camera for gameplay, where also the collection box is visible).  
This camera uses a special shader that converts the captured image footage into a greyscale image, where the grey-values convey depth-information.  
This image is then saved as the file 'DepthTexture' within `Assets/TrackingApi` of Project-Simulator and overwritten everytime a new depth-image is rendered.

## Multi-Screen usage for the real table
The real table has three projectors next to each other to display everything on the table. Therefore in Unity there are three additional cameras used, so each of the camera shows one part of the billiard table.

Currently there is no functional screen for displaying the table without the balls.

The color of the table can be changed in `ActivateAllDisplays.cs` if needed for better tracking. 

The Multi-Screen functionality is only working if 4 or more screens are attached to the computer. This can also be changed in `ActivateAllDisplays.cs`.