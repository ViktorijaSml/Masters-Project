<div align='center'>

<h1>UIFlow Virtual</h1>
<h3>Blockily coding simulator for M5stickC Plus microcontroller in Unity, C#</h3>

:blush: Welcome to UIFLow Virtual! This repository contains a coding environment featuring a simulator for the M5StickC Plus microcontroller. 
This project is a part of the EU-funded <a href="https://roboaquaria-project.eu">RoboAquaria</a> initiative, aimed at raising awareness about marine environment protection, as well as promoting STEM education.

<br>
<h4>TRY THE PREVIEW :arrow_right:<a href="https://viktorijasml.github.io/UIFlow-Virtual-Preview/"> **HERE** </a>:arrow_left:</h4>
<br>
</div>

# Table of Contents

- [About the Project](#about-the-project)
  - [Platform Compatibility](#platform-compatibility)
  - [Features](#features)
  - [Prerequisites](#prerequisites)
  - [Run Locally](#run-locally)
- [Tutorials](#tutorials)
  - [Adding New Categories and Blocks](#adding-new-categories-and-blocks)
  - [Adding New Units](#adding-new-units)
  - [Using the App](#using-the-app)
- [License](#license)
- [Acknowledgements](#acknowledgements)
- [Contributors](#contributors)


# About the Project
This project originated as part of my master's thesis, with the primary goal of making learning and teaching coding more accessible and effective.
By using *UBlockly*, a reimplementation of *Google's Blockly* library in Unity, we've created a platform that mimics the *UIFlow IDE*, but also includes an integrated simulator. 
This allows students to test their code at home without needing physical hardware, and enables teachers to focus on more advanced tasks with the actual hardware in the classroom. 
Additionally, it ensures anyone can practice and improve their coding skills at home, regardless of access to physical devices.

![image](https://github.com/ViktorijaSml/UIFlow-Virtual-/assets/73490593/2c4e7e6a-9e4e-4a56-9e93-1ffea5008b8b)

## Platform Compatibility
UIFlow Virtual is compatible with **Android** and **PC platforms (Windows, Mac and Linux)**. While it is primarily designed for these platforms, 
it may also work on **iOS** devices if extracted appropriately. For PC platforms, successful testing has only been conducted on Windows.

## Features
This project enchances UBlockly with fixes and new addons:

### New Categories and Blocks
  - LED
  - Screen
  - Volume
  - Real Time Clock
  - Watch Dog Timer
  - Events
  - System
  - Label
  - Angle
  - Dual-Button
  - EnvIV
  - Joystick

### M5StickC Plus Simulator
  - Buttons A & B
  - Screen
  - LED
  - Labels
  - Units:
    - Joystick
    - Angle
    - Dual-Button
    - EnvIV

### Fixes and Improvements
- **Runner:** Sections of blocks are executed asynchronously.
- **Loops:** Infinite loops can run without crashing the app.
- **Logic blocks:** `If` and `If-else` blocks are placed in the `Logic` category
- **Clone blocks:** Clone the existing block in the workspace by double-clicking on it.

### File Management Features
- Rename File
- Delete File
- Open New File
  
### Additional Features
- **Units System:** Add new units easily. There are empty prefabs of other units that exist for the microcontroller.
- **Showing Necessary Categories:** The `IShowable` interface allows categories to be hidden if the element representing the categories isn't active in the simulator.
- **Refresh Button:** Allows the user to restart the app if an error occurs.
- **Save file:** Updated saving system to include the new features.
- **Simulation Mode:** During execution of your code, the ability to add new units and labels is disabled. If an unit is present, a new window will appear for interacting with it.

## Prerequisites

- Install Unity Editor `2021.3.22f1` <a href="https://unity.com/releases/editor/whats-new/2021.3.22">Here</a>


## Run Locally

- Clone the project

```bash
git clone https://github.com//ViktorijaSml/UIFlow-Virtual.git
```
- Or download `.zip` and open with Unity hub
```bash
https://github.com/ViktorijaSml/UIFlow-Virtual/archive/refs/heads/main.zip
```
- Find a scene under the name *"UIFlow Virtual"* and open it in Unity Editor.

# Tutorials

## Adding New Categories and Blocks:
Learn how to extend this project (or the UBlockly library itself) by adding new categories and blocks. 
Find a detailed blog on how UBlockly works, as well as an amaizing guide to help you add new blocks: <a href="https://github.com/imagicbell/ublockly">imagicbell's Ublockly</a>

## Adding New Units:
**1. Examine Available Units**
  - Start by reviewing all available units, whether they are already implemented or not. These units are listed within the hierarchy of `Canvas–Units/Units/Items/ScrollView/ViewPort/Content`.
  
    ![image](https://github.com/ViktorijaSml/UIFlow-Virtual-/assets/73490593/5cfc1749-cd3f-44c7-ba8b-21434900e6ef)
  - The units are essentially prefabs located in the `Assets/Resources/Units Images` folder.
  - Active prefabs are displayed in a lighter color, while inactive ones are present but not currently in use.

**2. Identify the Unit for Implementation**
  - Identify the specific unit you want to implement.
  - Activate it by checking its box in the `Inspector` window.
    
    ![image](https://github.com/ViktorijaSml/UIFlow-Virtual-/assets/73490593/616546a5-5655-43bd-acbb-77894eb93f4b)
  - When the project is launched and the unit is selected using the *“Unit”* button, a child object with the same name will be instantiated in two locations:
    - Inside `Canvas-Buttons&Screen/Buttons/Right/UnitSlot`: an object representing an image is instantiated from the `Units Images` folder.
    - Inside `Canvas-Units/UnitSimulation`: the object simulating that unit is instantiated from the `Units Objects` folder.
  - :heavy_exclamation_mark: Make sure that objects of the specific unit within both folders have identical names for the system to function correctly.   

**3. Editing the Unit Prefab**
  - Locate the selected unit prefab within the `Units Objects` folder.
  - Double-click on the prefab to open the editing window.
  - :heavy_exclamation_mark: Unimplemented units typically have only one object containing their name. This object serves as the root object of the hierarchy implementing the unit’s simulation and it should not be removed.

    ![image](https://github.com/ViktorijaSml/UIFlow-Virtual-/assets/73490593/ca5c86be-d5ef-4898-b2a3-ef29ada06cd9)

**4. Implementing the “[Unit]Manager” Class**
  - The root object should contain a class named `[Unit]Manager.`
  - This class should inherit from the `IShowable` interface, so that the category appears when the unit is selected.
  - The `[Unit]Manager` class enables the functionality of detecting block commands related to the unit only when the user has selected it.
  - If no additional methods are needed, you can use the `DummyManager` class to fulfill the interface requirement.
  - :grey_exclamation: Tip: if your desired unit has more features to implement, create its `[Unit]Behaviour` class and implement them there. 

**5. Adding Block Commands**
  - After creating the unit’s simulation, add its block commands.
  - Once the project is running, users should be able to use the unit’s block commands when selected.
  - Test them using the simulator.


## Using the App:
- This tutorial provides a comprehensive overview of the app's functionality and how to make the most out of it for your programming needs: 
[User manual - UIFlow Virtual app.pdf](https://github.com/ViktorijaSml/UIFlow-Virtual-/files/15444133/User.manual.-.UIFlow.Virtual.app.pdf)
- Check out this playlist for examples demonstrating how to use the app: <a href="https://www.youtube.com/watch?v=sjC4ORt9zM4&list=PLuA2EhZhnlfQJ-oeH0-hpjOfi1CC_V1jE">UIFlow Virtual Basics Playlist.</a>

# License

Distributed under the Apache 2.0 License. See LICENSE.txt for more information.

# Acknowledgements
Thank you for making this possible:
- [UBlockly by imagicbell](https://github.com/imagicbell/ublockly) @imagicbell
  
# Contributors

Big thanks to these two guys for helping out a ton in this project:
  - <a href = "https://github.com/Antonio-Gorisek">Antonio Gorisek</a> @Antonio-Gorisek
  - <a href = "https://github.com/JosipMajer">Josip Majer</a> @JosipMajer
