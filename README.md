# Documentation technique

## Principaux objets du projet
### Scène
La scène principale de l’application se trouve dans `Assets/Scenes/VRScene.unity`.
Elle contient les objets suivants : 
- `XR Origin` qui représente le joueur.
- `XR Interaction` qui sert à l’interaction XR.
- `Map` qui représente la carte générée par Mapbox.
- `Direction` qui permet la construction du chemin de l’itinéraire à suivre sur le sol.
- `APIManager` qui va permettre de faire les requêtes API auprès de Wikipédia pour la recherche d’information sur les points d’intérêts, de Microsoft Text-to-Speech pour l’audio guide, et de spawner des indicateurs de points d’intérêt aux positions des résultats de la requête Wikipédia (Utilisés pour la minimap).
- `EventSystem` qui sert comme manager de l’interface utilisateur et à charger l’itinéraire à suivre.

### Prefabs

####  Player
Il existe deux objets qui peuvent représenter le jouer : 
- `Assets/Prefabs/Player/KeyboardPlayer/KeyboardPlayer.prefab` qui est un objet joueur (sans interface) qui peut se contrôler au clavier uniquement.
- `Assets/Prefabs/Player/VRPlayer/XR Origin.prefab` qui est un objet joueur qui peut se contrôler grâce aux contrôles VR ou être déplacé grâce aux flèches directionnelles du clavier.  Cet objet est composé de `Main Camera` qui représente le casque VR, `LeftHand Controller` et `RightHand Controller` qui représentent les manettes.  `LeftHand Controller` a comme objet enfant `Interface` qui est le GUI utilisateur pour la sélection des articles sur les points d'intérêts et l’affichage des informations de ceux-ci.  `RightHand Controller`  a comme objet enfant l’interface `Minimap`.

#### Minimap
Objets d’interface pour la minimap:
- `Assets/Prefabs/Minimap/IrregularCircleUI` élément esthétique circulaire pour les contours de la minimap.
- `Assets/Prefabs/Minimap/MinimapMaterials/MinimapPlayerIcon.prefab`  et `Assets/Prefabs/Minimap/MinimapMaterials/MinimapPOIIcon.prefab` respectivement les icônes qui représentent le joueur et les points d'intérêt (spawné par le script `POIIndicator` de l’objet `APIManager`).

#### Map
- `Assets/Prefabs/Map/Map.prefab` carte générée par Mapbox.
- `Assets/Prefabs/Map/NameGetter.prefab` objets spawné sur les points d'intérêts mapbox afin de récupérer leur nom et leur coordonnées géographiques (à mettre dans `MAP LAYERS/ POINTS OF INTERESTS/New Location Properties/Prefab` dans les options du script `Abstract Map` de l’objet `Map.prefab`).
- `Assets/Prefabs/Map/Waypoint.prefab` Indicateur de point de passage pour le tracé de l’itinéraire  (spawné par le script ‘SpawnOnMap’ de l’objet `Direction`).
- `Assets/Prefabs/Map/POITracker.prefab` marqueur de point d'intérêt (spawné par le script `POIIndicator` de l’objet `APIManager`) .

#### Interface
Éléments pour l’interface de sélection des articles sur les points d’intérêts.
- `Assets/Prefabs/Interface/AudioGuideButton.prefab` Bouton pour l’audioguide.
- `Assets/Prefabs/Interface/BackButton.prefab` Bouton pour revenir au canvas de la sélection des articles sur les points d'intérêts. 
- `Assets/Prefabs/Interface/Recyclable Scroll View.prefab` Liste scrollable de boutons avec recyclage, utilise les boutons  - `Assets/Prefabs/Interface/ScrollerButton.prefab`.

#### Demo
Modèles 3D de la tour Eiffel et de l’arc de triomphe pour la présentation.

## Scripts

### API
---
#### GeoGetter
Script présent sur chaque objet `NameGetter` qui est spawné à l’emplacement des points d'intérêts mapbox, permettant de récupérer le nom et les coordonnées de ces point d'intérêt et de lancer une recherche avec le script `WikipediaAPI` quand le joueur est proche de ceux-ci.

#### MicrosoftTTS
Script pour gérer les requêtes de text-to-speech auprès de l’API de Microsoft.

#### WikipediaAPI
Script pour gérer les recherches sur les points d'intérêts auprès de l’API de Wikipédia.

#### WikipediaPageData
Déclaration des classes `Data` (qui correspond à une recherche) et `pages`(qui correspond à un article Wikipédia).

### Direction
---
#### SpawOnMap
Placement des points de passages de l’itinéraire (objets `Waypoint.prefab`).

#### DirectionsFactory
Requêtes auprès de l’API Direction de Mapbox pour faire le tracé du chemin à partir des points de passages. Création du mesh du chemin. 

#### POIindicator
Placement des icônes `MinimapPOIIcon.prefab` pour la minimap et des marqueurs des points d'intérêts `POITracker.prefab ` (ici ils représentent les articles Wikipédia trouvé et non les points d’intérêts de Mapbox)

#### POITracker
Effet billboard pour les marqueurs des points d'intérêts `POITracker.prefab`

#### WaypointBehavior
Animation pour les points de passage de l’itinéraire (objets `Waypoint.prefab`).

### Interface
---
#### Info_Interface
Permet de récupérer toutes les recherches (Data et pages) faites par `WikipédiaAPI` et d’appeler `POIindicator` et de déclencher les impulsions haptiques lors de l’arrivée de celles-ci.
Permet la configuration des boutons de la liste scrollable d’article (affectation de la page et du titre des articles aux boutons).
#### InfoCanvas
Permet l’affichage de l’article Wikipédia sur l’interface.

#### InterfaceManager
Manager de l’interface. Permet d’afficher et de cacher la minimap ou l’interface selon les inputs des manettes VR. Permet la gestion des impulsions haptiques des manettes.

### Itinirary
---
#### Itinirary
Déclaration de la classe `Itinirary` qui définie un itinéraire avec un endroit de départ (ou le joueur à apparaitre au lancement de l’application) et une liste de points de passage.

#### ItiniraryProvider
Permet la lecture d’un itinéraire `Itinirary` sous le format json dans le fichier `AppData\LocalLow\MAL\Marche_a_l'ombre\ItinirarynData.json`. Si ce fichier n’existe pas, le script crée ce fichier avec un itinéraire par défaut.

### Minimap
---
#### MinimapPlayerIcon
Permet de gérer l’orientation de l’icône du joueur `MinimapPlayerIcon.prefab` en fonction de l’orientation de celui-ci
#### MinimapPOIAllwaysFAcingPlayer.
Permet de gérer l’orientation des icônes de point d’intérêt `MinimapPOIIcon.prefab` afin qu’elle soit toujours verticale par rapport à la minimap.
