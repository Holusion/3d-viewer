#Configuration

##Abstract

Un certain nombre d'options de configurations sont possibles, via le fichier config.json - ```Assets/Resources/config.json```
Ce dernier consiste en un tableau référencant les objets devant être configurés ainsi qu'une liste d'options globales.

Format par défaut :
```
{
  "options":{

  },
  "nodes":[

  ]
}
```

Par exemple pour un objet **"obj1"** :
```
{
  "options":{

  },
  "nodes":[
    {
      "name":"obj1",
      objects:[
        "mySpecialLight"
      ]
    }
  ]
}
```
Dans l'exemple ci dessus, l'objet *mySpecialLight* sera référencé comme lié à obj1. C'est à dire qu'il sera automatiquement désactivé pour tous les autres objets, mais activé pour ce dernier.



Cela permet par exemple d'éclairer plus fortement (en activant des lumières) des objets plus sombres, sans "brûler" les plus clairs.

##Global Options

###autoRotation

**Optionnel** _\<int>_ Defaut : 0

rotation automatique après *x* secondes d'inactivité. si inferieur ou égal à 0, désactive l'option.



###exitAfter

**Optionnel** _\<int>_ Defaut : 0

Quitte l'application après *x* secondes d'inactivité.

**TIP** : L'option ne fonctionne pas en mode débug dans l'éditeur.

##Node Options

###objects
**Optionnel** _\<tableau>_

référence un *GameObject* de la hiérarchie comme appartenant à la "scène" du modèle. C'est à dire que l'objet référencé sera activé / désactivé en même temps que le dit modèle.

**NB:** Un *GameObject* peut être référencé par plusieurs modèles.

###axes

**Optionnel** _\<tableau(3)>_

Donne les autorisations de rotation sur les axes x, y et z pour l'objet.
Permet aussi de régler la vitesse de rotation.

```
"axes":[1,0,0]
  //Fera tourner l'objet uniquement autour d'un axe vertical
"axes":[0,1,0]
  //Fera tourner l'objet uniquement autour d'un axe hoizontal
"axes":[0.1,0.1,0,1]
  //Fera tourner l'objet sur les 3 axes, 10 fois plus lentement que la normale.
```
