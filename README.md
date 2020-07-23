# Dejavu3x
Dejavu3x - Android game with custom telemetry and event logging system

Dejavu3x is an arcade game made with the Unity engine. I developed it for my bachelor thesis, in which I was researching the influence of user interface on a gameplay. 

The main purpose of this game was to collect anonymous data about user gameplay and their interaction with different on-screen game controllers. Collected data was later analyzed and presented in my bachelor thesis. I also embeded an interactive survey in the game. Players had to fill in the questions and provide feedback about the controllers and user interface.
I decided to implement my own telemetry and event logging system, so I didn't use the Unity Analytics. I also developed my own touch visualization system. 

The original version of Dejavu3x (2018) was able to run on a wide range of Android smartphones and was supported by almost every OS version. 
In order to ensure as large feedback as possible, I made the game light in size, fast paced and with low hardware requirements. Unfortunately, it does not work properly anymore, because the servers for data upload were shut down. 

I decided to revisit Dejavu3x and remaster it. I improved the graphics and made some corrections, while preserving the same visual style, GUI (controllers) and level layout. This repository contains the Dejavu3x source code in C#, image resources and compiled versions of the game. There is also a quick explanation of some source files below. The assets from the Unity Asset Store used in the making of this game are listed on the bottom of the page. The gameplay videos are available [here](https://www.youtube.com/playlist?list=PLw9WFaLfT0oM2f8FaGTFG7f98a7pjsYSL).  

:warning: <b>Dejavu3x is not meant for profit or monetization and it does not contain ads. I created it solely for educational and research purposes.</b>  


## Downloads (coming soon) :arrow_down:

[Normal build](./Build/dejavu3x.apk)<br></b>No device info or telemetry information displayed on-screen, no touch visualizations, survey enabled</b><br><br><a id="raw-url" href="./Build/dejavu3x.apk">![](./Icons/dejavu3x_icon.png?raw=true)</a>  |  [Debug / test build](./Build/dejavu3x_debug.apk)<br>Telemetry information visible on the screen, touch visualizations enabled, survey disabled<br><br><a id="raw-url" href="./Build/dejavu3x_debug.apk">![](./Icons/dejavu3xdebug_icon.png?raw=true)
:-------------------------:|:-------------------------:

## Goal :star:
In the game Dejavu3x, you play as a knight on a journey to the castle. 
You must collect the three keys in order to get through the castle gates. 
But each time you get a key, you are teleported back to the beginning of the level and the on-screen controller changes.
So you have to play through the level three times, each time with a different controller layout.
Be aware of the dangers. Avoid sharp spikes, chasms and water. And watch out for the enemy knight. 

## Screenshots :framed_picture:

![Alt text](./screenshots/screenshot1.jpg?raw=true "Dejavu3x screenshot 1")  |  ![Alt text](./screenshots/screenshot2.jpg?raw=true "Dejavu3x screenshot 2")
:-------------------------:|:-------------------------:
![Alt text](./screenshots/screenshot3.jpg?raw=true "Dejavu3x screenshot 3")  |  ![Alt text](./screenshots/screenshot4.jpg?raw=true "Dejavu3x screenshot 4")

## Differences :wrench:
There are some differences in comparison to the old build of the Dejavu3x:
- <b>Gameplay:</b> there are new and improved animations in the game. When a player falls on spikes or is hit by the enemy, a death animation starts playing. The knight becomes "bored" if there is no input from player for some time. The arrows now show the way at the penultimate jumping puzzle. Rewritten and improved player movement. Player now takes some time to turn and to jump on place. 
- <b>Graphics:</b> Increased texture and display resolution. Added more fog and smoke. Forced anisotropic filtering on all textures. Increased number of particles. Improved post processing: added chromatic abberation, blur and SSAO. 
- <b>Logging and telemetry:</b> updated logging and telemetry system. Logs are now split into multiple files. Changed the order of the data. Simplified the code for the data upload, because the original upload servers were shut down.
- <b>Hardware requirements:</b> while the original game required at least 1.2 GHz Quad-core CPU, new version requires at least 2.0 GHz Octa-core CPU. RAM requirements are increased from 1 GB to 3 GB. Vulkan API support is recommended. The game size is increased from 50 MB to 170 MB.


