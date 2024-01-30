# DataBase2Project
Projekt na przedmiocie Bazy Danych 2, implementujący studencki indeks elektroniczny - Jsos 3.0.

## Autorzy
- Adam Czekalski
- Filip Murawski
- Maciej Padula
- Olaf Szajda

## Stworzenie bazy
Aby skorzystać z aplikacji należy najpierw stworzyć serwer Microsoft SQL Server np. uruchamiając go lokalnie na dockerze: https://hub.docker.com/_/microsoft-mssql-server
Następnie w pliku appsettings.json należy podać connection string do bazy w nowow utworzonym serwerze. Uruchomienie pliku ScriptsExecutor.exe powinno wykonać wszystkie skrypty z katalogu Queries i stworzyć struktury bazy.

## Uruchomienie aplikacji

Instalację rozpoczynamy od pobrania pliku zip z zarchiwizowaną aplikacją. Jego zawartość powinna wyglądać następująco:
![image](https://github.com/AdamusPL/DataBase2Project/assets/50674232/01590d84-cfe1-43e4-a328-03efeec8d252)

Aby uruchomić aplikację należy najpierw skonfigurować podłączenie do bazy. Oznacza to, że należy podać tzw. ConnectionStringi dla użytkowników bazy danych oraz klucz do szyfrowania haseł:
```JSON
{
  "ConnectionStrings": {
    "StudentSql": "Server=172.28.0.1;Database=University-Main;User Id=Student;Password=zaq1@WSX;TrustServerCertificate=True",
    "AdministrationSql": "Server=172.28.0.1;Database=University-Main;User Id=Administration;Password=zaq1@WSX;TrustServerCertificate=True",
    "LecturerSql": "Server=172.28.0.1;Database=University-Main;User Id=Lecturer;Password=zaq1@WSX;TrustServerCertificate=True"
  },
  "Jwt": {
    "Key": "token o dlugosci conajmniej trzydziestu dwoc znakow do szyfrowania hasel",
    "Issuer": "adres_ip_aplikacji:port_aplikacji"
  }
}
```

Każdy connection string odpowiada połączeniu do bazy jednego użytkownika bazodanowego. Należy ustawienia te wypełnić takimi które odpowiadać będą infrastrukturze uruchomionej na uczelni. Dane do podania to: adresIp bazy danych, hasła dla użytkowników.
Zmiany te można dokonać w pliku appsettings.json albo dodać wartości te do zmiennych środowiskowych pod następującymi nazwami:
- ConnectionStrings__StudentSql
- ConnectionStrings__AdministrationSql
- ConnectionStrings__LecturerSql 
- Jwt__Key
- Jwt__Issuer

Uruchomienie
Aby uruchomić aplikację należy w wierszu poleceń będąc w katalogu aplikacji wpisać polecenie: `.\Jsos3.exe --urls http://adresIpAplikacji:portAplikacji`
Po włączeniu powinna pokazać się konsola:
![image](https://github.com/AdamusPL/DataBase2Project/assets/50674232/9d534794-f2f7-4e0f-a62f-215a567e67a1)

Od tego momentu pod adresem ip hostującej maszyny strona powinna być dostępna:
![image](https://github.com/AdamusPL/DataBase2Project/assets/50674232/db4b1a61-4bad-42d2-85c8-6485b884c7b0)

### Uwaga aby działało poprawnie logowanie należy uruchomić aplikację z https co równa się z posiadaniem domeny
