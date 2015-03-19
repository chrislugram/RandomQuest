#RandomQuest

## General
RandomQuest is a demo project created by Christian Lugo. It's made with Unity3D 5.0. The project is configured (and tested) to run in Windows 8.

## What is in this project

In this project you can found different tools for general game development. I use typical RPG-games scenes to test the tools. Next, a list of most interesting tools included in the project:

  - StateApp: It's a simple solution to control the screens flow of the application. This solutions was used in few development of mobile apps.
  - OrbitCamera: Specific solution to move the camera in a RPG-game with 3D graphics. 
    - With WASD can to move the pivot which look at the camera.
    - With right-click can to move the Character.
    - With left button can orbit around Character.
  - Combat RPG: Specific solution to control the turns in a RPG-game.
  - DQSSystem: General small solution to create dynamic dialogs and situations in a game. The original idea is a mixed between a [Elan Rusking in GDC 2012 tal](http://www.gamasutra.com/view/news/198377/Video_Valves_system_for_creating_AIdriven_dynamic_dialog.php) and a [Ken Levine in GDC 2014 talk] (https://www.youtube.com/watch?v=58FWUkA8y2Q). This system creates "Requests" composed by information about who creates the request, character info, memory info and world info, and evaluate it with differents "Rules". Each "Rule" has a list of generic "Criterions" and one "Response". The response can be a dialog, actions, other "Request", etc. Also include a Influence System. The NPCs have influence by Character actions and create different "Request" depending it. Each NPC has a list of  different kind of "Influence" (Kill, Attack, Steal) and a value for or against of it.
