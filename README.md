Készítők: Bodolai Dalma (APNATZ), Kovács Bence (KDFH53), Reicher Lilla Réka (NIXHT4), 9-es csoport.

Visual Studio 2022, 17.9.5 verzióját használtuk, azon belül a "React and ASP.NET Core" project templatet.
Szükséges telepítendő packagek a "ReactApp1\reactapp1.client" mappába a futtatáshoz:
-npm install react-bootstrap bootstrap
-npm install react-router-dom

Minta bejelentkezési adatok: 
-Felhasz.név: John 
-Jelszó: 123

Az adatbáziskezelés beépített packagekkel működik, a Microsoft ASP.NET SQL-es bővítményével, ami automatikus migrációkkal tölti fel adatokkal a lokális adatbázist.

Az authorizáció/authentikáció feladatkört JWT token generálással oldottuk meg, amely a login használatával jön létre, és tartalmazza az adott felhasználó "role" típusát.  
Szükséges telepítendő packagek a "ReactApp1\reactapp1.client" mappába a futtatáshoz:
-npm install jwt-decode
