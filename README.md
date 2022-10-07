------------------------------------------------------------------ 21/09/2022
-------------------------------- Team-Burger --------------------------------
-----------------------------------------------------------------------------

Réaliser par :
	Alexandre SANCHES MATEUS - GP
	Nicolas MANACH - GP
	Thomas RAULIN - GP




------------ Système de stat ------------
Pour les enemies -> copier l'EnemyData puis le modifier
Chaque entité du jeu a :
	attaque -> maximum que l'entité peut infliger (si une attaque a 100% de dégâts)
	défence -> maximum que l'entité peut encaisser (nouveau dégât par soustraction)
	vitesse -> valeur pour l'esquive (si sa vitesse est supérieur à l'énnemie, il a une chance d'ésquiver)


------------ Système de combat ------------
Attaque :
	Nom, Force (dégât max), Type (melee, range), Chance (taux de réussite)
	Tour commençant par le chasseur
	Cas partivulier : si l'attaque de l'énnemi est prioritaire ou si son attaque est lent.
	Après avoir choisi sont attaque -> QTE, si réussi peut infliger un coup critique (dégat de l'attaque sans
	réduction par la défense ennemie)

Si l'ennemie n'a pas remarqué le joueur :
	Il peut faire plusieur action, à chaque action il accroit ces chances de se faire remarquer :
		- Tirer
		- Utilisable : soigner, poser piège, monter ses spec, ...
	Si il se fait remarquer, l'annimal attaque le même tour, puis passe en t/t classique.

Si l'ennemie a remarqué le joueur :
	t/t classique
	chaque tour le joueur peut attaquer ou utiliser un object dans son sac.


------------ Game Feel ------------
	// Freeze frame lorsque l'animal est toucher
	Slomo lorsque la balle est tirer - plus de slomo en même teps que l'ouverture du combat

	particule (vfx graphe) -> courrir, nager, taper un arbre, dégât, saignement (non réaliste : préféré une anipation),
	environement (neige)
	sound (F-mode) -> dispertion du son/échos lors du tire, bruit de pas (différent sol), environnement

	Animations sur tout les perso et entité/décor (trop de travail) (en combat et aventure), (saignement) lorsque le combat commence -> Tweening

	Tweening -> caméra, début combat
	Haptic ffedback -> Tir avec son arme, dégat, prendre un arbre lorsque l'on cours (uniquement/ lor d'une action longue)
	Cam Shake -> battle phase (+ de dégât, + de shake), tir, prendre un arbre lorsque l'on cours

	Deep of field -> lors de la visé, passage en mode combat
	Lens distortion -> lors de la visé

	flash -> les entité prennent des dégâts
	Dynamic environnement -> tir sur des caisse, arbre, ...

	Controlle et UI

