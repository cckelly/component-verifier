# Component Verifier

I've found that sometimes when applying changes to prefabs or just general work inside Unity can lead to unfortunate duplicate components on a single game object which can cause all sorts of problems. This simple script does a check on all game objects (roots, children, inactive/active) for duplicate components within the currently active scene and alerts you of any game objects that have components that been applied more than once. Component Verifier also gives you the option to immediately remove any duplicate components. 

Unfortunately, there is no way around looping through every object in the scene and getting all of that object's components, so depending on the size of your scene it may not be that performant.

I've added a simple GUI which can be accessed with `Window/Component Verifier`.

![alt text](http://i67.tinypic.com/b9j1xs.jpg)

# Installation

You can drop the script from Assets/Editor into your own project or simply import ComponentVerifier.unitypackage.

# TODO

* Support for multiple scene verification
