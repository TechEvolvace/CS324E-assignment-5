PROJECT: CS 324E Assignment 5 - 3D Animation

--- HOW TO RUN THE ANIMATION ---

This project is a standard MonoGame (DesktopGL) application built using the .NET CLI.

Navigate to the Project Directory:
Open your terminal/command prompt and navigate to the root directory of the project folder:

cd /path/to/group_12_assignment5

Restore Dependencies:
Ensure all necessary packages are installed:

dotnet restore

Build the Project and Content:
Run the build command to compile the C# code and process all 3D content (meshes/textures) via the MonoGame Content Pipeline:

dotnet build

Run the Application:
Execute the compiled game:

dotnet run

--- EXPECTED ANIMATION ---

The scene features a continuous, large-scale 3D animation:

Klang Gear System: A complex gear system (Big Gear and Small Gear) rotates and translates in the background.

Whirlipede: Rolls back and forth across a fixed path on the ground using smooth, non-linear acceleration.

Hoppip: Floats across the scene with a bobbing motion, its head rotating independently.