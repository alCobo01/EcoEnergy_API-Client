# T1. PR2. API REST - Álvaro Cobo

## Descripció del Projecte

Aquest projecte consisteix en una aplicació web completa desenvolupada amb tecnologies .NET, que inclou una API REST i un client que consumeix aquesta API. El projecte està dissenyat per gestionar un catàleg de videojocs, permetre als usuaris registrar-se, iniciar sessió, valorar els jocs i comunicar-se mitjançant un xat.

## Estructura del Projecte

### Projecte API
![apiDiagram](img/apiDiagram.png)

L'API REST està construïda amb ASP.NET Core i proporciona diversos endpoints per gestionar:
- **Autenticació d'usuaris**: Registre, inici de sessió i gestió de rols (usuari normal i administrador)
- **Gestió de videojocs**: Llistar, obtenir detalls, afegir, modificar i eliminar jocs
- **Sistema de votació**: Permet als usuaris registrats votar els seus jocs preferits

La API utilitza:
- Entity Framework Core per a la persistència de dades
- JWT (JSON Web Tokens) per a l'autenticació i autorització d'usuaris
- SignalR per a la comunicació en temps real

### Projecte Client
![clientDiagram](img/clientDiagram.png)

El client està desenvolupat amb ASP.NET Core utilitzant el patró Razor Pages i ofereix una interfície d'usuari per:
- Registrar-se i iniciar sessió
- Visualitzar el catàleg de jocs
- Veure detalls de cada joc
- Votar els jocs (si l'usuari està autenticat)
- Xatejar amb altres usuaris en temps real
- Gestionar els jocs (crear, modificar, eliminar) si l'usuari té rol d'administrador

## Tecnologies Utilitzades

- **Backend**: ASP.NET Core, Entity Framework Core, SignalR
- **Frontend**: Razor Pages, Bootstrap
- **Seguretat**: JWT, Identity Framework
- **Base de dades**: SQL Server

Aquest projecte demostra la implementació d'una arquitectura client-servidor completa amb autenticació, autorització i comunicació en temps real.

