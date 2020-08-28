# Game Development Project: GundamSD
## Introduction
**GundamSD** is a simple **2D platformer**, made by Yve's De Jonghe, student of AP.
The game's style is inspired by, of course, **Mario** and **Metal Slug**.
The sprite atlasses for the characters are all from an old game,
called **SD Gundam: Operation U.C.** for a small handheld console, the **WonderSwan**.
Tilesets were found online for free, all sources are linked **below**.
The game is build in the **Monogame** framework.

## Features
### Levels
The game consists of 2 levels:
* Tutorial level
* Main level 
The levels were drawn in **Tiled**. **Tiled** is a general purpose **tile** map editor.
It functions as a free tool to allow the easy creation of map layouts.
These maps are generated into a **CSV** file, which in turn can be used in Monogame.
Monogame.extended has a built in library for this purpose, if only it worked.
Instead, I used **TiledSharp*, A .NET C# library for importing Tiled maps.
**TiledSharp** does not render maps for you, it only reads the map as it is a parser.
How I've done it is seen in the **MapManager** class. It can even render maps with **multiple tilesets**!

### Animation
Every sprite is animated with a atlas (so 1 texture). The atlas sources are listed below.
All I had to do was to format them in GIMP. (So that they are situated in a 64x64 grid)

### Mechanics
* Collision Detection
* Mario like movement
* Animated attack sequences
* Shooting
* Simple AI

## Resources/Credits
- Skins: https://www.spriters-resource.com/
  (Original credits: Mobile Suit Gundam, by Sunrise)
  - Player skin: https://www.spriters-resource.com/wonderswan_wsc/sdgundamoperationuc/sheet/23339/
  - Enemy skin: https://www.spriters-resource.com/wonderswan_wsc/sdgundamoperationuc/sheet/23316/

- Image editor: https://www.gimp.org/

- Map editor/creation: https://www.mapeditor.org/
- Tilesets for drawing map
  - Tutorial Level: https://0x72.itch.io/16x16-industrial-tileset
  - Main level: https://opengameart.org/content/bulkhead-walls-hangar
