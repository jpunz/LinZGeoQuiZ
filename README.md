# <img src="documents/AppIcon_transparent.png" width="40"/> LinZ Geo QuiZ

This is a cross-platform application for iOS and Android made in Xamarin (Xamarin Studio / Visual Studio).
The app is a quiz about the geographical location of streets, bus stops, hospitals and nursing homes in Linz (Austria). It should help to improve the geographical knowledge about Linz for e.g. rescuers (fire brigade, police, ambulance), taxi drivers and so on and can also be used for training purposes.
The learning effect of using this app should be, to find a specific location in Linz very fast and without using a navigation system, just by having a building's name or an address. This is very important for taxi drivers and rescuers.
Because of the gamification aspect, the users are able to learn something about the locations in Linz, without studying borings maps or the like.

Quiz categories in game mode:
* Street names
* Retirement homes
* Public buildings
* Bus stops
* Mixed (questions out of all categories)

## Technology

The Android and iOS app is built by using Xamarin (C#). The IDE, which is being used is Xamarin Studio for Mac OSX or Microsoft Visual Studio for Windows.
For storing all the geographical data and user data, we are using Firebase.
The map represenatation in the app is depending on the used Device. For iOS it's using Apple Maps and for Android Google Maps. This is realized by using the NuGet-Package [Xamarin.Forms.Maps](https://www.nuget.org/packages/Xamarin.Forms.Maps).
The geographical data (streets, bus stops,...) was exported from an OSM-File of Linz to a CSV-file using [osmfilter](https://wiki.openstreetmap.org/wiki/Osmfilter) and [osmconvert](https://wiki.openstreetmap.org/wiki/Osmconvert). 
For generating an OSM-File of Linz, [osmosis](https://wiki.openstreetmap.org/wiki/Osmosis) in combination with a .poly-File, which contains the borders of Linz, was applied to an OSM-File of Upper Austria.

Brief description (perhaps bulletpoints) on the used technology stack.

## Team Setup

* Ravdeep Arora
* Marcel Breitenfellner
* Christian Obermayr
* Jürgen Punz

## Artefacts

* [Project Proposal](documents/proposal.pdf)
* [Screen Design](documents/ScreenDesigns.bmpr), [MyBalsamiq](https://www.mybalsamiq.com)
* [Peer Talk Slides](documents/Peer_Talk.pptx)
<!--* [UI Sketches](documents/ScreenDesigns.bmpr)-->
<!--* [Final Project Presentation](documents/final-presentation.pdf)-->
<!--* Code is living in `src`-->
<!--* [Project Video](https://www.youtube.com/embed/gGOXMWGVwDg) or [Project Poster](documents/poster.pdf)-->
