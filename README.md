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