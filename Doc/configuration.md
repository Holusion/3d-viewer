#Configuration

##Abstract

Un certain nombre d'options de configurations sont possibles, via le fichier config.json - ```Assets/Resources/config.json```
Ce dernier consiste en un tableau référencant les obejts devant être configurés.

Par exemple pour un objet **"obj1"** :
```
  [
    {
      "name":"obj1",
      objects:[
        "mySpecialLight"
      ]
    }
  ]
```
Dans l'exemple ci dessus, l'objet *mySpecialLight* sera référencé comme lié à obj1. C'est à dire qu'il sera automatiquement désactivé pour tous les autres objets, mais activé pour ce dernier.



Cela permet par exemple d'éclairer plus fortement (en activant des lumières) des objets plus sombres, sans "brûler" les plus clairs.

##Options

###objects
**Optionnel** _\<tableau>_

référence un *GameObject* de la hiérarchie comme appartenant à la "scène" du modèle. C'est à dire que l'objet référencé sera activé / désactivé en même temps que le dit modèle.

**NB:** Un *GameObject* peut être référencé par plusieurs modèles.

###axes

**Optionnel** _\<tableau(2)>_

Donne les autorisations de rotation sur les axes x et y pour l'objet.
Permet aussi de régler la vitesse de rotation.

```
"axes":[1,0]
  //Fera tourner l'objet uniquement autour d'un axe vertical
"axes":[0,1]
  //Fera tourner l'objet uniquement autour d'un axe hoizontal
"axes":[0.1,0.1]
  //Fera tourner l'objet sur les 2 axes, 10 fois plus lentement que la normale.
```
