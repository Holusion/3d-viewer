#Viewer 3D Holusion

#Fonctionnement

Cette application permet la manipulation d'objets 3D en utilisant un [Leap Motion](https://developer.leapmotion.com) / une souris / un clavier.

Accéder au détail des [contrôles](#contrôles)

#Usage

###Mise en place

téléchargez le projet sous forme [d'archive](https://github.com/Holusion/3d-viewer/archive/master.zip) ou via un client [git](https://windows.github.com/)

Ouvrez le avec Unity3D (**FREE** ou **PRO**, testé à partir de 4.5.1f3).

Faites un essai ```ctrl+P```. Par défaut, une modélisation de la terre devrait apparaitre et être [contrôlable](#contrôles).
Dès que vous aurez importé d'autres objets, celui-ci sera retiré automatiquement.

###Import

Le contenu du dossier ```Resources/Objects```constitue une "playlist" d'affichage. Ce dossier est vide par défaut.

Importez-y un objet de format compatible avec Unity3D.

Assurez-vous de la bonne configuration des textures, bump map, etc...

Référez vous à la [documentation](http://docs.unity3d.com/Manual/HOWTO-importObject.html) du moteur pour plus de détails à ce sujet.

###Configuration

Voir [documentation](Doc/configuration.md) associée

Etape optionnelle, permet de mieux adapter l'application à chaque modèle.

Les options par défaut conviendront généralement pour une première prise en main.

###Test

Lancez de nouveau l'application avec ```ctrl+P```, vous pouvez vérifier le bon affichage de vos objets.

###Export

Lancez un 'build' avec ```ctrl+maj+b```

Choissez l'option ```Linux```et l'architecture ```x86_64```

Cliquez sur **build**

Enregistrez sous le nom souhaité

Sélectionnez le fichier et le dossier ainsi créés et intégrez les à une archive au format *.zip* ou *.tar*.

#Contrôles

**TODO : ** explication de tous les contrôles possibles.


##TODO

 * ne pas planter si leap motion non installé

 