## Controllers :joystick:
On each playthrough a different controller is assigned randomly to the player. There are three types of on-screen controllers:
- <b>Digital Pad (DPad) (1):</b> consists of two directional buttons (left, right) and two action buttons (jump, attack), positioned in the lower part of the screen. 
- <b>Joystick (2):</b> consists of two action buttons (jump, attack), positioned in the lower right corner and a directional pad, which can be spawned by touching anywhere on the screen. 
Graphical elements of this controller fade out after some time of user inactivity.
- <b>Sticky keys (3):</b> consists of two directional and action buttons, arranged along left and right corner. Left and right button work as </i>sticky keys</i>. 
They stay pressed until the same key or other direction key is pressed again.


## Telemetry, touch visualization and event logging :pencil:
Various telemetry and logging scripts monitors user actions during the gameplay. All user touches on the screen (during the game) are recorded and stored in the log files. Event logger keeps track of gameplay events like jumps, respawns, deaths etc. The original version of the game stored all data in a single file, while the updated version splits telemetry data into multiple files.  

When the debug information is enabled, all touches on the screen are visualized by red semi-transparent circle and a text indicating the touch position and finger ID. Additionally, gameplay data is also printed on a screen.

![Alt text](./screenshots/debug/screenshot1_dgb.jpg?raw=true "Dejavu3x screenshot with debugging information enabled")


## Data format :books:
### Touch log
Data from the touches on the screen during the game is stored in a file <i>TOUCH_LOG.txt</i>. Every record has the following form of comma-separated values:

<code class="language-plaintext highlighter-rouge">[Time] [Finger ID] [Touch hit] [Position X] [Position Y] [Controller ID number]</code>

The value <i>Touch hit</i> represents the UI object, which is hit by the touch / finger. The value is an empty string, if the user misses UI objects and just touches the canvas. Controller ID Number represent the ID number of the current assigned controller.

### Game events log
<b>Event: Death</b>  
<code class="language-plaintext highlighter-rouge">[Event name] [Time] [Player position X] [Player position Y] [Trap name] [Number of death] [Controller ID number]</code>

<b>Event: Player jump, Sword attack or Enemy kill</b>  
<code class="language-plaintext highlighter-rouge">[Event name] [Time] [Player position X] [Player position Y] [Controller ID number]</code>

<b>Event: Player gets a key</b>  
<code class="language-plaintext highlighter-rouge">[Event name] [Time]</code>

### Survey results
User feedback is stored in a file <i>SURVEY_ANSWERS.txt</i>. The file contains numbers, which represent survey answers, separated with a newline.


## Source code :page_with_curl:
<code class="language-plaintext highlighter-rouge"><i>./Scripts/Game/AssignController.cs</i></code>
This script randomly assings the controller on each playthrough. It disables and enables corresponding controller Game Objects. It also keeps track of all previously assigned controllers. 

<code class="language-plaintext highlighter-rouge"><i>./Scripts/Game/DebugInfo.cs</i></code> 
The script gets some device information and prints it on the screen. At the startup, it writes this data to the log file. It also displays the FPS (Frames per second) and FT (Frame time).

<code class="language-plaintext highlighter-rouge"><i>./Scripts/Telemetry/InputStatistics.cs</i></code> 
The script gets the touches and sorts them. It counts hits and misses of UI. It can display touch visualizations and touch information, such as finger ID, position, radius and pressure.

<code class="language-plaintext highlighter-rouge"><i>./Scripts/Telemetry/Logger.cs</i></code> 
The script handles the log files. It creates new directories and log files, writes gameplay information and stores all telemetry data.

<code class="language-plaintext highlighter-rouge"><i>./Scripts/Screenshot/Screenshot.cs</i></code> 
The script contains public methods, which can be called to capture a screenshot of the game. Screenshot can be taken normally, without GUI or with GUI only. 

<code class="language-plaintext highlighter-rouge"><i>./Scripts/Game/LimitFPS.cs</i></code> and <code class="language-plaintext highlighter-rouge"><i>./Scripts/Game/LimitResolution.cs</i></code>
Useful scripts to limit FPS and display resolution in-game. Resolution width and height can be set independently or changed by the scale factor.  To provide the same gameplay conditions for all players, the game was originally limited to 30 FPS.

