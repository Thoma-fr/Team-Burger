-------------------------------------------------------------------------------------------------------------
      _______   _____      _      _     _              _____    _    _   _____   _____   _____   _____
     |__   __| | ____|    /_\    | \   / |            |  __ |  | |  | | |  _  | |  ___| | ____| |  _  |
        | |    | __|     //_\\   |  \_/  |   ______   |     |  | |  | | |  ___| | |  _  | __|   |  ___|
        | |    | |__    / ___ \  | |\_/| |  |______|  |  __  | | |__| | |  _ \  | |_| | | |__   |  _ \
        |_|    |_____| /_/   \_\ |_|   |_|            |______| |______| |_| \_\ |_____| |_____| |_| \_\

-------------------------------------------------------------------------------------------------------------

     Réaliser par® :   Alexandre SANCHES MATEUS - GP	Nicolas MANACH - GP	Thomas RAULIN - GP




             _____      _      _     _   _____         ____   _         _    __   __
            |  ___|    /_\    | \   / | | ____|       | __ | | |       /_\   \ \_/ /
   ______   | |  _    //_\\   |  \_/  | | __|         |  __| | |      //_\\   \   /     ______
  |______|  | |_| |  / ___ \  | |\_/| | | |__         | |    | |__   / ___ \   | |     |______|
            |_____| /_/   \_\ |_|   |_| |_____|       |_|    |____| /_/   \_\  |_|


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
	chaque tour le joueur peut attaquer ou utiliser un object dans son sac. G




             _____      _      _     _   _____         _____   _____   _____   _
            |  ___|    /_\    | \   / | | ____|       |  ___| | ____| | ____| | |
   ______   | |  _    //_\\   |  \_/  | | __|         |  _|   | __|   | __|   | |       ______
  |______|  | |_| |  / ___ \  | |\_/| | | |__         | |     | |__   | |__   | |__    |______|
            |_____| /_/   \_\ |_|   |_| |_____|       |_|     |_____| |_____| |____|


GAME PLAY :
	Slomo lorsque la balle est tirer - plus de slomo en même teps que l'ouverture du combat

	particule (vfx graphe) -> courrir, nager, taper un arbre, dégât, saignement (non réaliste : préféré une anipation)
	sound (F-mode) -> dispertion du son/échos lors du tire, bruit de pas (différent sol), environnement/embiance (plage, foret, oiseau)

	Animations sur tout les perso et entité/décor (trop de travail) (en combat et aventure), (saignement) lorsque le combat commence -> Tweening
	Cam Shake -> tir, prendre un arbre lorsque l'on cours

	Tweening -> caméra, début combat, (shop) déplacement de l'arme jusqu'a la poche du joueur
	Haptic feedback -> Tir avec son arme, dégat, prendre un arbre lorsque l'on cours (uniquement/ lor d'une action longue)

	flash -> les entité prennent des dégâts
	Intéractive environnement -> tir sur des caisse, arbre, ...

ENVIRONEMENT :
	Saison -> Neige, 
	Particule -> Feuille qui tombe, Vague sur l'eau, Particule du coup de feu, Mouvement du vent
	Animation (Tweening) -> Ondulation des végétaux causé par le vent, nuage
	Cycle jour/nuit

UI :
	Tweening sur le déroulement des options + sac
	sound -> ouverture/fermeture + sons différent dans chaque partie de l'UI
	Cam Shake -> battle phase (+ de dégât, + de shake)
	post processing -> ex : vignetage lorque la vie est basse, sang si il est blaisser

	Deep of field -> lors de la visé, passage en mode combat
	Lens distortion -> lors de la visé

CONTROLE :
	Pas d'accélération, ni smooth 
	Tweening sur la cam lorsque le joueur vise + déplacement
	   => Camera -> cinemachine


Toggle pour visée
utiliser le meme viseur que celui au sol
Slomo est mieux en fonction d'une référence
Cinemachine impulse
savoir où l'on tire

plus d'effet sur la balle (trail, FOV, Vignettage, rotation)
