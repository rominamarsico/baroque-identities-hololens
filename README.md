# Baroque Identities - a HoloLens project

This is a university project at Hochschule Darmstadt, study Leadership in the Creative Industries (MA) <br> <br>

### Project Setup
The project waits for input of the realtime firebase database, which you can influence here: https://baroque-identities.firebaseapp.com/ <br>
The project link to the website is here: https://github.com/rominamarsico/baroque-identities-webapp <br>
To use the cloud function to add a message to the realtime database, use this link: https://us-central1-baroque-identities.cloudfunctions.net/addMessage?text=

### Project Team:
Programmer: Isabel Gaubatz, Romina Marsico <br>
3D artists: Mona Moghimi, Inga Reichert <br>
Corporate designer: Katharina Devecioglu <br>
Video producer: Marcel Moosmann

## Settings for Unity usage
Unity 3D version: 2018.1.0f2 (http request currently not working in newer Unity versions) <br>
Enable the following scripts in Unity: <br>
- MenuController > GazeMouseManager

## Settings for HoloLens usage
Enable the following scripts in Unity: <br>
- MenuController > GazeGestureManager <br> <br>

Settings in Visual Studio: <br>
- Release <br>
- x86 <br>
- Device

## Tipps for video capturing
- in Visual Studio, start without debugging <br>
- connect with the HoloLens via USB and use http://127.0.0.1:10080 <br>
- in Device Portal, Views > Mixed Reality Capture: use "Low" for the live preview quality

