# Work in progress! #

## Interface, usability, etc. ##

By default, only show options for "simple" interface; allow switching to "advanced" interface at any time from menu.

Both interfaces should use the "XML actor definition" idea from Sorata for certain user-selectable things like obviously actors, as well as ex. BGMs, anything like that.

Have a "new project" window similar to Visual Studio's which contains defaults like ex. "single outdoor map" (like Hyrule Field) or "dungeon with multiple rooms" (like, well, most/all dungeons) or simply "empty project". Selecting any of those will make the program fill in certain default values by itself, like environment setups (lighting, fog). All settings should still be user-changeable after project creation, depending on the complexity of them only in "advanced" interface mode.

An in-program tutorial is probably overkill, but a well done HTML-based tutorial should be included. Corresponding HTML pages should be launchable from inside the program, ex. pressing "Help" inside the actor editor form/panel/whatever would launch the user's default browser and show the page that describes the process of adding and editing actors.

Having a fully accurate preview is probably not possible. Lighting in particular is difficult to simulate correctly. Accurate simulation of texture mapping, the color combiner and fog should basically be doable, but are a bit of work.

Mouse-based movement of ex. actors, similar to Sayaka or UoT, should also be implemented.

Editable in all modes are _at least_ actors, objects, waterboxes, spawn points, transition actors, waypoints, special objects (_field_ or _dangeon_)... likely a few more.

### Options in "simple" mode ###

  * Timeflow (off/on)
  * Skybox (off/on & type)
  * BGM
  * ...

### Options in "advanced" mode ###

  * Timeflow speed
  * Sound reverb/echo
  * Camera settings
  * ...

## File formats, etc. ##

More file types besides Wavefront .obj's should be available for importing later, although focus should be on .obj for now. Model loaders should be implemented in C# interface form, so that additional loaders can be written and added to the program with relative ease. Every type of model is parsed into a series of common classes (ex. Material, Group, Polygon, Vertex, etc) internally during load, which is what conversion to display lists etc. will be based on.

Model2N64's model library Bakemono will not be used, although concepts and some code will be lifted from it.

Importing existing scenes/rooms to re-edit them will probably not be possible. Certain properties of existing scenes could be imported into a project, like actors, but having the program parse scenes, extract all their data, interpret and "pseudo-dump" display lists into the common classes, etc. is rather unlikely to happen.

## Game-specific properties, etc. ##

...