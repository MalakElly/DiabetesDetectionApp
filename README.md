Diabetes Detection App — Architecture Microservices (.NET 8)

 Projet 10 – OpenClassrooms
 Réalisé par Malak El Oualy, Développeuse .NET chez IB Cegos
Stack : .NET 8 · ASP.NET Core · Razor Pages · Ocelot · ML.NET · SQL Server · Docker · xUnit,MongoDB

=> Objectif du projet

L’objectif de ce projet est de concevoir une application distribuée capable de :

Gérer les patients et leurs notes médicales

Authentifier les utilisateurs via un service d’identité

Calculer un risque de diabète selon des règles métier

Prédire le diabète grâce à un modèle ML.NET

Fournir une interface web simple (Razor Pages) consommant tous les services via une API Gateway

Le tout conteneurisé avec Docker

=> Architecture globale
-Services principaux
FrontEnd	Interface utilisateur (Razor Pages) connectée à la Gateway	
API Gateway	Point d’entrée unique de toutes les requêtes	
PatientService.API	Gestion CRUD des patients	
PatientService.Core	Couche métier : entités, DTO, interfaces, règles	
PatientService.Infrastructure	Couche d’accès aux données 
NotesService	Gestion des notes médicales liées aux patients avec MongoDB	
RiskService	Calcul de risque à partir de règles métier	
AuthService	Authentification JWT / rôles utilisateurs	
DiabetesPredictionService	Prédiction ML.NET du diabète	
SQL Server	Base de données principale	

=> Workflow d’appel typique

1.L’utilisateur s’authentifie (AuthService) → obtient un JWT

2.Le FrontEnd appelle /patients via Gateway

3.Récupération des notes du patient (NotesService)

4.Calcul du risque (RiskService)

5.Prédiction finale (DiabetesPredictionService)

6.Affichage du résultat sur l’interface

🌱 Green Code — Écoconception logicielle

Le projet a été développé dans une logique de Green Code,  de sobriété numérique et de réduction de l’empreinte environnementale du logiciel.

=> Objectif	et application concrète:
Réduction des traitements inutiles :	Utilisation du AsNoTracking() dans EF Core pour les lectures simples.
Minimisation des échanges réseau	: Les appels FrontEnd → Gateway → Services sont ciblés et optimisés.
Optimisation mémoire:	Utilisation de IEnumerable et suppression des objets non utilisés (dispose pattern).
Dockerisation efficiente:	Images légères .NET 8 (multi-stage builds avec dotnet publish --no-restore).
Logs responsables	: Logging minimal (niveau Warning en production).
Green UI	: FrontEnd Razor Pages sans framework lourd (pas d’Angular/React inutile).
Mutualisation des ressources:	Un conteneur SQL partagé par plusieurs microservices, évitant les duplications.
Scalabilité raisonnée	: Possibilité de déployer seulement les services nécessaires selon la charge.
Documentation intégrée	: Fichiers .md et .json (Swagger, ocelot.json) → pas de PDF lourds par défaut.
