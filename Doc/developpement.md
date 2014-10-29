#Développement

##Fonctionnement

On peut trouver l'ensemble des scripts dans ```Assets/scripts```.
L'application fonctionne grace à un unique script main() qui est instancié sur un objet **master**.

Elle prend comme référence de taille l'objet par défaut **Sphere**, qui sera affiché si aucune alternative n'est proposée (voir [usage](../)).

La scène par défaut contient aussi :

- Des éclairages par défaut
- 4 caméras

L'utilisateur est libre d'ajouter ou d'éditer tout objet qu'il jugerait utile. Il sera conservé au moment du rendu.
Il est déconseillé de modifier la configuration des cameras, au risque d'empêcher l'affichage sur l'hologramme.

##Scripts

A partir du script **main**, l'application récupère et instancie tous les objets présents dans le dossier ```Resources/Objects``` et en forme une [liste](../Assets/scripts/Classes/Models.cs) de [modèles](../Assets/scripts/Classes/Model.cs)

Chaque modèle peut être controlé via les methodes suivantes :

```
  void setRotation(Vector3 axis);
  void setRotation(float x,float y,float z);
```

La liste des modèles expose de plus certaines fonctions utiles :

```
  void next();
  int getCurrent();
  void setCurrent(int index);
```

Les axes x et y correspondant au plan de l'écran, respectivement horizontalement et verticalement.

3 Contrôleurs sont proposés par défaut (```Classes/parsers```):

```
  LeapParser
  MouseParser
  KeyParser
```

Ils sont appellés séquentiellement, donc la souris sera ignorée si un leap motion est détecté.

Ces parsers proposent une fonction ```bool update()``` dont la valeur retour indique si le parser suivant doit être testé.
Ils sont [appellés](../Assets/scripts/main.cs#L25) à chaque frame par le script principal

Ils modifient le modèle de façon autonome en fonction de leurs entrées.
