# AutoCAD Layer Removal DialogBox (C#.NET)
This repository contains a C#.NET sample application that generates DLL designed to integrate with AutoCAD. It demonstrates how to create a DialogBox (popup) with a text input field where users can enter layer names separated by commas. 

When the action button is pressed, the DLL will:

1. Parse the entered layer names.

2. Select the corresponding layers in the active AutoCAD drawing.

3. Remove those layers from the selected area.

⚠️ Note: This DLL is not a standalone application. It must be loaded into AutoCAD using the NETLOAD command.



Prerequisites

- AutoCAD (2019 or later recommended)
- Visual Studio 2019+
- .NET Framework 4.7.2+ or .NET 6 (Windows Desktop)
- AutoCAD .NET API references (acdbmgd.dll, acmgd.dll)