<code class="language-plaintext highlighter-rouge"><i>./Scripts/Player/Movement.cs</i></code> 
This script moves the player. It also handles some actions like jumping and attacking. 

<code class="language-plaintext highlighter-rouge"><i>./Scripts/Player/Respawn.cs</i></code> 
This script handles the respawning and restarts. It triggers the death and respawn particles and starts the phone vibration on death.

<code class="language-plaintext highlighter-rouge"><i>./Scripts/Player/BoundingBox/CheckTriggers.cs</i></code> 
Checks the collisions with the traps, keys or ground.

<code class="language-plaintext highlighter-rouge"><i>./Scripts/Enemy/EnemyScript.cs</i></code> 
Handles the enemy movement and attacking logic.


## Models, textures and other resources :construction:
I used the following paid and free assets from the Unity Asset Store:

<b>Touch Controls Kit</b> - [https://assetstore.unity.com/packages/tools/input-management/touch-controls-kit-12373](https://assetstore.unity.com/packages/tools/input-management/touch-controls-kit-12373)  
<b>Dismounted Knight</b> - [https://assetstore.unity.com/packages/3d/characters/humanoids/dismounted-knight-7263](https://assetstore.unity.com/packages/3d/characters/humanoids/dismounted-knight-7263)  
<b>A Piece of Nature</b> - [https://assetstore.unity.com/packages/3d/environments/fantasy/a-piece-of-nature-40538](https://assetstore.unity.com/packages/3d/environments/fantasy/a-piece-of-nature-40538)  
<b>Variety FX</b> - [https://assetstore.unity.com/packages/vfx/particles/variety-fx-25229](https://assetstore.unity.com/packages/vfx/particles/variety-fx-25229)  
<b>Casual Kingdom World Sounds - Free</b> - [https://assetstore.unity.com/packages/audio/music/casual-kingdom-world-sounds-free-136406](https://assetstore.unity.com/packages/audio/music/casual-kingdom-world-sounds-free-136406)  
<b>Fairy Castle Kit</b> - [https://assetstore.unity.com/packages/3d/environments/fantasy/fairy-castle-kit-82085](https://assetstore.unity.com/packages/3d/environments/fantasy/fairy-castle-kit-82085)  
<b>Mobile Optimized Post Processing (LWRP URP support)</b> - [https://assetstore.unity.com/packages/vfx/shaders/fullscreen-camera-effects/mobile-optimized-post-processing-lwrp-urp-support-152673](https://assetstore.unity.com/packages/vfx/shaders/fullscreen-camera-effects/mobile-optimized-post-processing-lwrp-urp-support-152673)  
<b>Mini Pack : Keys</b> - [https://assetstore.unity.com/packages/3d/props/mini-pack-keys-80428](https://assetstore.unity.com/packages/3d/props/mini-pack-keys-80428)  
<b>#NVJOB Water Shader</b> - [https://assetstore.unity.com/packages/vfx/shaders/nvjob-water-shader-149916](https://assetstore.unity.com/packages/vfx/shaders/nvjob-water-shader-149916)  
<b>Top-Down Caves</b> - [https://assetstore.unity.com/packages/3d/environments/dungeons/top-down-caves-39124](https://assetstore.unity.com/packages/3d/environments/dungeons/top-down-caves-39124)  
<b>Skyboxes MegaPack 1</b> - [https://assetstore.unity.com/packages/2d/textures-materials/sky/skyboxes-megapack-1-2402](https://assetstore.unity.com/packages/2d/textures-materials/sky/skyboxes-megapack-1-2402)  
<b>Mobile Ready Bloom asset</b> - [https://assetstore.unity.com/packages/2d/textures-materials/mobile-ready-bloom-asset-58230](https://assetstore.unity.com/packages/2d/textures-materials/mobile-ready-bloom-asset-58230)  
I also used free icons from <b>Icons8</b> - [https://icons8.com](https://icons8.com)  


## About me :slightly_smiling_face:
I am a Computer Science student, who loves music, nature and video games. Visit [my profile](https://connect.unity.com/u/vid-trtnik) on Unity Connect for information about my new projects and games in development.  